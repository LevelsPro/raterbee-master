using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaterBee.Models
{
    public class SurveyModel
    {
        public int CompanyId { get; set; }
        public int BeaconId { get; set; }
        public Guid Guid { get; set; }
        public List<Survey> SurveyList { get; set; }
        public List<SurveyQuestion> SurveyQuestions { get; set; }
    }
    

    public class Survey
    {
        public int Question { get; set; }
        public string Answer { get; set; }
    }

    public class SurveyQuestion
    {
        public string Question { get; set; }
        public int Id { get; set; }
    }


}