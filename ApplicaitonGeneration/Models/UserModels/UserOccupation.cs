using ApplicaitonGeneration;
using ApplicaitonGeneration.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace ApplicationGeneration.Models
{
    public class UserOccupation
    {
        public int Id { get; set; }
        public int userid { get; set; }
        public Timespan Timespan { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }

        public void Init()
        {
            Timespan = new Timespan();
        }

        public UserOccupation() {
            Init();
        }

        public UserOccupation(db_UserOccupations uo)
        {
            Init();
            if (uo != null)
            {
                Id = uo.Id;
                userid = uo.UserId;
                Timespan.startdate = uo.StartDate.ToString();
                Timespan.enddate = uo.EndDate.ToString();
                Occupation = uo.Occupation;
                CompanyName = uo.CompanyName;
            }

        }

        public db_UserOccupations CreateObject_db(AspNetUser currentUser)
        {
            return new db_UserOccupations()
            {
                Id = this.Id,
                UserId = (currentUser != null) ? currentUser.Id : 0,
                AspNetUser = currentUser,
                Occupation = Occupation,
                CompanyName = CompanyName,
                StartDate =Timespan.StartDate.TryParseDate(),
                EndDate = Timespan.EndDate.TryParseDate()
            };
        }

        public UserOccupation(ref int[] id,
                ref string [] occupation,
                ref string [] companyName,
                ref string [] employmentStart,
                ref string [] employmentEnd)
        {
            Id = Extensions.PopFirst(ref id);
            Occupation = Extensions.PopFirst(ref occupation).EmptyIfNull();
            CompanyName = Extensions.PopFirst(ref companyName).EmptyIfNull();
            Timespan = new Timespan(ref employmentStart, ref employmentEnd);
        }
    }
}