using ApplicationGeneration.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationGeneration.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IAspNetUserRepository Users { get; }
        IAspNetRoleRepository Roles { get; }
        IRealtorClientRepository RealtorClients { get; }
        IRealtorHomeRepository RealtorHomes { get; }
        IRealtorEmploymentRepository RealtorEmployments { get; }
        IRealtorBankRepository RealtorBanks { get; }
        IRealtorReferenceRepository RealtorReferences { get; }
        IUserAccountRepository UserAccounts { get; }
        IUserAddressRepository UserAddresses { get; }
        IUserContactRepository UserContacts { get; }
        IUserLandlordRepository UserLandlords { get; }
        IUserLeaseRepository UserLeases { get; }
        IUserReferenceRepository UserReferences { get; }
        IUserSalaryRepository UserSalaries { get; }
        IUserOccupationRepository UserOccupations { get; }

        int Complete();
    }
}