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
    public class RealtorClientBank
    {
        public int Id { get; set; }
        public int RealtorClientId { get; set; }

        public string Name;
        public UserAddress Address { get; set; }
        public UserAccount Account { get; set; }
        public UserReference BankCpa { get; set; }

        public void Init()
        {
            Address = new UserAddress();
            Account = new UserAccount();
            BankCpa = new UserReference();
        }

        public RealtorClientBank()
        {
            Init();
        }

        public RealtorClientBank(db_RealtorBanks rb)
        {
            Init();
            if (rb != null)
            {
                Id = rb.Id;
                RealtorClientId = rb.ClientId;
                Address = new UserAddress(rb.db_UserAddresses);
                Account = new UserAccount(rb.db_UserAccounts);
                BankCpa = new UserReference(rb.db_UserReferences);
            }
        }

        public void DeleteObject_db(IUnitOfWork unitOfWork)
        {
            var context = unitOfWork.RealtorBanks;
            var toDelete = unitOfWork.RealtorBanks.Get(Id);
            if (toDelete != null)
                context.Remove(toDelete);
        }

        public db_RealtorBanks CreateObject_db(IUnitOfWork unitOfWork, db_RealtorClients rc, AspNetUser currentUser)
        {
            db_UserAccounts accountobj = null;
            db_UserAddresses addressobj = null;
            db_UserReferences referenceobj = null;

            if (Address.Id > 0)
                unitOfWork.UserAddresses.Attach(Address.CreateObject_db(currentUser));
            else
                addressobj = unitOfWork.UserAddresses.Add(Address.CreateObject_db(currentUser));

            if (Account.Id > 0)
                unitOfWork.UserAccounts.Attach(Account.CreateObject_db(currentUser));
            else
                accountobj = unitOfWork.UserAccounts.Add(Account.CreateObject_db(currentUser));

            //if (BankCpa.Id > 0)
            //    unitOfWork.UserReferences.Attach(BankCpa.CreateObject_db(currentUser));
            //else
            //    referenceobj = unitOfWork.UserReferences.Add(BankCpa.CreateObject_db(currentUser));

            return new db_RealtorBanks()
            {
                Id = this.Id,
                ClientId = rc.Id,
                AccountId = (Account.Id > 0) ? Account.Id : default(int?),
                AddressId = (Address.Id > 0) ? Address.Id : default(int?),
                ReferenceId = null,
                Name = Name,
                db_UserAccounts = accountobj,
                db_UserAddresses = addressobj,
                db_UserReferences = referenceobj
            };
        }

        //public RealtorClientBank(ref int[] id,
        //     ref string[] bankdetailBankName,
        //     ref string[] bankdetailBankAddress,
        //     ref string[] bankdetailBankAccountType)
        //{
        //    var arr = new string[0];
        //    Id = Extensions.PopFirst(ref id);
        //    Name = Extensions.PopFirst(ref bankdetailBankName);
        //    Address = new Address(ref id, ref arr, ref bankdetailBankAddress, ref arr, ref arr, ref arr, ref arr, ref arr, ref arr);
        //    Account = new Account(ref id, ref arr, ref bankdetailBankAccountType, ref arr);
        //}
    }
}