using ApplicaitonGeneration;
using ApplicationGeneration;
using ApplicationGeneration.DAL.Interfaces;
using System.Linq;

namespace ApplicationGeneration.DAL.Implementations
{
    public class SurveyAnswerRepository : Repository<rb_SurveyAnswers>, ISurveyAnswerRepository
    {
        public SurveyAnswerRepository(ApplicationEntities context) : base(context)
        {
        }

        public ApplicationEntities entities
        {
            get { return Context as ApplicationEntities; }
        }
    }
    
}