using ApplicaitonGeneration;
using ApplicationGeneration.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace ApplicationGeneration.Models
{
    public class RealtorClientHome
    {
        public int Id { get; set; }
        public int RealtorClientId { get; set; }

        public UserAddress Address { get; set; }
        public UserLease Lease { get; set; }
        public Landlord Landlord { get; set; }

        public void Init()
        {
            Address = new UserAddress();
            Lease = new UserLease();
            Landlord = new Landlord();
        }

        public RealtorClientHome(db_RealtorHomes rh)
        {
            Init();
            if (rh != null)
            {
                Id = rh.Id;
                RealtorClientId = rh.ClientId;
                Address = new UserAddress(rh.db_UserAddresses);
                Lease = new UserLease(rh.db_UserLeases);
                Landlord = new Landlord(rh.db_UserLandlords);
            }
        }


        public void DeleteObject_db(IUnitOfWork unitOfWork)
        {
            var context = unitOfWork.RealtorHomes;
            var toDelete = unitOfWork.RealtorHomes.Get(Id);
            if (toDelete != null)
                context.Remove(toDelete);
        }


        public db_RealtorHomes CreateObject_db(IUnitOfWork unitOfWork, db_RealtorClients rc, AspNetUser currentUser)
        {
            db_UserAddresses addressobj = null;
            db_UserLeases leaseobj = null;
            db_UserLandlords landlordobj = null;

            if (Address.Id > 0)
                unitOfWork.UserAddresses.Attach(Address.CreateObject_db(currentUser));
            else
                addressobj = unitOfWork.UserAddresses.Add(Address.CreateObject_db(currentUser));

            if (Lease.Id > 0)
                unitOfWork.UserLeases.Attach(Lease.CreateObject_db(currentUser));
            else
                leaseobj = unitOfWork.UserLeases.Add(Lease.CreateObject_db(currentUser));

            if (Landlord.Id > 0)
                unitOfWork.UserLandlords.Attach(Landlord.CreateObject_db(unitOfWork, rc, currentUser));
            else
                landlordobj = unitOfWork.UserLandlords.Add(Landlord.CreateObject_db(unitOfWork, rc, currentUser));

            return new db_RealtorHomes()
            {
                Id = this.Id,
                ClientId = rc.Id,
                AddressId = (Address.Id > 0) ? Address.Id : default(int?),
                LeaseId = (Lease.Id > 0) ? Lease.Id : default(int?),
                LandlordId = (Landlord.Id > 0) ? Landlord.Id : default(int?),
                db_UserAddresses = addressobj,
                db_UserLeases = leaseobj,
                db_UserLandlords = landlordobj
            };
        }

        public RealtorClientHome()
        {
            Init();
        }
    }
}