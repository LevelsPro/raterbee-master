using ApplicaitonGeneration;
using ApplicationGeneration.DAL.Implementations;
using ApplicationGeneration.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationGeneration.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationEntities _context;

        public IAspNetUserRepository Users { get; private set; }
        public IAspNetRoleRepository Roles { get; private set; }
        public IRealtorClientRepository RealtorClients { get; private set; }
        public IRealtorHomeRepository RealtorHomes { get; private set; }
        public IRealtorEmploymentRepository RealtorEmployments { get; private set; }
        public IRealtorBankRepository RealtorBanks { get; private set; }
        public IRealtorReferenceRepository RealtorReferences { get; private set; }
        public IUserAccountRepository UserAccounts { get; private set; }
        public IUserAddressRepository UserAddresses { get; private set; }
        public IUserContactRepository UserContacts { get; private set; }
        public IUserLandlordRepository UserLandlords { get; private set; }
        public IUserLeaseRepository UserLeases { get; private set; }
        public IUserReferenceRepository UserReferences { get; private set; }
        public IUserSalaryRepository UserSalaries { get; private set; }
        public IUserOccupationRepository UserOccupations { get; private set; }

        public UnitOfWork(ApplicationEntities context)
        {
            _context = context;

            Users = new AspNetUserRepository(_context);
            Roles = new AspNetRoleRepository(_context);
            RealtorClients = new RealtorClientRepository(_context);
            RealtorHomes = new RealtorHomeRepository(_context);
            RealtorEmployments = new RealtorEmploymentRepository(_context);
            RealtorBanks = new RealtorBankRepository(_context);
            RealtorReferences = new RealtorReferenceRepository(_context);
            UserAccounts = new UserAccountRepository(_context);
            UserAddresses = new UserAddressRepository(_context);
            UserContacts = new UserContactRepository(_context);
            UserLandlords = new UserLandlordRepository(_context);
            UserLeases = new UserLeaseRepository(_context);
            UserReferences = new UserReferenceRepository(_context);
            UserSalaries = new UserSalaryRepository(_context);
            UserOccupations = new UserOccupationRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
