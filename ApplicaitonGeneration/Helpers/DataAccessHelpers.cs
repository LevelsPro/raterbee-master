using ApplicationGeneration.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicaitonGeneration.Helpers
{
    public static class DataAccessHelpers
    {

        public static void DeleteObject_db(IUnitOfWork unitOfWork, string type, int Id)
        {
            // TODO secure this...
            if (type == "Banks")
            {
                var realtorObject = unitOfWork.RealtorBanks.Get(Id);
                var realtorContext = unitOfWork.RealtorBanks;
                {
                    var nullId = realtorObject.AccountId;
                    var context = unitOfWork.UserAccounts;
                    if (nullId.HasValue) context.RemoveById(nullId.Value);
                }
                {
                    var nullId = realtorObject.AddressId;
                    var context = unitOfWork.UserAddresses;
                    if (nullId.HasValue) context.RemoveById(nullId.Value);
                }
                realtorContext.RemoveById(Id);
            }
            else if (type == "Employments")
            {
                var realtorObject = unitOfWork.RealtorEmployments.Get(Id);
                var realtorContext = unitOfWork.RealtorEmployments;
                {
                    var nullId = realtorObject.OccupationId;
                    var context = unitOfWork.UserOccupations;
                    if (nullId.HasValue) context.RemoveById(nullId.Value);
                }
                {
                    var nullId = realtorObject.AddressId;
                    var context = unitOfWork.UserAddresses;
                    if (nullId.HasValue) context.RemoveById(nullId.Value);
                }
                {
                    var nullId = realtorObject.SalaryId;
                    var context = unitOfWork.UserSalaries;
                    if (nullId.HasValue) context.RemoveById(nullId.Value);
                }
                {
                    var nullId = realtorObject.ReferenceId;
                    var context = unitOfWork.UserReferences;
                    if (nullId.HasValue) context.RemoveById(nullId.Value);
                }
                realtorContext.RemoveById(Id);
            }
            else if (type == "Homes")
            {
                var realtorObject = unitOfWork.RealtorHomes.Get(Id);
                var realtorContext = unitOfWork.RealtorHomes;
                {
                    var nullId = realtorObject.AddressId;
                    var context = unitOfWork.UserAddresses;
                    if (nullId.HasValue) context.RemoveById(nullId.Value);
                }
                {
                    var nullId = realtorObject.LeaseId;
                    var context = unitOfWork.UserLeases;
                    if (nullId.HasValue) context.RemoveById(nullId.Value);
                }
                {
                    var landlordId = realtorObject.LandlordId;
                    var landlordContext = unitOfWork.UserLandlords;
                    if (landlordId.HasValue)
                    {
                        var landlordObject = landlordContext.Get(landlordId.Value);
                        {
                            var nullId = landlordObject.AddressId;
                            var context = unitOfWork.UserAddresses;
                            if (nullId.HasValue) context.RemoveById(nullId.Value);
                        }
                        {
                            var nullId = landlordObject.ContactId;
                            var context = unitOfWork.UserContacts;
                            if (nullId.HasValue) context.RemoveById(nullId.Value);
                        }
                        landlordContext.RemoveById(landlordId.Value);
                    }
                }
                realtorContext.RemoveById(Id);
            }
            else if ((type == "PersonalReferences") || (type == "ProfessionalReferences"))
            {
                var realtorObject = unitOfWork.RealtorReferences.Get(Id);
                var realtorContext = unitOfWork.RealtorReferences;
                {
                    var nullId = realtorObject.ReferenceId;
                    var context = unitOfWork.UserReferences;
                    if (nullId.HasValue) context.RemoveById(nullId.Value);
                }
                realtorContext.RemoveById(Id);
            }
        }
    }
}