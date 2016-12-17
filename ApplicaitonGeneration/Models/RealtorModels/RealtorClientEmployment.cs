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
    public class RealtorClientEmployment
    {
        public int Id { get; set; }
        public int RealtorClientId { get; set; }

        public UserAddress Address { get; set; }
        public UserOccupation Occupation { get; set; }
        public UserSalary Salary { get; set; }
        public UserReference Supervisor { get; set; }

        public void Init()
        {
            Address = new UserAddress();
            Occupation = new UserOccupation();
            Salary = new UserSalary();
            Supervisor = new UserReference();
        }

        public RealtorClientEmployment(db_RealtorEmployments re)
        {
            Init();
            if (re != null)
            {
                Id = re.Id;
                RealtorClientId = re.ClientId;
                Address = new UserAddress(re.db_UserAddresses);
                Occupation = new UserOccupation(re.db_UserOccupations);
                Salary = new UserSalary(re.db_UserSalaries);
                Supervisor = new UserReference(re.db_UserReferences);
            }
        }


        public void DeleteObject_db(IUnitOfWork unitOfWork)
        {
            var context = unitOfWork.RealtorEmployments;
            var toDelete = unitOfWork.RealtorEmployments.Get(Id);
            if (toDelete != null)
                context.Remove(toDelete);
        }

        public db_RealtorEmployments CreateObject_db(IUnitOfWork unitOfWork, db_RealtorClients rc, AspNetUser currentUser)
        {
            db_UserOccupations occupationobj = null;
            db_UserAddresses addressobj = null;
            db_UserSalaries salaryobj = null;
            db_UserReferences referenceobj = null;

            if (Occupation.Id > 0)
                unitOfWork.UserOccupations.Attach(Occupation.CreateObject_db(currentUser));
            else
                occupationobj = unitOfWork.UserOccupations.Add(Occupation.CreateObject_db(currentUser));

            if (Address.Id > 0)
                unitOfWork.UserAddresses.Attach(Address.CreateObject_db(currentUser));
            else
                addressobj = unitOfWork.UserAddresses.Add(Address.CreateObject_db(currentUser));

            if (Salary.Id > 0)
                unitOfWork.UserSalaries.Attach(Salary.CreateObject_db(currentUser));
            else
                salaryobj = unitOfWork.UserSalaries.Add(Salary.CreateObject_db(currentUser));

            if (Supervisor.Id > 0)
                unitOfWork.UserReferences.Attach(Supervisor.CreateObject_db(unitOfWork, rc, currentUser));
            else
                referenceobj = unitOfWork.UserReferences.Add(Supervisor.CreateObject_db(unitOfWork, rc, currentUser));

            return new db_RealtorEmployments()
            {
                Id = this.Id,
                ClientId = rc.Id,
                AddressId = (Address.Id > 0) ? Address.Id : default(int?),
                OccupationId = (Occupation.Id > 0) ? Occupation.Id : default(int?),
                SalaryId = (Salary.Id > 0) ? Salary.Id : default(int?),
                ReferenceId = (Supervisor.Id > 0) ? Supervisor.Id : default(int?),
                db_UserOccupations = occupationobj,
                db_UserAddresses = addressobj,
                db_UserSalaries = salaryobj,
                db_UserReferences = referenceobj
            };
        }


        public RealtorClientEmployment()
        {
            Init();
        }
    }
}