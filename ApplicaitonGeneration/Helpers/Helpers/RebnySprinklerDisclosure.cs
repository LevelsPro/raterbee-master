using iTextSharp.text.pdf;
using System;
using System.IO;
using ApplicationGeneration.Models;

namespace ApplicaitonGeneration.Helpers
{
    public static partial class RealtorDocument
    {
        static public byte[] RebnySprinklerDisclosure(string inputDir, RealtorClient applicant)
        {

            var inputFile = inputDir + "RebnySprinklerDisclosure.pdf";
            var reader = new iTextSharp.text.pdf.PdfReader(inputFile);
            var stream = new MemoryStream();
            var stamper = new PdfStamper(reader, stream);

            var fields = stamper.AcroFields;
            fields.SetField("existence or nonexistence of a Sprinkler System in the Leased Premises 1", applicant.ContactInfo.FullName);
            fields.SetField("existence or nonexistence of a Sprinkler System in the Leased Premises 2", applicant.ApartmentApplyingTo.Address.AddressLine1 + " " + applicant.ApartmentApplyingTo.Address.AddressLine2);
            fields.SetField("1", "");	// second applicant
            fields.SetField("2", ""); // 2nd entry on address line
            fields.SetField("the Leased", applicant.ApartmentApplyingTo.Address.AddressNumber);
            fields.SetField("undefined", applicant.ApartmentApplyingTo.Lease.Timespan.StartDate);
            fields.SetField("inspected was on", "");
            fields.SetField("Acknowledgment  Signatures", "");
            fields.SetField("New York State Real Property Law Article 7 Section 231a 1", applicant.ContactInfo.FirstName);
            fields.SetField("New York State Real Property Law Article 7 Section 231a 2", "");
            fields.SetField("1_2", "");		// Second tenant
            fields.SetField("2_2", "");
            //fields.SetField("1_3", applicant.ApartmentApplyingTo.Landlord.ContactInfo.FullName);
            fields.SetField("2_3", "");
            fields.SetField("Date", "");
            fields.SetField("Date_2", "");
            fields.SetField("Date_3", "");
            fields.SetField("Check Box1", "");
            fields.SetField("Check Box2", "");

            stamper.FormFlattening = true;
            stamper.Dispose();

            var result = stream.ToArray();

            reader.Close();

            return result;
        }
    }
}
