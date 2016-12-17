using ApplicaitonGeneration;
using ApplicationGeneration.DAL.Interfaces;
using System.Linq;

namespace ApplicationGeneration.DAL.Implementations
{
    public class RealtorHomeRepository : Repository<db_RealtorHomes>, IRealtorHomeRepository
    {
        public RealtorHomeRepository(ApplicationEntities context) : base(context)
        {
        }

        public ApplicationEntities entities
        {
            get { return Context as ApplicationEntities; }
        }
    }
    
}