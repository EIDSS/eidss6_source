using System;
using System.Collections.Generic;

namespace bv.model.Model.Core
{
    public delegate string GetPermissionName(long permission);

    public interface ISecurityManager
    {
        bool AccessGranted { get; }

        int LogIn(string organization, string userName, string password,
            Action onBeforeLogin = null, Action onSuccess = null, bool skipSecurityLog = false);

        LoginResult LogInOrgOutput(string organization, string userName, string password,
            Action onBeforeLogin = null, Action onSuccess = null, bool skipSecurityLog = false);

        int ChangePassword(string organization, string userName, string currentPassword, string newPassword, bool skipSecurityLogInLogin = false);
        int SetPassword(object userID, string password);
        void LogOut();
        IDictionary<string, bool> GetPermissions(object userId);
    }
}