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
        ApplicationEntities context = new ApplicationEntities();
        private IUnitOfWork _unitOfWork;
        public SurveyController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationEntities());
        }

        //public ActionResult WinterWear()
        //{
        //    var companyName = "WinterWear";
        //    ViewBag.Message = companyName + " Survey";
        //    var company = _unitOfWork.Companies.Find(c => c.CompanyName == companyName).FirstOrDefault();
        //    if (company != null)
        //    {
        //        return View(new SurveyModel() { CompanyId = company.Id });
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}
        //public ActionResult Modells()
        //{
        //    var companyName = "Modells";
        //    ViewBag.Message = companyName + " Survey";
        //    var company = _unitOfWork.Companies.Find(c => c.CompanyName == companyName).FirstOrDefault();
        //    if (company != null)
        //    {
        //        return View(new SurveyModel() { CompanyId = company.Id });
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}
        public ActionResult New(int Id)
        {
            var companyId = 0;
            var companyName = "";
            var beacon = _unitOfWork.Beacons.Find(b => b.Id == Id).FirstOrDefault();
            if (beacon != null)
            {
                companyId = beacon.rb_Companies.Id;
                companyName = beacon.rb_Companies.CompanyName.Trim();
            }
            if (companyId > 0)
            {
                ViewBag.Message = companyName + " Survey";
                return View(companyName, new SurveyModel() { CompanyId = companyId, BeaconId = Id });
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
            if (model.CompanyId > 0)
            {
                var timenow = DateTime.Now;
                foreach (var ans in model.SurveyList)
                {
                    _unitOfWork.SurveyAnswers.Add(new rb_SurveyAnswers()
                    {
                        CompanyId = model.CompanyId,
                        BeaconId = model.BeaconId,
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
                var companyId = beacon.rb_Companies.Id;
                var companyName = beacon.rb_Companies.CompanyName.Trim();
                var removeThankYouPage = beacon.RemoveThankYouPage.HasValue ? beacon.RemoveThankYouPage.Value : false;
                if (removeThankYouPage)
                {
                    return Json(new { RedirectTo = "/Survey/new/" + beacon.Id } );
                }
                return Json(new { RedirectTo = "/Survey/Thanks/?name=" + companyName });
            }
            return Json(new { RedirectTo = "/Survey/Thanks" });
        }

        [HttpPost]
        public ActionResult Email(string Email)
        {
            MailHelper.EmailFromArvixe(Email, "Here is your coupon", "Coupon stuff");
            return View("EmailSent");
        }
    }
}
