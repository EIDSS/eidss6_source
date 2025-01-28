using bv.common.Configuration;
using bv.common.Core;
using bv.model.BLToolkit;
using bv.model.Model.Core;
using eidss.model.Core;
using eidss.model.customization.Core;
using eidss.model.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Text;

namespace eidss.model.customization.Georgia
{
    class GeorgiaCustomization : BaseCustomization
    {
        public override PersonIdentityServiceFeatures PersonIdentityServiceFeatures
        {
            get
            {
                return new PersonIdentityServiceFeatures()
                {
                    IsAvailable = true,
                    IsOnHumanCase = false,
                    IsOnPatient = false,
                    IsOnHumanCasePatient = true,
                    IsOnContactedPerson = true,
                    ButtonResId = "btTitleFindInPersonIdentityServiceGG",
                };
            }
        }

        public override VisibilityFeatures VisibilityFeatures
        {
            get
            {
                return new VisibilityFeatures()
                {
                    IsPersonIDBeforeName = true,
                    IsHumanCaseSampleMainTestVisible = true,
                    IsHerdEpiControlMeasuresVisible = true,
                    IsLastNameBeforeFirstName = true,
                    IsOnlyHouseUseInAddress = false,
                };
            }
        }

        private string PinVerificationSuffix
        {
            get { return BaseSettings.GGPinServiceVerification ? string.Empty : "NoVerification"; }
        }

        public override Tuple<Schema.Patient, string> GetFromPersonIdentityService(Schema.Patient p)
        {
            var strPinVerificationSuffix = PinVerificationSuffix;
            try
            {
                if (p.strPersonID.Length != 11 || p.strPersonID.Any(c => !Char.IsDigit(c)))
                    return new Tuple<Schema.Patient, string>(null, string.Format("strPinWrongFormat{0}", strPinVerificationSuffix));

                if (!p.datDateofBirth.HasValue)
                    return new Tuple<Schema.Patient, string>(null, "strPinDOB");

                var ret = _getFromPersonIdentityServiceInternal(p);
                return ret;
            }
            catch (Exception ex)
            {
                string e = ex.Message;
            }

            return new Tuple<Schema.Patient, string>(null, string.Format("strPinNotResponding{0}", strPinVerificationSuffix));

        }

        private Tuple<Schema.Patient, string> _getFromPersonIdentityServiceInternal(Schema.Patient p)
        {
            var strPinVerificationSuffix = PinVerificationSuffix;
            string pin = p.strPersonID;
            string form = p.Parent is Schema.HumanCase ? "H02" : "H04";
            string caseID = p.Parent is Schema.HumanCase ?
                (p.Parent as Schema.HumanCase).strCaseID :
                (p.Parent is Schema.ContactedCasePerson ?
                    ((p.Parent as Schema.ContactedCasePerson).Parent as Schema.HumanCase).strCaseID :
                    "");
            DateTime? dtResponse = null;
            string resultCode = "";

            using (var handler = new HttpClientHandler())
            using (var httpClient = new HttpClient(handler))
            {
                //httpClient.BaseAddress = new Uri(BaseSettings.GgPinServiceBaseUrl);
                GeorgiaPIN.Client client = new GeorgiaPIN.Client(httpClient);
                try
                {
                    client.BaseUrl = BaseSettings.GgPinServiceBaseUrl;
                    string codedUsername = Config.GetSetting("GgPinServiceUsr");
                    string codedPassword = Config.GetSetting("GgPinServicePwd");
                    string username = Cryptor.Decrypt(codedUsername, BaseSettings.CryptAlgorithmMode);
                    string password = Cryptor.Decrypt(codedPassword, username, BaseSettings.CryptAlgorithmMode);

                    var token = client.LoginAsync(username, password, null).GetAwaiter().GetResult();
                    if ((token == null) || (token == default(System.Guid)))
                    {
                        throw new Exception("PIN Service unreachable");
                    }

                    GeorgiaPIN.PersonDataModel person = client.GetPersonDataAsync(p.strPersonID, p.datDateofBirth.Value.Year, token).GetAwaiter().GetResult();
                    if (person == null)
                    {
                        resultCode = "NO_RECORDS_FOUND";
                        return new Tuple<Schema.Patient, string>(null, string.Format("strPinNoRecordsFound{0}", strPinVerificationSuffix));
                    }

                    dtResponse = DateTime.Now;
                    resultCode = "OK";

                    using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
                        {
                            FilterParams filters = new FilterParams();
                            filters.Add("strPersonID", "=", p.strPersonID);
                            var items = Schema.PatientListItem.Accessor.Instance(null).SelectListT(manager, filters);
                            if (items != null && items.Count == 1)
                            {
                                var proot = Schema.Patient.Accessor.Instance(null).SelectByKey(manager, items[0].idfHumanActual);
                                p = proot;
                            }
                        }

                    p.PersonIDType = p.PersonIDTypeLookup.FirstOrDefault(i => i.idfsBaseReference == (long)PersonalIDType.PIN_GG);
                    p.strPersonID = person.PrivateNumber;
                    p.strFirstName = person.FirstName;
                    p.strLastName = person.LastName;
                    if (!string.IsNullOrEmpty(person.GenderID))
                    {
                        p.Gender = p.GenderLookup.FirstOrDefault(c => c.idfsBaseReference == (person.GenderID.Equals("1") ? (long)GenderType.Male : (person.GenderID.Equals("2") ? (long)GenderType.Female : 0)));
                    }

                    p.bPINMode = true;
                    DateTime xx;
                    if ((person.BirthDate != null) && (DateTime.TryParseExact(person.BirthDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out xx)))
                    {
                        p.datDateofBirth = xx;
                    }

                    return new Tuple<Schema.Patient, string>(p, null);

                }
                catch (Exception ex)
                {
                    LogError.Log("ErrorLog_PinService_", ex, stream =>
                    {
                        stream.WriteLine(String.Format("PIN = {0}, Form = {1}, CaseID = {2}, UserID = {3}, UserName = {4}, UserOrganization = {5}, EIDSSDateTime = {6}, PINServiceDateTime = {7}, Result = {8}",
                            pin, form, caseID,
                            EidssUserContext.Instance.CurrentUser.LoginName,
                            EidssUserContext.Instance.CurrentUser.FullName,
                            EidssUserContext.Instance.CurrentUser.OrganizationEng,
                            DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                            dtResponse.HasValue ? dtResponse.Value.ToString("yyyy-MM-ddTHH:mm:ss") : "",
                            resultCode
                            ));
                    });
                    resultCode = "Exception:" + ex.Message;
                    throw;
                }
                finally
                {
                    if (client != null)
                    {
                        client = null;
                    }

                    LogError.Log("Log_PinService_", null, stream =>
                    {
                        stream.WriteLine(String.Format("PIN = {0}, form = {1}, CaseID = {2}, UserID = {3}, UserName = {4}, UserOrganization = {5}, EIDSSDateTime = {6}, PINServiceDateTime = {7}, Result = {8}",
                            pin, form, caseID,
                            EidssUserContext.Instance.CurrentUser.LoginName,
                            EidssUserContext.Instance.CurrentUser.FullName,
                            EidssUserContext.Instance.CurrentUser.OrganizationEng,
                            DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                            dtResponse.HasValue ? dtResponse.Value.ToString("yyyy-MM-ddTHH:mm:ss") : "",
                            resultCode
                            ));
                    }, "{0}{3}.txt");
                }
            }
            //implementation for previous service
            //Georgia.CommonDataWebSoapClient client = new Georgia.CommonDataWebSoapClient();
            //try
            //{
            //    string codedUsername = Config.GetSetting("GgPinServiceUsr");
            //    string codedPassword = Config.GetSetting("GgPinServicePwd");
            //    string username = Cryptor.Decrypt(codedUsername, BaseSettings.CryptAlgorithmMode);
            //    string password = Cryptor.Decrypt(codedPassword, username, BaseSettings.CryptAlgorithmMode);

            //    var token = client.Login(username, password);
            //    if (token == null)
            //    {
            //        throw new Exception("PIN Service unreachable");
            //    }

            //    var person = client.GetPersonInfoEx(token.Value, Guid.Empty, p.strPersonID, p.datDateofBirth.Value.Year);
            //    if (person == null)
            //    {
            //        resultCode = "NO_RECORDS_FOUND";
            //        return new Tuple<Schema.Patient, string>(null, string.Format("strPinNoRecordsFound{0}", strPinVerificationSuffix));
            //    }

            //    dtResponse = DateTime.Now;
            //    resultCode = "OK";

            //    using (DbManagerProxy manager = DbManagerFactory.Factory.Create(ModelUserContext.Instance))
            //        {
            //            FilterParams filters = new FilterParams();
            //            filters.Add("strPersonID", "=", p.strPersonID);
            //            var items = Schema.PatientListItem.Accessor.Instance(null).SelectListT(manager, filters);
            //            if (items != null && items.Count == 1)
            //            {
            //                var proot = Schema.Patient.Accessor.Instance(null).SelectByKey(manager, items[0].idfHumanActual);
            //                p = proot;
            //            }
            //        }

            //    p.PersonIDType = p.PersonIDTypeLookup.FirstOrDefault(i => i.idfsBaseReference == (long)PersonalIDType.PIN_GG);
            //    p.strPersonID = person.PrivateNumber;
            //    p.strFirstName = person.FirstName;
            //    p.strLastName = person.LastName;
            //    p.Gender = p.GenderLookup.FirstOrDefault(c => c.idfsBaseReference == (person.GenderID.Equals(PersonGendersEnum.Male) ? (long)GenderType.Male : (person.GenderID.Equals(PersonGendersEnum.Female) ? (long)GenderType.Female : 0)));

            //    p.bPINMode = true;
            //    DateTime xx;
            //    if ((person.BirthDate != null) && (DateTime.TryParseExact(person.BirthDate.ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out xx)))
            //    {
            //        p.datDateofBirth = xx; //person.BirthDate;
            //    }

            //    return new Tuple<Schema.Patient, string>(p, null);
            //}
            //catch (Exception ex)
            //{
            //    LogError.Log("ErrorLog_PinService_", ex, stream =>
            //    {
            //        stream.WriteLine(String.Format("PIN = {0}, Form = {1}, CaseID = {2}, UserID = {3}, UserName = {4}, UserOrganization = {5}, EIDSSDateTime = {6}, PINServiceDateTime = {7}, Result = {8}",
            //            pin, form, caseID,
            //            EidssUserContext.Instance.CurrentUser.LoginName,
            //            EidssUserContext.Instance.CurrentUser.FullName,
            //            EidssUserContext.Instance.CurrentUser.OrganizationEng,
            //            DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
            //            dtResponse.HasValue ? dtResponse.Value.ToString("yyyy-MM-ddTHH:mm:ss") : "",
            //            resultCode
            //            ));
            //    });
            //    resultCode = "Exception:" + ex.Message;
            //    throw;
            //}
            //finally
            //{
            //    if (client != null && client.State != System.ServiceModel.CommunicationState.Closed)
            //        client.Close();

            //    LogError.Log("Log_PinService_", null, stream =>
            //    {
            //        stream.WriteLine(String.Format("PIN = {0}, form = {1}, CaseID = {2}, UserID = {3}, UserName = {4}, UserOrganization = {5}, EIDSSDateTime = {6}, PINServiceDateTime = {7}, Result = {8}",
            //            pin, form, caseID,
            //            EidssUserContext.Instance.CurrentUser.LoginName,
            //            EidssUserContext.Instance.CurrentUser.FullName,
            //            EidssUserContext.Instance.CurrentUser.OrganizationEng,
            //            DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
            //            dtResponse.HasValue ? dtResponse.Value.ToString("yyyy-MM-ddTHH:mm:ss") : "",
            //            resultCode
            //            ));
            //    }, "{0}{3}.txt");
            //}
        }
    }
}
