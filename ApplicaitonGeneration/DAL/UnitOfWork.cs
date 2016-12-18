using ApplicationGeneration;
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
        public ISurveyAnswerRepository SurveyAnswers { get; }
        public ISurveyQuestionRepository SurveyQuestions { get; }
        public ICompanyRepository Companies { get; }
        public IBeaconRepository Beacons { get; }

        public UnitOfWork(ApplicationEntities context)
        {
            _context = context;

            Users = new AspNetUserRepository(_context);
            Roles = new AspNetRoleRepository(_context);
            Companies = new CompanyRepository(_context);
            Beacons = new BeaconRepository(_context);
            SurveyAnswers = new SurveyAnswerRepository(_context);
            SurveyQuestions = new SurveyQuestionRepository(_context);
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
