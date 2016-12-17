using ApplicaitonGeneration;
using ApplicaitonGeneration.Helpers;
using ApplicationGeneration.DAL;
using ApplicationGeneration.Models;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using iTextSharp.text.pdf;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ApplicationGeneration.Controllers
{
    [Authorize]
    public class RealtorDocumentController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public RealtorDocumentController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationEntities());
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(new RealtorClient());
        }

        public ActionResult Edit(int id)
        {
            var clientinfo = new RealtorClient(_unitOfWork.RealtorClients.Get(id));

            return View(clientinfo);
        }

        public ActionResult DeleteObject(int id, string type)
        {
            // TODO secure this...
            DataAccessHelpers.DeleteObject_db(_unitOfWork, type, id);
            _unitOfWork.Complete();

            return Json(new { Result = "Successfull Deleted" });
        }

        [HttpPost]
        public ActionResult New(
                    string actiontype,
                    string documenttypes,
                    RealtorClient realtorclient

            )
        {
            return handleClientInfo(actiontype, documenttypes, realtorclient);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult New(
                    int[] Id,
                    string actiontype,
                    string documenttypes,

                    string clientcount,
                    string homecount,
                    string employmentcount,
                    string bankcount,
                    string personalcount,
                    string professionalcount,

                    string[] FirstName,
                    string[] LastName,
                    string[] Email,
                    string[] PhoneNumber,

                    string[] Education,

                    string[] AddressNumber,
                    string[] Street,
                    string[] City,
                    string[] State,
                    string[] Zip,
                    string[] ApartmentNumber,
                    string[] ApartmentFloor,
                    string[] Neighborhood,

                    string[] StartDate,
                    string[] EndDate,

                    string[] MonthlyRent,
                    string[] SecurityDeposit,
                    string[] GarageFee,
                    string[] UtilitiesIncludedInRent,

                    string[] Title,
                    string[] CompanyName,
                    string[] Relationship,

                    string[] AnnualSalary,
                    string[] Bonus,
                    string[] OtherIncomeSource,
                    string[] OtherIncomeAmount,
                    string[] TotalAnnualIncome,

                    string[] BankName,
                    string[] AccountName,
                    string[] AccountType
            )
        {
            // TODO Check these variables
            var clientinfo = new RealtorClient(
                    Id,
                    clientcount,
                    homecount,
                    employmentcount,
                    bankcount,
                    personalcount,
                    professionalcount,

                     FirstName,
                     LastName,
                     Email,
                     PhoneNumber,

                     Education,

                     AddressNumber,
                     Street,
                     City,
                     State,
                     Zip,
                     ApartmentNumber,
                     ApartmentFloor,
                     Neighborhood,

                     StartDate,
                     EndDate,

                     MonthlyRent,
                     SecurityDeposit,
                     GarageFee,
                     UtilitiesIncludedInRent,

                     Title,
                     CompanyName,
                     Relationship,

                     AnnualSalary,
                     Bonus,
                     OtherIncomeSource,
                     OtherIncomeAmount,
                     TotalAnnualIncome,

                     BankName,
                     AccountName,
                     AccountType
                );
            return handleClientInfo(actiontype, documenttypes, clientinfo);
        }

        public ActionResult handleClientInfo(string actiontype, string documenttypes, RealtorClient clientinfo){ 
            if (actiontype == "save")
            {
                var currentUserId = 0;
                var currentUser = _unitOfWork.Users.Get(User.Identity.GetUserId<int>());
                if (currentUser != null)
                    currentUserId = currentUser.Id;

                var realtorClient = new db_RealtorClients();
                if (clientinfo.Id > 0)
                {
                    realtorClient.Id = clientinfo.Id;
                    var currentRealtorClient = _unitOfWork.RealtorClients.Find(rc => rc.Id == clientinfo.Id && rc.UserId == currentUserId).FirstOrDefault();
                    if (currentRealtorClient == null)
                    {
                        // Test this... catch people tyring to update false info
                    }
                    else
                    {
                        realtorClient = currentRealtorClient;
                        _unitOfWork.RealtorClients.Attach(realtorClient);
                    }
                }
                else
                {
                    _unitOfWork.RealtorClients.Add(realtorClient);
                    realtorClient.UserId = currentUserId;
                    realtorClient.AspNetUser = currentUser;
                }
                //realtorClient.Education

                realtorClient.UserId = currentUserId;
                realtorClient.ContactId = clientinfo.ContactInfo.Id;

                RealtorClient.AddOrAttach(clientinfo, _unitOfWork, realtorClient, currentUser);
                _unitOfWork.Complete();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                var inputdir = ControllerContext.HttpContext.Server.MapPath("~/Helpers/PDFs/");
                var pdfbytes = new byte[] { };
                var resultFilename = "";
                var documentlist = documenttypes.Split(',');

                var pagebytes = new List<byte[]>();
                foreach (var documenttype in documentlist)
                {
                    if (documenttype == "RebnyNonStabilizedLease")
                    {
                        pagebytes.Add(RealtorDocument.GenerateREBNYNonStabalizedLease(inputdir, clientinfo));
                    }
                    else if (documenttype == "322West57LeaseApplication")
                    {
                        pagebytes.Add(RealtorDocument.Generate322West57LeaseApplication(inputdir, clientinfo, clientinfo));
                    }
                    else if (documenttype == "RebnySprinkleDisclosure")
                    {
                        pagebytes.Add(RealtorDocument.RebnySprinklerDisclosure(inputdir, clientinfo));
                    }
                    else if (documenttype == "RebnyNoticeOfIntentionToSellOrLease")
                    {
                        pagebytes.Add(RealtorDocument.RebnyNoticeOfIntentionToSellOrLeaseCondominiumUnit(inputdir, clientinfo));
                    }
                    else if (documenttype == "AkamCondominiumLeaseApplication")
                    {

                        pagebytes.Add(RealtorDocument.AkamCondominiumLeaseApplication1(inputdir, clientinfo, null));
                        pagebytes.Add(RealtorDocument.AkamCondominiumLeaseApplication2(inputdir, clientinfo, null));
                        pagebytes.Add(RealtorDocument.AkamCondominiumLeaseApplication3(inputdir, clientinfo, null));
                    }
                }
                pdfbytes = Combine(pagebytes);
                resultFilename = "ApplicationGeneration-" + clientinfo.ContactInfo.FirstName; //+ "-" + clientinfo.ApartmentApplyingTo.Landlord.ContactInfo.FullName + ".pdf";
                return File(pdfbytes, "application/pdf", resultFilename);
            }
        }

        private byte[] Combine(List<byte[]> pagebytes)
        {
            byte[] result;
            using (MemoryStream ms = new MemoryStream())
            {
                using (var document = new iTextSharp.text.Document())
                {
                    using (PdfCopy copy = new PdfCopy(document, ms))
                    {
                        document.Open();

                        for (int i = 0; i < pagebytes.Count; ++i)
                        {
                            PdfReader reader = new PdfReader(pagebytes[i]);
                            // loop over the pages in that document
                            int n = reader.NumberOfPages;
                            for (int page = 0; page < n;)
                            {
                                copy.AddPage(copy.GetImportedPage(reader, ++page));
                            }
                        }
                    }
                }
                result = ms.ToArray();
            }
            return result;
        }
    }
}
