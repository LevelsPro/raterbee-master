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
    public class UserAccount
    {
        public int Id { get; set; }
        public int userid { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string AccountNumber { get; set; }

        public UserAccount() { }

        public UserAccount(db_UserAccounts ua)
        {
            if (ua != null)
            {
                Id = ua.Id;
                userid = ua.UserId;
                AccountName = ua.Name;
                AccountType = ua.Type;
                AccountNumber = ua.Number;
            }
        }

        public db_UserAccounts CreateObject_db(AspNetUser currentUser)
        {
            return new db_UserAccounts()
            {
                Id = this.Id,
                UserId = (currentUser != null) ? currentUser.Id : 0,
                AspNetUser = currentUser,
                Name = AccountName,
                Type = AccountType,
                Number = AccountNumber
            };
        }

        public UserAccount(ref int[] id,
                ref string[] Accountname,
                ref string[] Accounttype,
                ref string[] Accountnumber)
        {
            Id = Extensions.PopFirst(ref id);
            AccountName = (Accountname != null) ? Extensions.PopFirst(ref Accountname).EmptyIfNull() : "";
            AccountType = (Accounttype != null) ? Extensions.PopFirst(ref Accounttype).EmptyIfNull() : "";
            AccountNumber = (Accountnumber != null) ? Extensions.PopFirst(ref Accountnumber).EmptyIfNull() : "";
        }
    }
}