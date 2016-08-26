using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Demo.AuthService.Web.MoveMe
{
    internal static class Cert
    {
        /// <summary>
        /// For this demo we're using the example signing certificate from the IdentityServer3 samples repository
        /// and embedding it in our repository. This is a bad plan for production. 
        /// 
        /// In a production environment you should be adding this to your certificate store. 
        /// For instructions on using the Azure Websites certificate store see this:
        /// https://azure.microsoft.com/en-us/blog/using-certificates-in-azure-websites-applications/
        /// </summary>
        /// <returns></returns>
        public static X509Certificate2 Load()
        {
            // todo: change out the sample signing certificate for a real one
            var assembly = typeof(Cert).Assembly;
            var certpath = "Demo.AuthService.Web.MoveMe.idsrv3test.pfx";
            using (var stream = assembly.GetManifestResourceStream(certpath))
            {
                return new X509Certificate2(ReadStream(stream), "idsrv3test");
            }
        }

        private static byte[] ReadStream(Stream input)
        {
            var buffer = new byte[16*1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}