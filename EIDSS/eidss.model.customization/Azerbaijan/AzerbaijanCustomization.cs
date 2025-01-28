using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bv.common.Configuration;
using eidss.model.customization.Core;
using eidss.model.Core;
using System.ServiceModel;
using System.Net;
using eidss.model.Enums;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using bv.common.Core;

namespace eidss.model.customization.Azerbaijan
{
    class AzerbaijanCustomization : BaseCustomization
    {
        public override PersonIdentityServiceFeatures PersonIdentityServiceFeatures
        {
            get
            {
                return new PersonIdentityServiceFeatures()
                {
                    IsAvailable = true,
                    IsOnHumanCase = true,
                    IsOnPatient = false,
                    IsOnHumanCasePatient = false,
                    IsOnContactedPerson = false,
                    ButtonResId = "btTitleFindInPersonIdentityService",
                };
            }
        }

        public override Tuple<Schema.Patient, string> GetFromPersonIdentityService(Schema.Patient p)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    var ret = _getFromPersonIdentityServiceInternal(p);
                    if (ret == null)
                        return new Tuple<Schema.Patient, string>(null, "strSearchCriteriaIsWrong");
                    return new Tuple<Schema.Patient, string>(ret, null);
                }
                catch(Exception)
                {

                }
            }
            return new Tuple<Schema.Patient, string>(null, "strPersonServiceIsUnavailable");

        }


        private Schema.Patient _getFromPersonIdentityServiceInternal(Schema.Patient p)
        {
            IdentityServiceClient client = null;

            try
            {
                client = new IdentityServiceClient();
                var encryptedUser = Config.GetSetting("AzPinServiceUsr");
                var enryptedPassword = Config.GetSetting("AzPinServicePwd");
                var usr = Cryptor.Decrypt(encryptedUser, BaseSettings.CryptAlgorithmMode);
                var pwd = Cryptor.Decrypt(enryptedPassword, usr, BaseSettings.CryptAlgorithmMode);
                client.ClientCredentials.UserName.UserName = usr;
                client.ClientCredentials.UserName.Password = pwd;
                Person prs = null;
                if (p.idfsSearchMethod == 1)
                {
                    var prsList = client.GetPersonByParams(p.strFirstName, p.strLastName, p.strSecondName, p.datDateofBirth);
                    if (prsList != null && prsList.Select(i => i.Pin).Distinct().Count() == 1)
                    {
                        prs = prsList[0];
                        if (prs != null)
                        {
                            try
                            {
                                prs = client.GetPersonByPin(prs.Pin);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                else if (p.idfsSearchMethod == 2)
                {
                    IdentityServiceDocumentType docType = IdentityServiceDocumentType.Passport;
                    switch (p.idfsDocumentType)
                    {
                        case 1:
                            docType = IdentityServiceDocumentType.Passport;
                            break;
                        case 2:
                            docType = IdentityServiceDocumentType.IDCardForAdult;
                            break;
                        case 3:
                            docType = IdentityServiceDocumentType.IDcardForChild;
                            break;
                        case 4:
                            docType = IdentityServiceDocumentType.TemporaryResidencePermit;
                            break;
                        case 5:
                            docType = IdentityServiceDocumentType.PermanentResidencePermit;
                            break;
                        case 6:
                            docType = IdentityServiceDocumentType.BirthCertificate;
                            break;
                    }
                    prs = client.GetPersonByDocumentNumber(p.strDocumentNumber, docType, docType == IdentityServiceDocumentType.BirthCertificate ? p.datDocumentDate : null);
                    if (prs != null)
                    {
                        try
                        {
                            prs = client.GetPersonByPin(prs.Pin);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else if (p.idfsSearchMethod == 3)
                {
                    prs = client.GetPersonByPin(p.strPersonID);
                }

                if (prs != null)
                {
                    using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
                    {
                        FilterParams filters = new FilterParams();
                        filters.Add("strPersonID", "=", prs.Pin);
                        var items = Schema.PatientListItem.Accessor.Instance(null).SelectListT(manager, filters);
                        if (items != null && items.Count == 1)
                        {
                            var proot = Schema.Patient.Accessor.Instance(null).SelectByKey(manager, items[0].idfHumanActual);
                            p = proot;
                        }
                    }

                    p.PersonIDType = p.PersonIDTypeLookup.FirstOrDefault(i => i.idfsBaseReference != 0 && i.idfsBaseReference != (long)PersonalIDType.Unknown);
                    p.strPersonID = prs.Pin;
                    p.strFirstName = prs.Name;
                    p.strLastName = prs.SurName;
                    p.strSecondName = prs.FatherName;
                    p.datDateofBirth = prs.DateOfBirth;
                    p.Gender = p.GenderLookup.FirstOrDefault(c => c.idfsBaseReference == (prs.Sex == "M" ? (long)GenderType.Male : (prs.Sex == "F" ? (long)GenderType.Female : 0)));
                    return p;
                }
            }
            catch(FaultException fex)
            {
                switch(fex.Code.Name)
                {
                    case "2.1":
                    case "2.2":
                    case "2.3":
                        return null;
                    case "0":
                    case "1":
                    default:
                        LogError.Log("ErrorLog_PinService_", fex, stream => 
                        {
                            stream.WriteLine(String.Format("SearchMethod = {0}, DocumentType = {1}, PersonID = '{2}', FirstName = '{3}', LastName = '{4}', SecondName = '{5}', DateofBirth = '{6}'",
                                p.idfsSearchMethod, p.idfsDocumentType, p.strPersonID, p.strFirstName, p.strLastName, p.strSecondName, p.datDateofBirth.HasValue ? p.datDateofBirth.Value.ToString("yyyy-MM-ddT00:00:00") : ""));
                            stream.WriteLine(String.Format("Code = {0}, Subcode = {1}, Reason = {2}",
                                fex.Code.Name, fex.Code.SubCode == null ? "" : fex.Code.SubCode.Name, fex.Reason.ToString()));
                        });
                        if (fex.InnerException != null)
                        {
                            LogError.Log("ErrorLog_PinService_", fex.InnerException);
                        }
                        throw;
                }
            }
            catch (Exception ex)
            {
                LogError.Log("ErrorLog_PinService_", ex, stream => 
                {
                    stream.WriteLine(String.Format("SearchMethod = {0}, DocumentType = {1}, PersonID = '{2}', FirstName = '{3}', LastName = '{4}', SecondName = '{5}', DateofBirth = '{6}'",
                        p.idfsSearchMethod, p.idfsDocumentType, p.strPersonID, p.strFirstName, p.strLastName, p.strSecondName, p.datDateofBirth.HasValue ? p.datDateofBirth.Value.ToString("yyyy-MM-ddT00:00:00") : ""));
                });
                if (ex.InnerException != null)
                {
                    LogError.Log("ErrorLog_PinService_", ex.InnerException);
                }
                throw;
            }
            finally
            {
                if (client != null && client.State != System.ServiceModel.CommunicationState.Closed)
                    client.Close();
            }
            return null;
        }

        public override VisibilityFeatures VisibilityFeatures
        {
            get
            {
                return new VisibilityFeatures()
                {
                    IsPersonIDBeforeName = true,
                    IsHumanCaseSampleMainTestVisible = false,
                    IsHerdEpiControlMeasuresVisible = false,
                    IsLastNameBeforeFirstName = true,
                    IsOnlyHouseUseInAddress = false,
                };
            }
        }

    }
}
