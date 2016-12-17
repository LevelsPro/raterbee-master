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
    public class UserSalary
    {
        public int Id { get; set; }
        public int userid { get; set; }

        public string AnnualSalary { get; set; }
        public string Bonus { get; set; }
        public string OtherIncomeSource { get; set; }
        public string OtherIncomeAmount { get; set; }
        public string TotalAnnualIncome { get; set; }

        public UserSalary() { }

        public UserSalary(db_UserSalaries us)
        {
            if (us != null)
            {
                Id = us.Id;
                userid = us.UserId;
                AnnualSalary = us.AnnualSalary.ToString();
                Bonus = us.Bonus.ToString();
                OtherIncomeSource = us.OtherIncomeSource.ToString();
                OtherIncomeAmount = us.OtherIncomeAmount.ToString();
                TotalAnnualIncome = us.TotalAnnualIncome.ToString();
            }
        }

        public db_UserSalaries CreateObject_db(AspNetUser currentUser)
        {
            return new db_UserSalaries()
            {
                Id = this.Id,
                UserId = (currentUser != null) ? currentUser.Id : 0,
                AspNetUser = currentUser,
                AnnualSalary = AnnualSalary.TryParseInt(),
                Bonus = Bonus.TryParseDecimal(),
                OtherIncomeSource = OtherIncomeSource.TryParseDecimal(),
                OtherIncomeAmount = OtherIncomeAmount.TryParseDecimal(),
                TotalAnnualIncome = TotalAnnualIncome.TryParseDecimal()
            };
        }

        public UserSalary(ref int[] id,
                ref string[] annualSalary,
                ref string[] bonus,
                ref string[] otherIncomeSource,
                ref string[] otherIncomeAmount,
                ref string[] totalAnnualIncome)
        {
            Id = Extensions.PopFirst(ref id);
            AnnualSalary = Extensions.PopFirst(ref annualSalary).EmptyIfNull();
            Bonus = Extensions.PopFirst(ref bonus).EmptyIfNull();
            OtherIncomeSource = Extensions.PopFirst(ref otherIncomeSource).EmptyIfNull();
            OtherIncomeAmount = Extensions.PopFirst(ref otherIncomeAmount).EmptyIfNull();
            TotalAnnualIncome = Extensions.PopFirst(ref totalAnnualIncome).EmptyIfNull();
        }
    }
}