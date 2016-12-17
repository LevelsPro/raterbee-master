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
    public class UserReference
    {
        public int Id { get; set; }
        public int userid { get; set; }
        public string Relationship { get; set; }
        public string Title { get; set; }
        public UserContact ContactInfo { get; set; }

        public void Init()
        {
            ContactInfo = new UserContact();
        }

        public UserReference()
        {
            Init();
        }

        public UserReference(db_UserReferences ur)
        {
            Init();
            if (ur != null)
            {
                Id = ur.Id;
                userid = ur.UserId;
                ContactInfo = new UserContact(ur.db_UserContacts);
                Relationship = ur.Relationship;
                Title = ur.Title;
            }
        }

        public db_UserReferences CreateObject_db(IUnitOfWork unitOfWork, db_RealtorClients rc, AspNetUser currentUser)
        {
            db_UserContacts contactobj = null;

            if (ContactInfo.Id > 0)
                unitOfWork.UserContacts.Attach(ContactInfo.CreateObject_db(currentUser));
            else
                contactobj = unitOfWork.UserContacts.Add(ContactInfo.CreateObject_db(currentUser));

            return new db_UserReferences()
            {
                Id = this.Id,
                UserId = (currentUser != null) ? currentUser.Id : 0,
                ContactId = (ContactInfo.Id > 0) ? ContactInfo.Id : default(int?),
                AspNetUser = currentUser,
                Relationship = Relationship,
                Title = Title,
                db_UserContacts = contactobj,
            };
        }


        public UserReference(int id,
             string relationship,
             string title,
            UserContact contact)
        {
            Id = id;
            Relationship = relationship;
            Title = title;
            ContactInfo = contact;
        }
    }
}