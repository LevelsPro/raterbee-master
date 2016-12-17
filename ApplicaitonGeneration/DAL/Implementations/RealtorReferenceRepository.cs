using ApplicaitonGeneration;
using ApplicationGeneration.DAL.Interfaces;
using System.Linq;

namespace ApplicationGeneration.DAL.Implementations
{
    public class RealtorReferenceRepository : Repository<db_RealtorReferences>, IRealtorReferenceRepository
    {
        public RealtorReferenceRepository(ApplicationEntities context) : base(context)
        {
        }

        public ApplicationEntities entities
        {
            get { return Context as ApplicationEntities; }
        }
    }
    
}