//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class rb_SurveyBeaconQuestions
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int BeaconId { get; set; }
    
        public virtual rb_SurveyBeacons rb_SurveyBeacons { get; set; }
        public virtual rb_SurveyQuestions rb_SurveyQuestions { get; set; }
    }
}
