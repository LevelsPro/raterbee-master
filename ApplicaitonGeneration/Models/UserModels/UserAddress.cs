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
    public class UserAddress
    {
        public int Id { get; set; }
        public int userid { get; set; }
        public string AddressNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ApartmentNumber { get; set; }
        public string ApartmentFloor { get; set; }
        public string Neighborhood { get; set; }

        public UserAddress() { }

        public UserAddress(ref int[] id,
            ref string[] addressnumber,
            ref string[] street,
            ref string[] city,
            ref string[] state,
            ref string[] zip,
            ref string[] apartmentnumber,
            ref string[] floor,
            ref string[] neighborhood
            )
        {
            Id = Extensions.PopFirst(ref id);
            AddressNumber = (addressnumber != null) ? Extensions.PopFirst(ref addressnumber).EmptyIfNull() : "";
            Street = (street != null) ? Extensions.PopFirst(ref street).EmptyIfNull() : "";
            City = (city != null) ? Extensions.PopFirst(ref city).EmptyIfNull() : "";
            State = (state != null) ? Extensions.PopFirst(ref state).EmptyIfNull() : "";
            Zip = (zip != null) ? Extensions.PopFirst(ref zip).EmptyIfNull() : "";
            ApartmentNumber = (apartmentnumber != null) ? Extensions.PopFirst(ref apartmentnumber).EmptyIfNull() : "";
            ApartmentFloor = (floor != null) ? Extensions.PopFirst(ref floor).EmptyIfNull() : "";
            Neighborhood = (neighborhood != null) ? Extensions.PopFirst(ref neighborhood).EmptyIfNull() : "";
        }

        public UserAddress(db_UserAddresses ua)
        {
            if (ua != null)
            {
                Id = ua.Id;
                userid = ua.UserId;
                AddressNumber = ua.AddressNumber;
                Street = ua.Street;
                City = ua.City;
                State = ua.State;
                Zip = ua.Zip;
                ApartmentNumber = ua.ApartmentNumber;
                ApartmentFloor = ua.Floor;
                Neighborhood = ua.Neighborhood;
            }
        }

        public db_UserAddresses CreateObject_db(AspNetUser currentUser)
        {
            return new db_UserAddresses() {
                Id = this.Id,
                UserId = (currentUser != null) ? currentUser.Id : 0,
                AspNetUser = currentUser,
                AddressNumber = AddressNumber,
                Street = Street,
                City = City,
                State = State,
                Zip = Zip,
                ApartmentNumber = ApartmentNumber,
                Floor = ApartmentFloor,
                Neighborhood = Neighborhood
            };
        }

        public string AddressLine1
        {
            get
            {
                var result = "";
                result = Street.EmptyIfNull();
                if (ApartmentNumber.EmptyIfNull() != "")
                {
                    if (result != "") result += ", ";
                    result += ApartmentNumber;
                }
                return result;
            }
        }
        public string AddressLine2
        {
            get
            {
                var result = "";
                result = City.EmptyIfNull();
                if (State.EmptyIfNull() != "")
                {
                    if (result != "") result += ", ";
                    result += State;
                }
                if (Zip.EmptyIfNull() != "")
                {
                    if (result != "") result += ", ";
                    result += Zip;
                }
                return result;
            }
        }
    }
}