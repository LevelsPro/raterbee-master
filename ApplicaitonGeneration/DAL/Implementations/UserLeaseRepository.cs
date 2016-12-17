using ApplicaitonGeneration;
using ApplicationGeneration.DAL.Interfaces;
using System.Linq;

namespace ApplicationGeneration.DAL.Implementations
{
    public class UserLeaseRepository : Repository<db_UserLeases>, IUserLeaseRepository
    {
        public UserLeaseRepository(ApplicationEntities context) : base(context)
        {
        }

        public ApplicationEntities entities
        {
            get { return Context as ApplicationEntities; }
        }
    }

}