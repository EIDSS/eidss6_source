﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using bv.common.Configuration;
using bv.common.Core;
using eidss.model.Core;
using eidss.model.Core.Security;
using eidss.web.common.Utils;
using eidss.webclient.Utils;
using bv.common.Resources;

namespace eidss.webclient.Models
{
    public class Login
    {
        public Login()
        {
            //Organization = "test_org";
            //UserName = "test_admin";
        }

        [LocalizedDisplayName("Organization")]
        public string Organization { get; set; }

        [LocalizedDisplayName("Login")]
        public string UserName { get; set; }

        [LocalizedDisplayName("Password")]
        public string Password { get; set; }

        [LocalizedDisplayName("PreferredLanguage")]
        public string LanguagePreference { get; set; }

        [LocalizedDisplayName("OldPassword")]
        public string OldPassword { get; set; }

        [LocalizedDisplayName("NewPassword")]
        public string NewPassword { get; set; }

        [LocalizedDisplayName("ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        public bool ShowParallelSessionsMessage { get; set; }

        public bool RemoveParallelSessions { get; set; }

        public bool KeepParallelSessions { get; set; }

        public bool IsChangePassword { get; set; }


        //EDS
        public string Algorithm { get; set; }
        public string Alias { get; set; }
        public string AuthorityKeyIdentifier { get; set; }
        public string CertNotAfter { get; set; }
        public string CertNotBefore { get; set; }
        public string IssuerCn { get; set; }
        public string IssuerDn { get; set; }
        public string KeyId { get; set; }
        public string Pem { get; set; }
        public string SerialNumber { get; set; }
        public string SubjectCn { get; set; }
        public string SubjectDn { get; set; }

        public List<SelectListItem> SupportedLanguages
        {
            get
            {
                string[] langs = Config.GetSetting("SupportedLanguages", Localizer.SupportedLanguages.Keys.Aggregate("", (s, i) => s + "," + i)).Split(',');
                return new List<SelectListItem>(
                    Localizer.SupportedLanguages.Keys.Intersect(langs).Select(c =>
                        new SelectListItem { Text = Localizer.GetMenuLanguageName(c), Value = CustomCultureHelper.GetCustomCultureName(c) }
                        )
                    );
            }
        }

        public string ErrorMessage { get; set; }

        public bool ValidateSecurityPolicy()
        {
            ErrorMessage = PasswordValidatorHelper.GetPaswordError(NewPassword, ConfirmPassword,null,true);
            return ErrorMessage == null;
        }

        public bool ChangePassword()
        {
            ErrorMessage = PasswordValidatorHelper.ChangePassword(Organization, UserName, OldPassword, NewPassword,
                                                                 ConfirmPassword, true);
            return ErrorMessage == null;
        }

        /*public bool ValidateOnChange()
        {
            if (NewPassword != ConfirmPassword)
            {
                ErrorMessage = Translator.GetMessageString("msgConfirmPasswordError");
                return false;
            }
            var security = new EidssSecurityManager();
            var result = security.LogIn(Organization, UserName, OldPassword);
            if (result == 0) //authorized
            {                
                return true;
            }
            else
            {
                ErrorMessage = SecurityMessages.GetLoginErrorMessage(result);
                return false;
            }            
        }*/

        public bool Authorize()
        {
            var security = new EidssSecurityManager();
            var curClientID = EidssUserContext.ClientID;
            if (!string.IsNullOrEmpty(curClientID))
                security.DeleteLocalConnectionContext(curClientID);
            var result = security.LogIn(Organization, UserName, Password,
                () =>
                    {
                        var clientId = Guid.NewGuid().ToString();

                        if (HttpContext.Current.Request.Cookies["ClientID"] != null)
                            HttpContext.Current.Request.Cookies["ClientID"].Value = clientId;

                        if (HttpContext.Current.Response.Cookies["ClientID"] != null)
                            HttpContext.Current.Response.Cookies["ClientID"].Value = clientId;
                        else
                            HttpContext.Current.Response.Cookies.Add(new HttpCookie("ClientID", clientId));
                    },
                () =>
                    {
                        EidssUserContext.CurrentLanguage = Cultures.GetLanguageAbbreviation(LanguagePreference);
                        System.Web.Security.FormsAuthentication.SetAuthCookie(UserName, false);
                        /*
                        var authCookie = FormsAuthentication.GetAuthCookie(UserName, false);
                        // Get the FormsAuthenticationTicket out of the encrypted cookie
                        var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                        // Create a new FormsAuthenticationTicket that includes our custom User Data
                        var newTicket = new FormsAuthenticationTicket(
                            ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent,
                            Guid.NewGuid().ToString()
                            );
                        // Update the authCookie's Value to use the encrypted version of newTicket
                        //authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                        // Manually add the authCookie to the Cookies collection

                        //while (HttpContext.Current.Response.Cookies.AllKeys.Any(c => c == "formsauth"))
                        //    HttpContext.Current.Response.Cookies.Remove("formsauth");
                        //HttpContext.Current.Response.Cookies.Add(authCookie);
                        HttpContext.Current.Response.Cookies["formsauth"].Value = FormsAuthentication.Encrypt(newTicket);*/
                    }, EidssSiteContext.Instance.EnableDisconnection);
            switch (result)
            {
                case 0:
                    return true;
                case 6:
                    int lockInMinutes = security.GetAccountLockTimeout(this.Organization, this.UserName);
                    string err = BvMessages.Get("ErrLoginIsLocked", "You have exceeded the number of incorrect login attempts. Please try again in {0} minutes.");
                    ErrorMessage = string.Format(err, lockInMinutes);
                    return false;
                default:
                    ErrorMessage = SecurityMessages.GetLoginErrorMessage(result);
                    return false;
            }

        }

        public bool AuthorizeInternal(out int retCode)
        {
            retCode = 0;
            var security = new EidssSecurityManager();
            var curClientID = EidssUserContext.ClientID;
            if (!string.IsNullOrEmpty(curClientID))
               security.DeleteLocalConnectionContext(curClientID);

            var result = security.LogInInternal(Organization, UserName, Password,
                () =>
                {
                    var clientId = Guid.NewGuid().ToString();

                    if (HttpContext.Current.Request.Cookies["ClientID"] != null)
                        HttpContext.Current.Request.Cookies["ClientID"].Value = clientId;

                    if (HttpContext.Current.Response.Cookies["ClientID"] != null)
                        HttpContext.Current.Response.Cookies["ClientID"].Value = clientId;
                    else
                        HttpContext.Current.Response.Cookies.Add(new HttpCookie("ClientID", clientId));
                },
                () =>
                {
                    EidssUserContext.CurrentLanguage = Cultures.GetLanguageAbbreviation(LanguagePreference);
                    System.Web.Security.FormsAuthentication.SetAuthCookie(UserName, false);
                }
                );
            retCode = result;
            switch (result)
            {
                case 0:
                    return true;
                case 6:
                    int lockInMinutes = security.GetAccountLockTimeout(this.Organization, this.UserName);
                    string err = BvMessages.Get("ErrLoginIsLocked", "You have exceeded the number of incorrect login attempts. Please try again in {0} minutes.");
                    ErrorMessage = string.Format(err, lockInMinutes);
                    return false;
                default:
                    ErrorMessage = SecurityMessages.GetLoginErrorMessage(result);
                    return false;
            }
        }

        public void LogOut()
        {
            var security = new EidssSecurityManager();
            security.LogOut();
        }
    }
}
