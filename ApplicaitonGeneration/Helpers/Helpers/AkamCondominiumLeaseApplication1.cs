using ApplicationGeneration.Models;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace ApplicaitonGeneration.Helpers
{
    public static partial class RealtorDocument
    {
        static public byte[] AkamCondominiumLeaseApplication1(string inputDir, RealtorClient applicant, RealtorClient coapplicant)
        {

            var inputFile = inputDir + "AkamCondominiumLeaseApplication1.pdf";
            var reader = new iTextSharp.text.pdf.PdfReader(inputFile);
            var stream = new MemoryStream();
            var stamper = new PdfStamper(reader, stream);

            var fields = stamper.AcroFields;
            fields.SetField("Building Name", "");
            fields.SetField("Unit #", "");
            fields.SetField("Building Address", "");
            fields.SetField("Monthly Rental", "");
            fields.SetField("Annual Rent", "");
            fields.SetField("Security Deposit", "");
            fields.SetField("Lease Term", "");
            fields.SetField("Lease Start Date", "");
            fields.SetField("Lease End Date", "");
            fields.SetField("Current Owner Name", "");
            fields.SetField("Current Owner Address 1", "");
            fields.SetField("Current Owner Address 2", "");
            fields.SetField("Current Owner Home Phone", "");
            fields.SetField("Applicant Home Phone", applicant.ContactInfo.PhoneNumber);
            fields.SetField("Applicant Address 1", applicant.ApartmentCurrent.Address.AddressLine1);
            fields.SetField("Applicant Address 2", applicant.ApartmentCurrent.Address.AddressLine2);
            fields.SetField("App from", applicant.ApartmentCurrent.Lease.Timespan.StartDate);             // Time at address
            fields.SetField("App To", applicant.ApartmentCurrent.Lease.Timespan.EndDate);
            //fields.SetField("LandlordAgent 1", applicant.ApartmentCurrent.Landlord.ContactInfo.FullName);
            //fields.SetField("LandlordAgent Address", applicant.ApartmentCurrent.Landlord.AddressFull);
            fields.SetField("Applicant Housing", "Live");
            fields.SetField("FT", "");
            fields.SetField("PT", "");
            fields.SetField("SE", "");          // Yes to check box these
            fields.SetField("RET", "");
            fields.SetField("ST", "");
            fields.SetField("UN", "");
            fields.SetField("Applicant Profession", applicant.EmploymentCurrent.Occupation.Occupation);
            fields.SetField("App Emp Date", applicant.EmploymentCurrent.Occupation.Timespan.StartDate);
            fields.SetField("App Emp End", applicant.EmploymentCurrent.Occupation.Timespan.EndDate);
            fields.SetField("Supervisors Name", applicant.EmploymentCurrent.Supervisor.ContactInfo.FullName);
            fields.SetField("Supervisors Phone", applicant.EmploymentCurrent.Supervisor.ContactInfo.PhoneNumber);
            fields.SetField("Annual Base Salary", applicant.EmploymentCurrent.Salary.AnnualSalary);
            fields.SetField("App Prior Employer", applicant.EmploymentCurrent.Occupation.CompanyName);
            fields.SetField("App Prior Empl Address", applicant.EmploymentCurrent.Address.AddressLine1);  // Address first line
            fields.SetField("App Prior Empl Address 2", applicant.EmploymentCurrent.Address.AddressLine2);                                        // City/State/Zip

            fields.SetField("App prior Emp", applicant.EmploymentPrevious.Occupation.Timespan.StartDate);
            fields.SetField("App Prior emp end", applicant.EmploymentPrevious.Occupation.Timespan.EndDate);


            fields.SetField("App Date", DateTime.Now.ToString().Split(' ')[0]);
            fields.SetField("Applicant Last Name", applicant.ContactInfo.LastName + ((coapplicant != null) ? (", " + coapplicant.ContactInfo.LastName) : ""));
            fields.SetField("Applicant Name", applicant.ContactInfo.FullName);
            fields.SetField("Unit Owner Cell Phone", "");
            fields.SetField("Unit Owner Email", "");
            fields.SetField("Applicant Email", applicant.ContactInfo.Email);
            fields.SetField("Applicant Employer", applicant.EmploymentCurrent.Occupation.CompanyName);
            fields.SetField("Applicant Empl Address", applicant.EmploymentCurrent.Address.AddressLine1);
            fields.SetField("Applicant Cell Phone", applicant.ContactInfo.PhoneNumber);
            fields.SetField("Applicant Empl Address 2", applicant.EmploymentCurrent.Address.AddressLine2);
            fields.SetField("Applicant Work Phone", "");
            if (coapplicant != null)
            {
                fields.SetField("CoApp Date from", coapplicant.ApartmentCurrent.Lease.Timespan.StartDate);
                fields.SetField("CoApp Date to", coapplicant.ApartmentCurrent.Lease.Timespan.EndDate);
                fields.SetField("CoApp Home Phone", coapplicant.ContactInfo.PhoneNumber);
                fields.SetField("CoApp Current Address", coapplicant.ApartmentCurrent.Address.AddressLine1);
                fields.SetField("CoApp Current Address 2", coapplicant.ApartmentCurrent.Address.AddressLine2);
                //fields.SetField("CoApp Landlord Agent", coapplicant.ApartmentCurrent.Landlord.ContactInfo.FullName);
                //fields.SetField("CoApp Landlord Agent Address", coapplicant.ApartmentCurrent.Landlord.AddressFull);
                fields.SetField("CoApp Housing", "Other");
                fields.SetField("CoApp profession", coapplicant.EmploymentCurrent.Occupation.Occupation);
                fields.SetField("Co App Employer", coapplicant.EmploymentCurrent.Occupation.CompanyName);
                fields.SetField("CoApp Empl Addess", coapplicant.EmploymentCurrent.Address.AddressLine1);
                fields.SetField("CoApp Emp Date", coapplicant.EmploymentCurrent.Occupation.Timespan.StartDate);
                fields.SetField("CoApp Emp End", coapplicant.EmploymentCurrent.Occupation.Timespan.EndDate);
                fields.SetField("CoApp Supervisor Name", coapplicant.EmploymentCurrent.Supervisor.ContactInfo.FullName);
                fields.SetField("CoApp Supervisor Phone", coapplicant.EmploymentCurrent.Supervisor.ContactInfo.PhoneNumber);
                fields.SetField("CoApp Prior Empl", coapplicant.EmploymentPrevious.Occupation.CompanyName);
                fields.SetField("CoApp Prior Emp Address", coapplicant.EmploymentPrevious.Address.AddressLine1);
                fields.SetField("CoApp Prior Emp Address 2", coapplicant.EmploymentPrevious.Address.AddressLine2);
                fields.SetField("CoApp Pr Emp Date", coapplicant.EmploymentPrevious.Occupation.Timespan.StartDate);
                fields.SetField("CoApp prior emp end", coapplicant.EmploymentPrevious.Occupation.Timespan.EndDate);

                fields.SetField("CoApp Name", coapplicant.ContactInfo.FullName);
                fields.SetField("CoApp Base Salary", coapplicant.EmploymentCurrent.Salary.AnnualSalary);
                fields.SetField("ft", "Yes");
                fields.SetField("pt", "");
                fields.SetField("se", "");
                fields.SetField("ret", "");
                fields.SetField("st", "");
                fields.SetField("un", "");
                fields.SetField("CoApp Email", coapplicant.ContactInfo.Email);
                fields.SetField("CoApp Cell Phone", coapplicant.ContactInfo.PhoneNumber);
                fields.SetField("CoApp Empl Address 2", coapplicant.EmploymentCurrent.Address.AddressLine2);
                fields.SetField("CoApp Work Phone", "");
            }

            stamper.FormFlattening = true;
            stamper.Dispose();

            var result = stream.ToArray();

            reader.Close();

            return result;
        }
    }
}
