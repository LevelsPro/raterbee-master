﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApplicaitonGeneration
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ApplicationEntities : DbContext
    {
        public ApplicationEntities()
            : base("name=ApplicationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<rb_Companies> rb_Companies { get; set; }
        public virtual DbSet<rb_SurveyAnswers> rb_SurveyAnswers { get; set; }
        public virtual DbSet<rb_SurveyBeacons> rb_SurveyBeacons { get; set; }
        public virtual DbSet<rb_SurveyQuestions> rb_SurveyQuestions { get; set; }
        public virtual DbSet<rb_SurveyBeaconQuestions> rb_SurveyBeaconQuestions { get; set; }
    }
}
