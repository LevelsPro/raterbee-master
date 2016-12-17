using ApplicaitonGeneration.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace ApplicationGeneration.Models
{
    public class Timespan
    {
        public string startdate { get; set; }
        public string enddate { get; set; }

        public Timespan() { }

        public Timespan(
         ref string[] Start,
         ref string[] End)
        {
            startdate = (Start != null) ? Extensions.PopFirst(ref Start).EmptyIfNull() : "";
            enddate = (End != null) ? Extensions.PopFirst(ref End).EmptyIfNull() : "";
        }

        public string StartDate
        {
            get
            {
                var result = "";
                var datearray = (startdate != null && startdate.Length > 0) ? startdate.Split(' ') : new string[0];
                if (datearray != null & datearray.Length > 0)
                    result = datearray[0];
                return result;
            }
        }

        public string EndDate
        {
            get
            {
                var result = "";
                var datearray = (enddate != null && enddate.Length > 0) ? enddate.Split(' ') : new string[0];
                if (datearray != null & datearray.Length > 0)
                    result = datearray[0];
                return result;
            }
        }
    }
}