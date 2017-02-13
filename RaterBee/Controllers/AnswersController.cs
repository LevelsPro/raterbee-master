using ApplicaitonGeneration;
using RaterBee;
using RaterBee.DAL;
using RaterBee.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;



namespace RaterBee.Controllers
{
    [Authorize]
    public class AnswersController : Controller
    {
        const int CommentQuestionNumber = 5;

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

            var questions = _unitOfWork.SurveyQuestions.Find(q => (q.FreeFormComment.HasValue ? !q.FreeFormComment.Value : true)).Select(q => new { q.Id, q.Question });
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
                var question = "";
                var q = questions.FirstOrDefault(que => que.Id == answer.QuestionId);
                if (q != null)
                    question = q.Question.Trim();
                answers.Add(new SurveyViewModel()
                {
                    id = answer.Id,
                    company = answer.rb_Companies.CompanyName,
                    beacon = answer.BeaconId == null ? 0 : answer.BeaconId.Value,
                    Guid = answer.Guid.HasValue ? answer.Guid.Value.ToString() : "",
                    question = question,
                    answer = answer.SurveyAnswer,
                    datesubmitted = answer.DateSubmitted == null ? DateTime.MinValue : answer.DateSubmitted.Value
                });

            }
            _unitOfWork.Complete();

            //// Distribution Answers chart
            //DateTime now = DateTime.Now;
            //ViewBag.AnswerChart = new string[25,5];
            //for (int x=1;x<25; x++)
            //{
            //    for (int z = 0; z < 5; z++)
            //    {
            //        ViewBag.AnswerChart[x, z] = "0";
            //    }
            //}
            //var y = 0;
            //ViewBag.AnswerChart[0, y++] = "Hours";
            //foreach (var question in questions)
            //{
            //    ViewBag.AnswerChart[0, y++] = question.Question.Trim();
            //}
            //var res = surveyAnswers
            //            .Where(sa => sa.QuestionId != CommentQuestionNumber) //&& sa.DateSubmitted > now.AddHours(-24) && sa.DateSubmitted <= now)             // Don't include free form comments]
            //            .GroupBy(
            //            r => new { Hour = (r.DateSubmitted.HasValue ? r.DateSubmitted.Value.Hour : 0), r.SurveyAnswer }, (key, group) => new
            //            {
            //                Key1 = key.Hour,
            //                Key2 = key.SurveyAnswer,
            //                Result = group.ToList()
            //            })
            //            //(r => new { Hour = (r.DateSubmitted.HasValue ? r.DateSubmitted.Value.Hour : 0), Rating = r.SurveyAnswer }) 
            //            //.Select(g => new { GroupedRating = g.Key, Count = g.Count() }) 
            //            .ToList();

            //foreach (var answerCount in res)
            //{
            //    var questionCount = new int[5];
            //    var answerTotal = new double[5];
            //    var key = answerCount.Key1.ToString();
            //    var k = 0;
            //    Int32.TryParse(key, out k);
            //    k++;                                        // Get rid of 0 time
            //    foreach (var rating in answerCount.Result)
            //    {
            //        var value = 0;
            //        Int32.TryParse(rating.SurveyAnswer, out value);
            //        answerTotal[rating.QuestionId] += value;
            //        questionCount[rating.QuestionId]++;
            //    }

            //    for (int x = 0; x < 5; x++)
            //    {
            //        if (questionCount[x] > 0)
            //        {
            //            ViewBag.AnswerChart[k, x] = (answerTotal[x] / questionCount[x]).ToString();
            //        } else
            //        {
            //            ViewBag.AnswerChart[k, x] = "0";
            //        }
            //    }
            //}

            answers.Reverse();
            return View(answers.Take(200));
        }
    }
}
