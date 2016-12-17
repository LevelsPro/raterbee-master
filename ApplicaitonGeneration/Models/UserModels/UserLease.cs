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
    public class UserLease
    {
        public int Id { get; set; }
        public int userid { get; set; }
        public Timespan Timespan { get; set; }

        public string MonthlyRent { get; set; }
        public string SecurityDeposit { get; set; }
        public string GarageFee { get; set; }
        public string UtilitiesIncludedInRent { get; set; }

        public void Init()
        {
            Timespan = new Timespan();
        }

        public UserLease()
        {
            Init();
        }

        public UserLease(db_UserLeases ul)
        {
            Init();
            if (ul != null)
            {
                Id = ul.Id;
                userid = ul.UserId;
                MonthlyRent = ul.MonthlyRent.ToString();
                SecurityDeposit = ul.SecurityDeposit.ToString();
                GarageFee = ul.garagefee.ToString();
                UtilitiesIncludedInRent = ul.UtilitiesIncludedInRent.ToString();
                Timespan.startdate = ul.StartDate.ToString();
                Timespan.enddate = ul.EndDate.ToString();
            }
        }

        public db_UserLeases CreateObject_db(AspNetUser currentUser)
        {
            return new db_UserLeases()
            {
                Id = this.Id,
                UserId = (currentUser != null) ? currentUser.Id : 0,
                AspNetUser = currentUser,
                MonthlyRent = MonthlyRent.TryParseDecimal(),
                SecurityDeposit = SecurityDeposit.TryParseDecimal(),
                garagefee = GarageFee.TryParseDecimal(),
                UtilitiesIncludedInRent = UtilitiesIncludedInRent.TryParseDecimal()
            };
        }

        public UserLease(ref int[] id,
                 ref string[] Start,
                 ref string[] End,
                 ref string[] MonthlyRent,
                 ref string[] SecurityDeposit,
                 ref string[] GarageFee,
                 ref string[] UtilitiesIncludedInRent)
        {
            this.Id = Extensions.PopFirst(ref id);
            Timespan = new Timespan(ref Start, ref End);
            this.MonthlyRent = Extensions.PopFirst(ref MonthlyRent);
            this.SecurityDeposit = Extensions.PopFirst(ref SecurityDeposit);
            this.GarageFee = Extensions.PopFirst(ref GarageFee);
            this.UtilitiesIncludedInRent = Extensions.PopFirst(ref UtilitiesIncludedInRent);
        }

    }
}