namespace bv.model.Model.Core
{
    public class LoginResult
    {
        public LoginResult(int resultCode, string resultOrganization)
        {
            ResultCode = resultCode;
            ResultOrganization = resultOrganization;
        }

        public int ResultCode { get; private set; }
        public string ResultOrganization { get; private set; }
    }
}