using ApplicationGeneration.Models;
using iTextSharp.text.pdf;
using System;
using System.IO;


namespace ApplicaitonGeneration.Helpers
{
    public static partial class RealtorDocument
    {
        static public byte[] AkamCondominiumLeaseApplication3(string inputDir, RealtorClient applicant, RealtorClient coapplicant)
        {

            var inputFile = inputDir + "AkamCondominiumLeaseApplication3.pdf";
            var reader = new iTextSharp.text.pdf.PdfReader(inputFile);
            var stream = new MemoryStream();
            var stamper = new PdfStamper(reader, stream);

            var fields = stamper.AcroFields;
            fields.SetField("Unit #", "#1");
            fields.SetField("App Date", DateTime.Now.ToString().Split(' ')[0]);
            fields.SetField("Applicant Last Name", applicant.ContactInfo.LastName + ((coapplicant != null) ? (", " + coapplicant.ContactInfo.LastName) : ""));
            if (applicant.ProfessionalReferences != null)
            {
                var reference = new UserReference();
                reference = (applicant.ProfessionalReferences.Count > 2 && applicant.ProfessionalReferences[2] != null) ? applicant.ProfessionalReferences[2].Reference: reference;
                fields.SetField("App Prof Ref 3", reference.ContactInfo.FullName);
                fields.SetField("App Prof Ref 3 Phone", reference.ContactInfo.PhoneNumber);
                fields.SetField("App Prof Ref 3 Email", reference.ContactInfo.Email);
            }
            if (coapplicant != null)
            {
                if (coapplicant.ProfessionalReferences != null)
                {
                    var reference = new UserReference();
                    reference = (coapplicant.ProfessionalReferences.Count > 2 && coapplicant.ProfessionalReferences[2] != null) ? coapplicant.ProfessionalReferences[2].Reference : reference;
                    fields.SetField("CoApp Prof Ref 3", reference.ContactInfo.FullName);
                    fields.SetField("CoApp Prof Ref 3 Phone", reference.ContactInfo.PhoneNumber);
                    fields.SetField("CoApp Prof Ref 3 Email", reference.ContactInfo.Email);
                }
            }
            fields.SetField("NA", "#10");
            fields.SetField("Relation to Applicant", applicant.Guarantor.relationship);
            fields.SetField("Guarantor Phone", applicant.Guarantor.ContactInfo.PhoneNumber);
            var bank = new RealtorClientBank();
            bank = (applicant.Banks != null && applicant.Banks.Count > 0 && applicant.Banks[0] != null) ? bank = applicant.Banks[0] : bank;
            fields.SetField("Guar Bank Name 1", bank.Name);
            fields.SetField("Guar Bank 1 Address", bank.Address.AddressLine1);
            fields.SetField("GrCk", "");
            fields.SetField("GRSV", "");
            fields.SetField("GRLN", "");
            bank = new RealtorClientBank();
            bank = (applicant.Banks != null && applicant.Banks.Count > 1 && applicant.Banks[1] != null) ? bank = applicant.Banks[1] : bank;
            fields.SetField("Guar Bank 2 Name", bank.Name);
            fields.SetField("Guar Bank 2 Address", bank.Address.AddressLine1);
            fields.SetField("grck", "");
            fields.SetField("grsv", "");
            fields.SetField("grln", "");
            fields.SetField("Gr ft", "");
            fields.SetField("Gr pt", "");
            fields.SetField("Gr se", "");
            fields.SetField("Gr R", "");
            fields.SetField("GR st", "");
            fields.SetField("Gr Un", "");
            fields.SetField("Guar Profession", applicant.Guarantor.EmploymentCurrent.Occupation.Occupation);
            fields.SetField("Guar Employer", applicant.Guarantor.EmploymentCurrent.Occupation.CompanyName);
            fields.SetField("Guar Empl Address 1", applicant.Guarantor.EmploymentCurrent.Address.AddressLine1);
            fields.SetField("Guar Empl Address 2", applicant.Guarantor.EmploymentCurrent.Address.AddressLine2);
            fields.SetField("Guar Emp Date", applicant.EmploymentCurrent.Occupation.Timespan.StartDate);
            fields.SetField("Guar Emp To", applicant.EmploymentCurrent.Occupation.Timespan.EndDate);
            fields.SetField("Guar Supervisor Name", applicant.EmploymentCurrent.Supervisor.ContactInfo.FullName);
            fields.SetField("Guar Supervisor Phone", applicant.EmploymentCurrent.Supervisor.ContactInfo.PhoneNumber);
            fields.SetField("Guar Base Salary", applicant.EmploymentCurrent.Salary.AnnualSalary);
            fields.SetField("Residents Known", "");
            fields.SetField("Residents Known 2", "");           // Known references in buildinng
            fields.SetField("YES", "");
            fields.SetField("NO", "");
            fields.SetField("If yes list type breed and age", "");
            fields.SetField("Y", "");
            fields.SetField("N", "");
            fields.SetField("Full", "");
            fields.SetField("PArt", "");
            fields.SetField("Pied", "");
            fields.SetField("CoApp Date", DateTime.Now.ToString().Split(' ')[0]);
            fields.SetField("Guarantor Name", applicant.Guarantor.ContactInfo.FullName);
            fields.SetField("Guarantor Date", DateTime.Now.ToString().Split(' ')[0]);
            fields.SetField("Occupant 1", "");
            fields.SetField("Occupant 3", "");
            fields.SetField("Occupant 5", "");
            fields.SetField("Occupant 2", "");
            fields.SetField("Occupant 4", "");
            fields.SetField("Occupant 6", "");

            stamper.FormFlattening = true;
            stamper.Dispose();

            var result = stream.ToArray();

            reader.Close();

            return result;
        }

    }
}
