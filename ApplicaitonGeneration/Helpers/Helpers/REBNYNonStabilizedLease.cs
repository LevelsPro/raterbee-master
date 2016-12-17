using ApplicationGeneration.Models;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace ApplicaitonGeneration.Helpers
{
    public static partial class RealtorDocument
    {
        static public byte[] GenerateREBNYNonStabalizedLease(string inputDir, RealtorClient leaseinfo)
        {
            // Article on how to get fields from pdf. The command is "pdftk 1.pdf dump_data_fields output test2.txt"
            //http://stackoverflow.com/questions/2127878/extract-pdf-form-field-names-from-a-pdf-form

            var inputFile = inputDir + "RebnyNonStabilizedLease.pdf";
            var reader = new iTextSharp.text.pdf.PdfReader(inputFile);
            var stream = new MemoryStream();
            var stamper = new PdfStamper(reader, stream);

            var fields = stamper.AcroFields;
            fields.SetField("field1", DateTime.Now.Month.ToString().Split(' ')[0]);
            fields.SetField("field2", DateTime.Now.Day.ToString().Split(' ')[0]);
            fields.SetField("field3", DateTime.Now.Year.ToString().Split(' ')[0]);
            //fields.SetField("field4", leaseinfo.ApartmentApplyingTo.Landlord.ContactInfo.FullName);
            //fields.SetField("field5", leaseinfo.ApartmentApplyingTo.Landlord.Address.AddressLine1 + leaseinfo.ApartmentApplyingTo.Address.AddressLine2);
            fields.SetField("field6", leaseinfo.ContactInfo.FullName);
            fields.SetField("field7", leaseinfo.ApartmentApplyingTo.Address.AddressLine1 + leaseinfo.ApartmentApplyingTo.Address.AddressLine2);
            fields.SetField("field8", leaseinfo.ApartmentApplyingTo.Address.AddressNumber);
            fields.SetField("field9", leaseinfo.ApartmentApplyingTo.Address.ApartmentFloor);
            fields.SetField("field10", leaseinfo.ApartmentApplyingTo.Address.AddressLine1);
            fields.SetField("field12", leaseinfo.ApartmentApplyingTo.Address.Neighborhood);
            //fields.SetField("field13", leaseinfo.leaseyears);
            //fields.SetField("field14", leaseinfo.leasemonths);
            //fields.SetField("field15", leaseinfo.leasedays);
            fields.SetField("field17", leaseinfo.ApartmentApplyingTo.Lease.Timespan.StartDate);
            fields.SetField("field18", leaseinfo.ApartmentApplyingTo.Lease.Timespan.EndDate);
            fields.SetField("field19", leaseinfo.ApartmentApplyingTo.Lease.MonthlyRent);
            fields.SetField("field20", "");
            fields.SetField("field21", leaseinfo.ApartmentCurrent.Lease.SecurityDeposit);
            //fields.SetField("field22", leaseinfo.ApartmentCurrent.Apartment.securitydepositbankname);
            //fields.SetField("field23", leaseinfo.ApartmentCurrent.Apartment.securitydepositbankaddress);
            fields.SetField("field26", leaseinfo.ApartmentCurrent.Lease.UtilitiesIncludedInRent);
            fields.SetField("Tenant1", leaseinfo.ContactInfo.FullName);
            fields.SetField("Tenant2", "");
            fields.SetField("Guarantor", leaseinfo.Guarantor.ContactInfo.FullName);
            fields.SetField("GuarantorAddress", leaseinfo.Guarantor.ApartmentCurrent.Address.AddressLine1 + " " + leaseinfo.Guarantor.ApartmentCurrent.Address.AddressLine1);
            fields.SetField("Witness", "");
            fields.SetField("Dated", DateTime.Now.ToString().Split(' ')[0]);
            //fields.SetField("Owner", "Owner");
            fields.SetField("Witness2", "");
            fields.SetField("Witness1", "");
            fields.SetField("Witness3", "");

            stamper.FormFlattening = true;
            stamper.Dispose();

            var result = stream.ToArray();
            reader.Close();

            return result;
        }

    }
}