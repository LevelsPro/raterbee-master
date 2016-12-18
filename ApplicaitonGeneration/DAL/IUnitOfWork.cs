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
        ISurveyAnswerRepository SurveyAnswers { get; }
        ISurveyQuestionRepository SurveyQuestions { get; }
        ICompanyRepository Companies { get; }
        IBeaconRepository Beacons { get; }

        int Complete();
    }
}