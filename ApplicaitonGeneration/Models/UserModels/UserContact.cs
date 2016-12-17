using ApplicaitonGeneration;
using ApplicaitonGeneration.Helpers;
using ApplicationGeneration.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace ApplicationGeneration.Models
{
    public class UserContact
    {
        public int Id { get; set; }
        public int userid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public UserContact()
        {
        }

        public UserContact(db_UserContacts uc)
        {
            if (uc != null)
            {
                Id = uc.Id;
                userid = uc.UserId;
                FirstName = uc.FirstName;
                LastName = uc.LastName;
                Email = uc.Email;
                PhoneNumber = uc.Phonenumber;
            }
        }

        public db_UserContacts CreateObject_db(AspNetUser currentUser)
        {
            return new db_UserContacts()
            {
                Id = this.Id,
                UserId = (currentUser != null) ? currentUser.Id : 0,
                AspNetUser = currentUser,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phonenumber = PhoneNumber
            };
        }


        public UserContact(ref int[] id, 
            ref string[] Firstname,
            ref string[] Lastname,
            ref string[] Email,
            ref string[] Phoennumber)
        {
            Id = Extensions.PopFirst(ref id);
            FirstName = (Firstname != null) ? Extensions.PopFirst(ref Firstname) : "";
            LastName = (Lastname != null) ? Extensions.PopFirst(ref Lastname) : "";
            this.Email = (Email != null) ? Extensions.PopFirst(ref Email) : "";
            PhoneNumber = (Phoennumber != null) ? Extensions.PopFirst(ref Phoennumber) : "";
        }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}