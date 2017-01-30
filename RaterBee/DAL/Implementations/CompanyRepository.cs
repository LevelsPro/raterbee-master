using ApplicaitonGeneration;
using RaterBee;
using RaterBee.DAL.Interfaces;
using System.Linq;

namespace RaterBee.DAL.Implementations
{
    public class CompanyRepository : Repository<rb_Companies>, ICompanyRepository
    {
        public CompanyRepository(ApplicationEntities context) : base(context)
        {
        }

        public ApplicationEntities entities
        {
            get { return Context as ApplicationEntities; }
        }
    }
    
}