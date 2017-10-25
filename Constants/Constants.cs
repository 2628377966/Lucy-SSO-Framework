namespace Lucy
{
    public static class Constants
    {
        public const string BaseAddress = "http://localhost:50735/core";
        public const string AuthorizeEndpoint = BaseAddress + "/connect/authorize";
        public const string LogoutEndpoint = BaseAddress + "/connect/endsession";
        public const string TokenEndpoint = BaseAddress + "/connect/token";
        public const string UserInfoEndpoint = BaseAddress + "/connect/userinfo";
        public const string IdentityTokenValidationEndpoint = BaseAddress + "/connect/identitytokenvalidation";
        public const string TokenRevocationEndpoint = BaseAddress + "/connect/revocation";

        #region MVCClients
        public const string MVCCLIENTID = "mvcClient";
        public const string MVCURL = "http://localhost:58426/";
        public const string MVCSECRET = "secret";
        #endregion

        #region MVC1
        public const string MVC1_CLIENTID = "mvc1";
        public const string MVC1_URL = "http://localhost:65074/";
        public const string MVC1_SECRET = "secret";
        #endregion

        #region PasswordClient
        public const string Password_ClientId = "carbon";
        public const string Password_ClientSecret = "21B5F798-BE55-42BC-8AA8";
        #endregion

        #region WebApiScopes
        public const string WebApi_CLIENTID = "generalApi";
        public const string WebApi_URL = "http://localhost:63297/";
        public const string WebApi_Scope = "generalApi";
        public const string WebApi_SECRET = "secret";
        #endregion

    



    }
}