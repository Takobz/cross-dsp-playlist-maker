using System.Text;

namespace CrossDSP.Infrastructure.Authentication.Common.Helpers
{
    public static class CredentialsHelper
    {
        public static string GenerateBase64ClientCredentials(
            string username,
            string password
        )
        {
            var byteClientCredentials = Encoding.UTF8.GetBytes($"{username}:{password}");
            return Convert.ToBase64String(byteClientCredentials);
        }
    }
}