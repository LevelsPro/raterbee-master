using ApplicaitonGeneration;
using ApplicationGeneration;
using ApplicationGeneration.DAL.Interfaces;
using System.Linq;

namespace ApplicationGeneration.DAL.Implementations
{
    public class SurveyQuestionRepository : Repository<rb_SurveyQuestions>, ISurveyQuestionRepository
    {
        public SurveyQuestionRepository(ApplicationEntities context) : base(context)
        {
        }

        public ApplicationEntities entities
        {
            get { return Context as ApplicationEntities; }
        }
    }
    
}