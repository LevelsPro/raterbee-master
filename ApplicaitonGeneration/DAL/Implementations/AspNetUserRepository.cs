using ApplicaitonGeneration;
using ApplicationGeneration.DAL.Interfaces;
using System.Linq;

namespace ApplicationGeneration.DAL.Implementations
{
    public class AspNetUserRepository : Repository<AspNetUser>, IAspNetUserRepository
    {
        public AspNetUserRepository(ApplicationEntities context) : base(context)
        {
        }

        public ApplicationEntities entities
        {
            get { return Context as ApplicationEntities; }
        }

        public bool CheckIfUserExists(string email)
        {
            return entities.AspNetUsers.Any(x => x.Email == email);
        }
    }
    
}