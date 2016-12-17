using iTextSharp.text.pdf;
using System;
using System.IO;
using ApplicationGeneration.Models;

namespace ApplicaitonGeneration.Helpers
{
    public static partial class RealtorDocument
    {
        static public byte[] RebnyNoticeOfIntentionToSellOrLeaseCondominiumUnit(string inputDir, RealtorClient applicant)
        {

            var inputFile = inputDir + "RebnyNoticeOfIntentionToSellOrLeaseCondominiumUnit.pdf";
            var reader = new iTextSharp.text.pdf.PdfReader(inputFile);
            var stream = new MemoryStream();
            var stamper = new PdfStamper(reader, stream);

            var fields = stamper.AcroFields;
            fields.SetField("Building Name", "");
            fields.SetField("Sell", "");
            fields.SetField("Lease", "");        // Yes to answer this
            fields.SetField("Applicant Names", applicant.ContactInfo.FirstName + " " + applicant.ContactInfo.LastName);
            fields.SetField("Purchase Price", "");
            fields.SetField("Proposed Closing Date", "");
            fields.SetField("Anticipated Occupancy Date", "");
            fields.SetField("Current CoOwner Name", "");
            fields.SetField("Owner Date", "");
            fields.SetField("CoOwn Date", "");
            fields.SetField("Unit #", "");
            fields.SetField("Monthly Rental", "");
            fields.SetField("Lease Term", applicant.ApartmentApplyingTo.Lease.Timespan.StartDate + " - " + applicant.ApartmentApplyingTo.Lease.Timespan.EndDate);
            //fields.SetField("Current Owner Name", applicant.ApartmentApplyingTo.Landlord.ContactInfo.FirstName);
            fields.SetField("Applicant Address 1", applicant.ApartmentCurrent.Address.AddressLine1);
            fields.SetField("Applicant Address 2", applicant.ApartmentCurrent.Address.AddressLine2);

            stamper.FormFlattening = true;
            stamper.Dispose();

            var result = stream.ToArray();

            reader.Close();

            return result;
        }
    }
}
