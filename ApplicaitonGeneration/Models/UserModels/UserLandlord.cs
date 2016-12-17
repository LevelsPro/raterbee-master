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
    public class Landlord
    {
        public int Id { get; set; }
        public int userid { get; set; }
        public UserContact ContactInfo { get; set; }
        public UserAddress Address { get; set; }

        public void Init()
        {
            ContactInfo = new UserContact();
            Address = new UserAddress();
        }

        public Landlord()
        {
            Init();
        }

        public Landlord(db_UserLandlords ul)
        {
            Init();
            if (ul != null)
            {
                Id = ul.Id;
                userid = ul.UserId;
                ContactInfo = new UserContact(ul.db_UserContacts);
                Address = new UserAddress(ul.db_UserAddresses);
            }
        }


        public db_UserLandlords CreateObject_db(IUnitOfWork unitOfWork, db_RealtorClients rc, AspNetUser currentUser)
        {
            db_UserAddresses addressobj = null;
            db_UserContacts contactobj = null;

            if (Address.Id > 0)
                unitOfWork.UserAddresses.Attach(Address.CreateObject_db(currentUser));
            else
                addressobj = unitOfWork.UserAddresses.Add(Address.CreateObject_db(currentUser));

            if (ContactInfo.Id > 0)
                unitOfWork.UserContacts.Attach(ContactInfo.CreateObject_db(currentUser));
            else
                contactobj = unitOfWork.UserContacts.Add(ContactInfo.CreateObject_db(currentUser));

            return new db_UserLandlords()
            {
                Id = this.Id,
                UserId = (currentUser != null) ? currentUser.Id : 0,
                AspNetUser = currentUser,
                ContactId = (ContactInfo.Id > 0) ? ContactInfo.Id : default(int?),
                AddressId = (Address.Id > 0) ? Address.Id : default(int?),
                db_UserContacts = contactobj,
                db_UserAddresses = addressobj
            };
        }
        public Landlord(int id, UserContact contact, UserAddress address)
        {
            Id = id;
            ContactInfo = contact;
            Address = address;
        }
        //public RealtorLandlord(
        //    ref int[] id,
        //    ref string[] FirstName,
        //    ref string[] LastName,
        //    ref string[] Email,
        //    ref string[] PhoneNumber,
        //    ref string[] AddressNumber,
        //    ref string[] Street,
        //    ref string[] City,
        //    ref string[] State,
        //    ref string[] Zip,
        //    ref string[] ApartmentNumber)
        //{
        //    var arr = new string[0];
        //    Id = Extensions.PopFirst(ref id);
        //    ContactInfo = new Contact(ref id, ref FirstName, ref LastName, ref Email, ref PhoneNumber);
        //    Address = new Address(ref id, ref AddressNumber, ref Street, ref City, ref State, ref Zip, ref ApartmentNumber, ref arr, ref arr);
        //}

        public string AddressFull
        {
            get
            {
                return Address.Street.EmptyIfNull() + ", " + Address.City.EmptyIfNull() + ", " + Address.State.EmptyIfNull() + ", " + Address.Zip.EmptyIfNull();
            }
        }
    }
}