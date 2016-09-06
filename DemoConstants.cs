using System;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// A central place to keep track of all the things that need to be the same 
/// across projects in the demo but aren't really reusable code.
/// </summary>
public class DemoConstants
{
    public const string IdentityServerHostUri = "https://localhost:44310";
    public const string IdentityServerIdentityEndpoint = "/identity";
    public const string IdentityServerUri = IdentityServerHostUri + IdentityServerIdentityEndpoint;
    public const string IdentityServerAuthorizationUri = IdentityServerUri + "/connect/authorize";
    public const string IdentityServerLogoutUri = IdentityServerUri + "/connect/endsession";

    public const string ImplicitClientId = "implicitclientdemo";
    public const string ImplicitClientUri = "http://localhost:50134";
    public const string ImplicitClientRedirectUri = ImplicitClientUri + "/Account/SignInCallback";
    public const string ImplicitClientPostLogoutUri = ImplicitClientUri;


    /// <summary>
    /// For this demo we're using the example signing certificate from the IdentityServer3 samples repository
    /// and embedding it in our repository. This is a bad plan for production. 
    /// 
    /// In a production environment you should be adding this to your certificate store. 
    /// For instructions on using the Azure Websites certificate store see this:
    /// https://azure.microsoft.com/en-us/blog/using-certificates-in-azure-websites-applications/
    /// </summary>
    /// <returns></returns>
    public static X509Certificate2 GetX509Certificate2()
    {
        const string certString = "MIIDBTCCAfGgAwIBAgIQNQb+T2ncIrNA6cKvUA1GWTAJBgUrDgMCHQUAMBIxEDAOBgNVBAMTB0RldlJvb3QwHhcNMTAwMTIwMjIwMDAwWhcNMjAwMTIwMjIwMDAwWjAVMRMwEQYDVQQDEwppZHNydjN0ZXN0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqnTksBdxOiOlsmRNd+mMS2M3o1IDpK4uAr0T4/YqO3zYHAGAWTwsq4ms+NWynqY5HaB4EThNxuq2GWC5JKpO1YirOrwS97B5x9LJyHXPsdJcSikEI9BxOkl6WLQ0UzPxHdYTLpR4/O+0ILAlXw8NU4+jB4AP8Sn9YGYJ5w0fLw5YmWioXeWvocz1wHrZdJPxS8XnqHXwMUozVzQj+x6daOv5FmrHU1r9/bbp0a1GLv4BbTtSh4kMyz1hXylho0EvPg5p9YIKStbNAW9eNWvv5R8HN7PPei21AsUqxekK0oW9jnEdHewckToX7x5zULWKwwZIksll0XnVczVgy7fCFwIDAQABo1wwWjATBgNVHSUEDDAKBggrBgEFBQcDATBDBgNVHQEEPDA6gBDSFgDaV+Q2d2191r6A38tBoRQwEjEQMA4GA1UEAxMHRGV2Um9vdIIQLFk7exPNg41NRNaeNu0I9jAJBgUrDgMCHQUAA4IBAQBUnMSZxY5xosMEW6Mz4WEAjNoNv2QvqNmk23RMZGMgr516ROeWS5D3RlTNyU8FkstNCC4maDM3E0Bi4bbzW3AwrpbluqtcyMN3Pivqdxx+zKWKiORJqqLIvN8CT1fVPxxXb/e9GOdaR8eXSmB0PgNUhM4IjgNkwBbvWC9F/lzvwjlQgciR7d4GfXPYsE1vf8tmdQaY8/PtdAkExmbrb9MihdggSoGXlELrPA91Yce+fiRcKY3rQlNWVd4DOoJ/cPXsXwry8pWjNCo5JD8Q+RQ5yZEy7YPoifwemLhTdsBz3hlZr28oCGJ3kbnpW0xGvQb3VHSTVVbeei0CfXoW6iz1";
        return new X509Certificate2(Convert.FromBase64String(certString));


    }

}
