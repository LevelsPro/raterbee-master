using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationGeneration.Models
{
    public class SurveyModel
    {
        public int CompanyId { get; set; }
        public int BeaconId { get; set; }
        public Guid Guid { get; set; }
        public List<Survey> SurveyList { get; set; }
    }

    public class Survey
    {
        public int Question { get; set; }
        public string Answer { get; set; }
    }


}