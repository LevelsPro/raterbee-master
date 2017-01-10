﻿using ApplicaitonGeneration;
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
                    question = questions.First(q => q.Id == answer.QuestionId).Question.Trim(),
                    answer = answer.SurveyAnswer,
                    datesubmitted = answer.DateSubmitted == null ? DateTime.MinValue : answer.DateSubmitted.Value
                });

            }
            _unitOfWork.Complete();

            // Distribution Answers chart
            ViewBag.AnswerChart = new List<string[]>();
            ViewBag.AnswerChart.Add(new string[] { "Answer", "Count" });
            var res = surveyAnswers  
                        .Where(sa=> sa.QuestionId != 5)             // Don't include free form comments
                        .GroupBy(r => r.SurveyAnswer) 
                        .Select(g => new { Rating = g.Key.Trim(), Count = g.Count() }) 
                        .ToList();
            foreach (var answerCount in res)
            {
                ViewBag.AnswerChart.Add(new string[] { answerCount.Rating, answerCount.Count.ToString() });
            }

            // Distribution questions chart
            ViewBag.QuestionChart = new List<string[]>();
            ViewBag.QuestionChart.Add(new string[] { "Question", "Count" });
            var res2 = surveyAnswers  
                        .GroupBy(r => r.rb_SurveyQuestions.Id) 
                        .Select(g => new { QuestionId = g.Key, Count = g.Count() }) 
                        .ToList();
            foreach (var answerCount in res2)
            {
                ViewBag.QuestionChart.Add(new string[] {
                    questions
                        .First(q => q.Id == answerCount.QuestionId)
                        .Question.Trim(),
                    answerCount.Count.ToString()
                });
            }

            answers.Reverse();
            return View(answers.Take(50));
        }
    }
}
