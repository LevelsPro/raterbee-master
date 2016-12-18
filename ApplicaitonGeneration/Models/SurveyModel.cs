using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationGeneration.Models
{
    public class SurveyModel
    {
        public int CompanyId { get; set; }
        public int BeaconId { get; set; }
        public List<Survey> SurveyList { get; set; }
    }

    public class Survey
    {
        public int Question { get; set; }
        public int Answer { get; set; }
    }


}