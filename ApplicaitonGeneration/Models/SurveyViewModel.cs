using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationGeneration.Models
{
    public class SurveyViewModel
    {
        public int id { get; set; }
        public string company { get; set; }
        public int beacon { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public DateTime datesubmitted { get; set; }


    }


}