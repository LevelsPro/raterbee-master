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
    public class RealtorClientReference
    {
        public int Id { get; set; }
        public int RealtorClientId { get; set; }

        public int ReferenceType { get; set; } 
        public UserReference Reference { get; set; }

        public void Init()
        {
            Reference = new UserReference();
        }
                 
        public RealtorClientReference()
        {
            Init();
        }
        public RealtorClientReference(db_RealtorReferences rr)
        {
            Init();
            if (rr != null)
            {
                Id = rr.Id;
                RealtorClientId = rr.ClientId;
                ReferenceType = rr.ReferenceType;
                Reference = new UserReference(rr.db_UserReferences);
            }
        }

        public void DeleteObject_db(IUnitOfWork unitOfWork)
        {
            var context = unitOfWork.RealtorReferences;
            var toDelete = unitOfWork.RealtorReferences.Get(Id);
            if (toDelete != null)
                context.Remove(toDelete);
        }

        public db_RealtorReferences CreateObject_db(IUnitOfWork unitOfWork, db_RealtorClients rc, AspNetUser currentUser)
        {
            db_UserReferences referenceobj = null;

            if (Reference.Id > 0)
                unitOfWork.UserReferences.Attach(Reference.CreateObject_db(unitOfWork, rc, currentUser));
            else
                referenceobj = unitOfWork.UserReferences.Add(Reference.CreateObject_db(unitOfWork, rc, currentUser));

            return new db_RealtorReferences()
            {
                Id = this.Id,
                ClientId = rc.Id,
                ReferenceId = (Reference.Id > 0) ? Reference.Id : default(int?),
                ReferenceType = ReferenceType,
                db_UserReferences = referenceobj
            };
        }


        //public RealtorClientReference(
        //    ref int[] id,
        //     int referenceType,
        //     ref string[] referenceFirstName,
        //     ref string[] referenceLastName,
        //     ref string[] referenceEmail,
        //     ref string[] referencePhonenumber,
        //     ref string[] referenceRelationship,
        //     ref string[] referenceTitle)
        //{
        //    var arr = new String[0];
        //    Id = Extensions.PopFirst(ref id);
        //    ReferenceType = referenceType;
        //    Reference = new Reference(ref id, ref referenceFirstName, ref referenceLastName, ref referenceEmail, ref referencePhonenumber, ref referenceRelationship, ref referenceTitle);
        //}
    }
}