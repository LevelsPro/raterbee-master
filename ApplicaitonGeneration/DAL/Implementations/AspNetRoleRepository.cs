using ApplicationGeneration;
using ApplicationGeneration.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationGeneration.DAL.Implementations
{
    public class AspNetRoleRepository : Repository<AspNetRole>, IAspNetRoleRepository
    {
        public AspNetRoleRepository(ApplicationEntities context) : base(context)
        {
        }

        public ApplicationEntities entities
        {
            get { return Context as ApplicationEntities; }
        }
    }
}