using ApplicaitonGeneration;
using ApplicationGeneration;
using ApplicationGeneration.DAL;
using ApplicationGeneration.Helpers;
using ApplicationGeneration.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ApplicationGeneration.Controllers
{
    public class SurveyController : Controller
    {
        const int CommentQuestionNumber = 5;         // Hardcoded for Feedback  quesiton At the moment (valid on development and production)

        ApplicationEntities context = new ApplicationEntities();
        private IUnitOfWork _unitOfWork;
        public SurveyController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationEntities());
        }

        public ActionResult New(int Id)
        {
            var companyId = 0;
            var companyName = "";
            var Guid = System.Guid.NewGuid();
            var beacon = _unitOfWork.Beacons.Find(b => b.Id == Id).FirstOrDefault();
            if (beacon != null)
            {
                companyId = beacon.rb_Companies.Id;
                companyName = beacon.rb_Companies.CompanyName.Trim();
            }
            if (companyId > 0)
            {
                ViewBag.Message = companyName + " Survey";
                return View(companyName, new SurveyModel() { CompanyId = companyId, BeaconId = Id, Guid = Guid });
            }
            else
                return View("Error");
        }


        public ActionResult Index()
        {
            return View(new SurveyModel() { CompanyId = 1 });
        }

        public ActionResult Thanks(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return View(name + "ThanksYou");
            }
            return View();
        }

        [HttpPost]
        public ActionResult New(SurveyModel model)
        {
            var feedbackLessThanThreeStars = false;
            if (model.CompanyId > 0)
            {
                var timenow = DateTime.Now;
                foreach (var ans in model.SurveyList)
                {
                    var intAnswer = 0;
                    Int32.TryParse(ans.Answer, out intAnswer);
                    if (intAnswer > 0 && intAnswer < 3)
                    {
                        feedbackLessThanThreeStars = true;
                    }
                    _unitOfWork.SurveyAnswers.Add(new rb_SurveyAnswers()
                    {
                        CompanyId = model.CompanyId,
                        BeaconId = model.BeaconId,
                        Guid = model.Guid,
                        QuestionId = ans.Question,
                        SurveyAnswer = ans.Answer,
                        DateSubmitted = timenow
                    });
                }
                _unitOfWork.Complete();
            }

            var beacon = _unitOfWork.Beacons.Find(b => b.Id == model.BeaconId).FirstOrDefault();
            if (beacon != null)
            {
                // TODO - refacor with feedback function call
                var companyId = beacon.rb_Companies.Id;
                var companyName = beacon.rb_Companies.CompanyName.Trim();
                var removeThankYouPage = beacon.RemoveThankYouPage.HasValue ? beacon.RemoveThankYouPage.Value : false;
                var addFeedbackPage = beacon.AddFeedbackPage.HasValue ? beacon.AddFeedbackPage.Value : false;
                if (feedbackLessThanThreeStars && addFeedbackPage)
                {
                    return Json(new { RedirectTo = "/Survey/Feedback/?name=" + companyName + "&guid=" + model.Guid + "&beaconId=" + model.BeaconId});
                }
                else if (removeThankYouPage)
                {
                    return Json(new { RedirectTo = "/Survey/new/" + beacon.Id });
                }
                return Json(new { RedirectTo = "/Survey/Thanks/?name=" + companyName });
            }
            return Json(new { RedirectTo = "/Survey/Thanks" });
        }


        public ActionResult Feedback(string name, Guid guid, int beaconId)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return View(name + "Feedback", new SurveyModel() { Guid = guid, BeaconId = beaconId });
            }

            //there was an error, return generic thanks page
            return View("Thanks");
        }
        public ActionResult FeedbackSubmit(int BeaconId, Guid guid, string FeedbackString)
        {
            var companyId = 0;
            var companyName = "";
            var Guid = System.Guid.NewGuid();
            var beacon = _unitOfWork.Beacons.Find(b => b.Id == BeaconId).FirstOrDefault();
            if (beacon != null)
            {
                companyId = beacon.rb_Companies.Id;
                companyName = beacon.rb_Companies.CompanyName.Trim();
            }
            if (!string.IsNullOrEmpty(companyName))
            {
                // TODO - refacor with New function call
                var timenow = DateTime.Now;
                _unitOfWork.SurveyAnswers.Add(new rb_SurveyAnswers()
                {
                    CompanyId = companyId,
                    BeaconId = BeaconId,
                    Guid = guid,
                    QuestionId = CommentQuestionNumber,
                    SurveyAnswer = FeedbackString,
                    DateSubmitted = timenow
                });
                _unitOfWork.Complete();

                var removeThankYouPage = beacon.RemoveThankYouPage.HasValue ? beacon.RemoveThankYouPage.Value : false;
                if (removeThankYouPage)
                {
                    return RedirectToAction("New", new  {Id = beacon.Id });
                }
                return RedirectToAction("Thanks", new { name= companyName });
            }

            //there was an error, return generic thanks page
            return View("Thanks");
        }
        
        [HttpPost]
        public ActionResult Email(string Email)
        {
            MailHelper.EmailFromArvixe(Email, "Here is your coupon", "Coupon stuff");
            return View("EmailSent");
        }
    }
}
