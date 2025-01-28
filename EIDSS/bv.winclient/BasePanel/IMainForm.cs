using System.Windows.Forms;

namespace bv.winclient.BasePanel
{
    public interface IMainForm
    {
        void RefreshLayout();

        void LogOutRedirectToLogin(string errId, int? securityEv, bool showErrorForm);
    }
}
