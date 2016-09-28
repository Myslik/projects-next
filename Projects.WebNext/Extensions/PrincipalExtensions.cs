using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Projects.WebNext.Extensions
{
    public static class PrincipalExtensions
    {
        private const string gravatarRootUrl = "https://www.gravatar.com/avatar/";

        public static string GetGravatarLink(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            string email = string.Empty;
            if (claimsIdentity != null)
            {
                email = claimsIdentity.FindFirst(ClaimTypes.Email).Value;
            }
            string gravatarHash = ComputeMD5(email == null ? string.Empty : email.ToLower());
            return $"{gravatarRootUrl}/{gravatarHash}";
        }

        private static string ComputeMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }   
        }
    }
}
