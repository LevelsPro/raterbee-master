using ApplicationGeneration.Models;
using iTextSharp.text.pdf;
using System;
using System.IO;


namespace ApplicaitonGeneration.Helpers
{
    public static partial class RealtorDocument
    {
        static public byte[] AkamCondominiumLeaseApplication2(string inputDir, RealtorClient applicant, RealtorClient coapplicant)
        {
            var inputFile = inputDir + "AkamCondominiumLeaseApplication2.pdf";
            var reader = new iTextSharp.text.pdf.PdfReader(inputFile);
            var stream = new MemoryStream();
            var stamper = new PdfStamper(reader, stream);

            var fields = stamper.AcroFields;
            fields.SetField("Unit #", "#1");
            fields.SetField("App Date", DateTime.Now.ToString().Split(' ')[0]);
            fields.SetField("Applicant Last Name", applicant.ContactInfo.LastName + ((coapplicant != null) ? (", " + coapplicant.ContactInfo.LastName) : ""));
            fields.SetField("App Prior Supervisors Name", applicant.EmploymentPrevious.Supervisor.ContactInfo.FullName);
            fields.SetField("App Prior Supervisors Phone", applicant.EmploymentPrevious.Supervisor.ContactInfo.PhoneNumber);
            fields.SetField("Education History", applicant.education);
            int position = 1;
            int index = 1;
            foreach (var bank in applicant.Banks)
            {
                fields.SetField("App Bank " + index + " Name", bank.Name);
                fields.SetField("App Bank " + index + " Address", bank.Address.AddressLine1);
                fields.SetField("ck" + position, "");
                fields.SetField("sv" + position, "");            // Yes is how you check these boxes
                fields.SetField("ln" + position, "");
                position++;
                index++;
            }
            //if (applicant.BankCpas.Count > 0)
            //{
            //    fields.SetField("App Stockborker CPA Name", applicant.BankCpas[0].ContactInfo.FullName);
            //    fields.SetField("App Stockbroker CPA Firm", applicant.BankCpas[0].firmname);
            //    fields.SetField("App Stockbroker CPA Address", applicant.BankCpas[0].Address.AddressLine1);
            //    fields.SetField("App Stockbroker CPA Phone", applicant.BankCpas[0].ContactInfo.phonenumber);
            //    fields.SetField("App Stockbroker CPA Email", applicant.BankCpas[0].ContactInfo.email);
            //}

            if (applicant.PersonalReferences != null)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (applicant.PersonalReferences.Count > x)
                    {
                        var reference = new UserReference();
                        reference = (applicant.PersonalReferences[x] != null) ? applicant.PersonalReferences[x].Reference : reference;
                        fields.SetField("App Personal Ref " + x + 1 + "", reference.ContactInfo.FullName);
                        fields.SetField("App Personal Ref " + x + 1 + " Phone", reference.ContactInfo.PhoneNumber);
                        fields.SetField("App Personal Ref " + x + 1 + " Email", reference.ContactInfo.Email);
                    }
                }
            }

            if (applicant.ProfessionalReferences != null)
            {
                for (int x = 0; x < 2; x++)
                {
                    if (applicant.ProfessionalReferences.Count > x)
                    {
                        var reference = new UserReference();
                        reference = (applicant.ProfessionalReferences[x] != null) ? applicant.ProfessionalReferences[x].Reference: reference;
                        fields.SetField("App Prof Ref " + x + 1, reference.ContactInfo.FullName);
                        fields.SetField("App Prof Ref " + x + 1 + " Phone", reference.ContactInfo.PhoneNumber);
                        fields.SetField("App Prof Ref " + x + 1 + " Email", reference.ContactInfo.Email);
                    }
                }
            }

            if (coapplicant != null)
            {
                fields.SetField("CoApp Prior Supervisor Name", coapplicant.EmploymentPrevious.Supervisor.ContactInfo.FullName);
                fields.SetField("CoApp Prior Suprevisor Phone", coapplicant.EmploymentPrevious.Supervisor.ContactInfo.PhoneNumber);
                fields.SetField("CoApp Education History", coapplicant.education);

                index = 1;
                foreach (var bank in coapplicant.Banks)
                {
                    fields.SetField("CoApp Bank " + index + " Name", bank.Name);
                    fields.SetField("CoApp Bank " + index + " Address", bank.Address.AddressLine1);
                    fields.SetField("ck" + position, "");
                    fields.SetField("sv" + position, "");
                    fields.SetField("ln" + position, "");
                    position++;
                    index++;
                }
                //if (coapplicant.BankCpas.Count > 0)
                //{
                //    fields.SetField("CoApp Stockborker CPA Name", coapplicant.BankCpas[0].ContactInfo.FullName);
                //    fields.SetField("CoApp Stockbroker CPA Firm", coapplicant.BankCpas[0].firmname);
                //    fields.SetField("CoApp Stockbroker CPA Address", coapplicant.BankCpas[0].Address.AddressLine1);
                //    fields.SetField("CoApp Stockbroker CPA Phone", coapplicant.BankCpas[0].ContactInfo.phonenumber);
                //    fields.SetField("CoApp Stockbroker CPA Email", coapplicant.BankCpas[0].ContactInfo.email);
                //}

                if (coapplicant.PersonalReferences != null)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if (applicant.PersonalReferences.Count > x)
                        {
                            var reference = new UserReference();
                            reference = (coapplicant.PersonalReferences[x] != null) ? coapplicant.PersonalReferences[x].Reference : reference;
                            fields.SetField("CoApp Personal Ref " + x + 1 + "", reference.ContactInfo.FullName);
                            fields.SetField("CoApp Personal Ref " + x + 1 + " Phone", reference.ContactInfo.PhoneNumber);
                            fields.SetField("CoApp Personal Ref " + x + 1 + " Email", reference.ContactInfo.Email);
                        }
                    }
                }

                if (coapplicant.ProfessionalReferences != null)
                {
                    for (int x=0; x<2;x++)
                    {
                        if (applicant.ProfessionalReferences.Count > x)
                        {
                            var reference = new UserReference();
                            reference = (coapplicant.ProfessionalReferences[x] != null) ? coapplicant.ProfessionalReferences[x].Reference : reference;
                            fields.SetField("CoApp Prof Ref " + x + 1, reference.ContactInfo.FullName);
                            fields.SetField("CoApp Prof Ref " + x + 1 + " Phone", reference.ContactInfo.PhoneNumber);
                            fields.SetField("CoApp Prof Ref " + x + 1 + " Email", reference.ContactInfo.Email);
                        }
                    }
                }
            }

            stamper.FormFlattening = true;
            stamper.Dispose();

            var result = stream.ToArray();

            reader.Close();

            return result;
        }

    }
}
