using ApplicaitonGeneration;
using RaterBee;
using RaterBee.DAL.Interfaces;
using System.Linq;

namespace RaterBee.DAL.Implementations
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