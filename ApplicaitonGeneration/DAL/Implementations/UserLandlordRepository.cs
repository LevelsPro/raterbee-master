﻿using ApplicaitonGeneration;
using ApplicationGeneration.DAL.Interfaces;
using System.Linq;

namespace ApplicationGeneration.DAL.Implementations
{
    public class UserLandlordRepository : Repository<db_UserLandlords>, IUserLandlordRepository
    {
        public UserLandlordRepository(ApplicationEntities context) : base(context)
        {
        }

        public ApplicationEntities entities
        {
            get { return Context as ApplicationEntities; }
        }
    }

}