using ApplicaitonGeneration;
using RaterBee;
using RaterBee.DAL.Interfaces;
using System.Linq;

namespace RaterBee.DAL.Implementations
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