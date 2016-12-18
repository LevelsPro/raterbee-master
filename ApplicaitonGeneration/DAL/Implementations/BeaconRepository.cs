using ApplicationGeneration;
using ApplicationGeneration.DAL.Interfaces;
using System.Linq;

namespace ApplicationGeneration.DAL.Implementations
{
    public class BeaconRepository : Repository<rb_SurveyBeacons>, IBeaconRepository
    {
        public BeaconRepository(ApplicationEntities context) : base(context)
        {
        }

        public ApplicationEntities entities
        {
            get { return Context as ApplicationEntities; }
        }
    }
    
}