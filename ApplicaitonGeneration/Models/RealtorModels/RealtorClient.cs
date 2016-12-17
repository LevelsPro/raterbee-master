
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using ApplicaitonGeneration.Helpers;
using ApplicationGeneration.DAL;
using ApplicaitonGeneration;
using System.Linq;

namespace ApplicationGeneration.Models
{
    public class RealtorClient
    {
        public int Id { get; set; }
        public int userid { get; set; }

        public UserContact ContactInfo { get; set; }
        public string relationship { get; set; }            // Needed to relate 2 clients or a client and a guarantor
        public string education { get; set; }               // Eduction needed for applicants

        public RealtorClient Guarantor { get; set; }

        public RealtorClientHome ApartmentApplyingTo { get; set; }
        public List<RealtorClientHome> Homes { get; set; }
        public List<RealtorClientEmployment> Employments { get; set; }

        public List<RealtorClientBank> Banks { get; set; }

        //public List<Reference> BankCpas { get; set; }

        public List<RealtorClientReference> PersonalReferences { get; set; }
        public List<RealtorClientReference> ProfessionalReferences { get; set; }

        public void Init()
        {
            ApartmentApplyingTo = new RealtorClientHome();
            ContactInfo = new UserContact();

            Homes = new List<RealtorClientHome>();
            Homes.Add(new RealtorClientHome());

            Employments = new List<RealtorClientEmployment>();
            Employments.Add(new RealtorClientEmployment());

            Banks = new List<RealtorClientBank>();
            Banks.Add(new RealtorClientBank());

            PersonalReferences = new List<RealtorClientReference>();
            PersonalReferences.Add(new RealtorClientReference());

            ProfessionalReferences = new List<RealtorClientReference>();
            ProfessionalReferences.Add(new RealtorClientReference());
        }

        public RealtorClient()
        {
            Init();
        }

        public RealtorClient(db_RealtorClients rc)
        {
            Init();
            if (rc != null)
            {
                Id = rc.Id;
                userid = rc.UserId;
                //education = rc.education;
                ContactInfo = new UserContact(rc.db_UserContacts);
                if (rc.db_RealtorHomes.Count > 0) Homes.RemoveAt(0);
                foreach (var home in rc.db_RealtorHomes)
                    Homes.Add(new RealtorClientHome(home));

                if (rc.db_RealtorEmployments.Count > 0) Employments.RemoveAt(0);
                foreach (var employment in rc.db_RealtorEmployments)
                    Employments.Add(new RealtorClientEmployment(employment));

                if (rc.db_RealtorBanks.Count > 0) Banks.RemoveAt(0);
                foreach (var bank in rc.db_RealtorBanks)
                    Banks.Add(new RealtorClientBank(bank));

                if (rc.db_RealtorReferences.Where(rr => rr.ReferenceType == 1).Count() > 0) PersonalReferences.RemoveAt(0);
                foreach (var reference in rc.db_RealtorReferences.Where(rr => rr.ReferenceType == 1))
                    PersonalReferences.Add(new RealtorClientReference(reference));

                if (rc.db_RealtorReferences.Where(rr => rr.ReferenceType == 2).Count() > 0) ProfessionalReferences.RemoveAt(0);
                foreach (var reference in rc.db_RealtorReferences.Where(rr => rr.ReferenceType == 2))
                    ProfessionalReferences.Add(new RealtorClientReference(reference));
            }
        }

        public RealtorClient(
                    int[] Id,
                    string clientcount,
                    string homecount,
                    string employmentcount,
                    string bankcount,
                    string personalcount,
                    string professionalcount,

                    string[] FirstName,
                    string[] LastName,
                    string[] Email,
                    string[] PhoneNumber,

                    string[] Education,

                    string[] AddressNumber,
                    string[] Street,
                    string[] City,
                    string[] State,
                    string[] Zip,
                    string[] ApartmentNumber,
                    string[] ApartmentFloor,
                    string[] Neighborhood,

                    string[] StartDate,
                    string[] EndDate,

                    string[] MonthlyRent,
                    string[] SecurityDeposit,
                    string[] GarageFee,
                    string[] UtilitiesIncludedInRent,

                    string[] Title,
                    string[] CompanyName,
                    string[] Relationship,

                    string[] AnnualSalary,
                    string[] Bonus,
                    string[] OtherIncomeSource,
                    string[] OtherIncomeAmount,
                    string[] TotalAnnualIncome,

                    string[] BankName,
                    string[] AccountName,
                    string[] AccountType)
        {
            Init();
            Guarantor = new RealtorClient();

            this.Id = Extensions.PopFirst(ref Id);
            ContactInfo = new UserContact(ref Id, ref FirstName, ref LastName, ref Email, ref PhoneNumber);
            this.education = Extensions.PopFirst(ref Education);

            var count = Convert.ToInt32(homecount);
            if (count > 0) Homes.RemoveAt(0);
            for (int x = 0; x < count; x++)
            {
                var arr = new string[0];
                Homes.Add(new RealtorClientHome());
                Homes[x].RealtorClientId = this.Id;
                Homes[x].Id = Extensions.PopFirst(ref Id);
                Homes[x].Address = new UserAddress(ref Id, ref AddressNumber, ref Street, ref City, ref State, ref Zip, ref ApartmentNumber, ref ApartmentFloor, ref Neighborhood);
                Homes[x].Lease = new UserLease(ref Id, ref StartDate, ref EndDate, ref MonthlyRent, ref SecurityDeposit, ref GarageFee, ref UtilitiesIncludedInRent);
                Homes[x].Landlord = new Landlord(Extensions.PopFirst(ref Id),
                                new UserContact(ref Id, ref FirstName, ref LastName, ref Email, ref PhoneNumber),
                                new UserAddress(ref Id, ref AddressNumber, ref Street, ref City, ref State, ref Zip, ref ApartmentNumber, ref ApartmentFloor, ref arr));

            }

            count = Convert.ToInt32(employmentcount);
            if (count > 0) Employments.RemoveAt(0);
            for (int x = 0; x < count; x++)
            {
                var arr = new string[0];
                Employments.Add(new RealtorClientEmployment());
                Employments[x].RealtorClientId = this.Id;
                Employments[x].Id = Extensions.PopFirst(ref Id);
                Employments[x].Occupation = new UserOccupation(ref Id, ref Title, ref CompanyName, ref StartDate, ref EndDate);
                Employments[x].Address = new UserAddress(ref Id, ref AddressNumber, ref Street, ref City, ref State, ref Zip, ref ApartmentNumber, ref ApartmentFloor, ref Neighborhood);
                Employments[x].Salary = new UserSalary(ref Id, ref AnnualSalary, ref Bonus, ref OtherIncomeSource, ref OtherIncomeAmount, ref TotalAnnualIncome);
                Employments[x].Supervisor = new UserReference(Extensions.PopFirst(ref Id), Extensions.PopFirst(ref arr), Extensions.PopFirst(ref Title),
                                                        new UserContact(ref Id, ref FirstName, ref LastName, ref Email, ref PhoneNumber));
            }

            count = Convert.ToInt32(bankcount);
            if (count > 0) Banks.RemoveAt(0);
            for (int x = 0; x < count; x++)
            {
                var arr = new string[0];
                Banks.Add(new RealtorClientBank());
                Banks[x].RealtorClientId = this.Id;
                Banks[x].Id = Extensions.PopFirst(ref Id);
                //Banks[x].Name = Extensions.PopFirst(ref BankName);
                Banks[x].Account = new UserAccount(ref Id, ref AccountName, ref AccountType, ref arr);
                Banks[x].Address = new UserAddress(ref Id, ref AddressNumber, ref Street, ref City, ref State, ref Zip, ref arr, ref arr, ref arr);
            }

            count = Convert.ToInt32(personalcount);
            if (count > 0) PersonalReferences.RemoveAt(0);
            for (int x = 0; x < count; x++)
            {
                var arr = new string[0];
                PersonalReferences.Add(new RealtorClientReference());
                PersonalReferences[x].RealtorClientId = this.Id;
                PersonalReferences[x].Id = Extensions.PopFirst(ref Id);
                PersonalReferences[x].ReferenceType = 1;
                PersonalReferences[x].Reference = new UserReference(Extensions.PopFirst(ref Id), Extensions.PopFirst(ref Relationship), Extensions.PopFirst(ref arr),
                                                        new UserContact(ref Id, ref FirstName, ref LastName, ref Email, ref PhoneNumber));
            }

            count = Convert.ToInt32(professionalcount);
            if (count > 0) ProfessionalReferences.RemoveAt(0);
            for (int x = 0; x < count; x++)
            {
                var arr = new string[0];
                ProfessionalReferences.Add(new RealtorClientReference());
                ProfessionalReferences[x].RealtorClientId = this.Id;
                ProfessionalReferences[x].Id = Extensions.PopFirst(ref Id);
                ProfessionalReferences[x].ReferenceType = 2;
                ProfessionalReferences[x].Reference = new UserReference(Extensions.PopFirst(ref Id), Extensions.PopFirst(ref Relationship), Extensions.PopFirst(ref arr),
                                                        new UserContact(ref Id, ref FirstName, ref LastName, ref Email, ref PhoneNumber));
            }

            //if (Id != null) throw new System.ArgumentException();
            //if (FirstName != null) throw new System.ArgumentException();
            //if (LastName != null) throw new System.ArgumentException();
            //if (Email != null) throw new System.ArgumentException();
            //if (PhoneNumber != null) throw new System.ArgumentException();

            //if (Education != null) throw new System.ArgumentException();

            //if (AddressNumber != null) throw new System.ArgumentException();
            //if (Street != null) throw new System.ArgumentException();
            //if (City != null) throw new System.ArgumentException();
            //if (State != null) throw new System.ArgumentException();
            //if (Zip != null) throw new System.ArgumentException();
            //if (ApartmentNumber != null) throw new System.ArgumentException();
            //if (ApartmentFloor != null) throw new System.ArgumentException();
            //if (Neighborhood != null) throw new System.ArgumentException();

            //if (StartDate != null) throw new System.ArgumentException();
            //if (EndDate != null) throw new System.ArgumentException();

            //if (MonthlyRent != null) throw new System.ArgumentException();
            //if (SecurityDeposit != null) throw new System.ArgumentException();
            //if (GarageFee != null) throw new System.ArgumentException();
            //if (UtilitiesIncludedInRent != null) throw new System.ArgumentException();

            //if (Title != null) throw new System.ArgumentException();
            //if (CompanyName != null) throw new System.ArgumentException();
            //if (Relationship != null) throw new System.ArgumentException();

            //if (AnnualSalary != null) throw new System.ArgumentException();
            //if (Bonus != null) throw new System.ArgumentException();
            //if (OtherIncomeSource != null) throw new System.ArgumentException();
            //if (OtherIncomeAmount != null) throw new System.ArgumentException();
            //if (TotalAnnualIncome != null) throw new System.ArgumentException();

            //if (BankName != null) throw new System.ArgumentException();
            //if (AccountType != null) throw new System.ArgumentException();

            return;
        }

        public static void AddOrAttach(RealtorClient clientinfo, IUnitOfWork _unitOfWork, db_RealtorClients realtorClient, AspNetUser currentUser)
        {
            if (clientinfo.ContactInfo.Id > 0)
                _unitOfWork.UserContacts.Attach(clientinfo.ContactInfo.CreateObject_db(currentUser));
            else
                realtorClient.db_UserContacts = clientinfo.ContactInfo.CreateObject_db(currentUser);

            foreach (var home in clientinfo.Homes)
                if (home.Id > 0)
                    _unitOfWork.RealtorHomes.Attach(home.CreateObject_db(_unitOfWork, realtorClient, currentUser));
                else
                    realtorClient.db_RealtorHomes.Add(home.CreateObject_db(_unitOfWork, realtorClient, currentUser));

            foreach (var employment in clientinfo.Employments)
                if (employment.Id > 0)
                    _unitOfWork.RealtorEmployments.Attach(employment.CreateObject_db(_unitOfWork, realtorClient, currentUser));
                else
                    realtorClient.db_RealtorEmployments.Add(employment.CreateObject_db(_unitOfWork, realtorClient, currentUser));

            foreach (var bank in clientinfo.Banks)
                if (bank.Id > 0)
                    _unitOfWork.RealtorBanks.Attach(bank.CreateObject_db(_unitOfWork, realtorClient, currentUser));
                else
                    realtorClient.db_RealtorBanks.Add(bank.CreateObject_db(_unitOfWork, realtorClient, currentUser));

            foreach (var reference in clientinfo.PersonalReferences)
                if (reference.Id > 0)
                    _unitOfWork.RealtorReferences.Attach(reference.CreateObject_db(_unitOfWork, realtorClient, currentUser));
                else
                    realtorClient.db_RealtorReferences.Add(reference.CreateObject_db(_unitOfWork, realtorClient, currentUser));

            foreach (var reference in clientinfo.ProfessionalReferences)
                if (reference.Id > 0)
                    _unitOfWork.RealtorReferences.Attach(reference.CreateObject_db(_unitOfWork, realtorClient, currentUser));
                else
                    realtorClient.db_RealtorReferences.Add(reference.CreateObject_db(_unitOfWork, realtorClient, currentUser));
        }

        // ========================================================
        // ============ Access variables ==========================
        // ========================================================
        public RealtorClientHome ApartmentCurrent
        {
            get
            {
                var result = new RealtorClientHome();
                if (Homes != null && Homes.Count > 0) { result = Homes[0]; }
                return result;
            }
        }
        public RealtorClientHome ApartmentPrevious
        {
            get
            {
                var result = new RealtorClientHome();
                if (Homes != null && Homes.Count > 1 && Homes[1] != null) { result = Homes[1]; }
                return result;
            }
        }

        public RealtorClientEmployment EmploymentCurrent
        {
            get
            {
                var result = new RealtorClientEmployment();
                if (Employments != null && Employments.Count > 0) { result = Employments[0]; }
                return result;
            }
        }
        public RealtorClientEmployment EmploymentPrevious
        {
            get
            {
                var result = new RealtorClientEmployment();
                if (Employments != null && Employments.Count > 1 && Employments[1] != null) { result = Employments[1]; }
                return result;
            }
        }
    }
}