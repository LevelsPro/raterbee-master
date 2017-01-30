using ApplicaitonGeneration;
using RaterBee;
using RaterBee.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaterBee.DAL.Implementations
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