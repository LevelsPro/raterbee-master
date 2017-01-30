using ApplicaitonGeneration;
using RaterBee;
using RaterBee.DAL.Interfaces;
using System.Linq;

namespace RaterBee.DAL.Implementations
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