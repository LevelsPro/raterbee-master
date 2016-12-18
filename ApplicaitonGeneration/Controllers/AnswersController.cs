using ApplicationGeneration;
using ApplicationGeneration.DAL;
using ApplicationGeneration.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ApplicationGeneration.Controllers
{
    [Authorize]
    public class AnswersController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public AnswersController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationEntities());

        }

        public ActionResult Index()
        {
            ViewBag.Message = "RaterBee Answers";
            var answers = new List<SurveyViewModel>();
            int CompanyId = 0;
            var user = _unitOfWork.Users.Get(User.Identity.GetUserId<int>());
            if (user != null) { CompanyId = user.CompanyId.HasValue ? user.CompanyId.Value : 0; }

            var questions = _unitOfWork.SurveyQuestions.GetAll().Select(q => new { q.Id, q.Question });
            IEnumerable<rb_SurveyAnswers> surveyAnswers;
            if (User.IsInRole("SuperAdmin"))
            {
                surveyAnswers = _unitOfWork.SurveyAnswers.GetAll();
            }
            else
            {
                surveyAnswers = _unitOfWork.SurveyAnswers.Find(sa => sa.CompanyId == CompanyId);
            }
            foreach (var answer in surveyAnswers)
            {
                answers.Add(new SurveyViewModel()
                {
                    id = answer.Id,
                    company = answer.rb_Companies.CompanyName,
                    beacon = answer.BeaconId == null ? 0 : answer.BeaconId.Value,
                    question = questions.First(q => q.Id == answer.QuestionId).Question,
                    answer = answer.SurveyAnswer,
                    datesubmitted = answer.DateSubmitted == null ? DateTime.MinValue : answer.DateSubmitted.Value
                });
            }
            _unitOfWork.Complete();

            answers.Reverse();
            return View(answers);
        }
    }
}
