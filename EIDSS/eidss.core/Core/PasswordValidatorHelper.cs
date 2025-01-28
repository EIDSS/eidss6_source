using eidss.model.Core.Security;
using eidss.model.Resources;

namespace eidss.model.Core
{
    public class PasswordValidatorHelper
    {
        public static string GetPaswordError(string password, string confirmPassword, EidssSecurityManager security = null, bool specialCharactersInHtmlFormat = false)
        {
            if (security == null)
                security = new EidssSecurityManager();
            if (confirmPassword != password)
            {
                return EidssMessages.Get("msgConfirmPasswordError");
            }
            int minimumLength = security.GetIntPolicyValue("intPasswordMinimalLength", 5);
            if (password.Length < minimumLength)
            {
                return string.Format(EidssMessages.Get("msgPasswordTooShort"), minimumLength);
            }

            if (!security.ValidatePassword(password))
            {
                string pwdComplexityErrorMsgPart = security.GetStrPolicyValue("strPwdComplexityErrMsgPart", string.Empty);
                string errmsg = SecurityMessages.GetLoginErrorMessage(8);
                if (!string.IsNullOrEmpty(pwdComplexityErrorMsgPart))
                {
                    if (specialCharactersInHtmlFormat)
                    {
                        pwdComplexityErrorMsgPart = pwdComplexityErrorMsgPart.Replace("&", "&#38;");
                        pwdComplexityErrorMsgPart = pwdComplexityErrorMsgPart.Replace("'", "&#39;");
                        pwdComplexityErrorMsgPart = pwdComplexityErrorMsgPart.Replace("<", "&#60;");
                        pwdComplexityErrorMsgPart = pwdComplexityErrorMsgPart.Replace(">", "&#62;");
                        errmsg = string.Format("{0}\n{1}", errmsg, pwdComplexityErrorMsgPart);
                        errmsg = string.Format("<p>{0}</p>", errmsg.Replace("\n", "<br>"));
                    }
                    else
                    {
                        pwdComplexityErrorMsgPart = pwdComplexityErrorMsgPart.Replace("&", "&&");
                        errmsg = string.Format("{0}\n", errmsg);
                        errmsg += pwdComplexityErrorMsgPart;
 
                    }
                }
                return errmsg;
            }
            return null;
        }
        public static string ChangePassword(string organization, string login, string oldPassword, string newPassword, string confirmPassword, bool specialCharactersInHtmlFormat = false, bool skipSecurityLogInLogin = false)
        {
            var security = new EidssSecurityManager();
            var err = GetPaswordError(newPassword, confirmPassword, security, specialCharactersInHtmlFormat);
            if (err != null)
                return err;
            var errCode = security.ChangePassword(organization, login, oldPassword, newPassword, skipSecurityLogInLogin);
            if (errCode != 0)
                return SecurityMessages.GetLoginErrorMessage(errCode);
            return null;
        }

    }
}
