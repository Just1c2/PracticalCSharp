using System.Text;
using XSystem.Security.Cryptography;

namespace PracticalDbFirst.Utils
{
    public static class Utils
    {
        public static string ComputeSha256Hash(this string username, string salt, string password)
        {
            var encrypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = encrypt.ComputeHash(Encoding.UTF8.GetBytes($"{username} - {salt} - {pass}"));
            foreach (var theByte in crypto) hash.Append(theByte.ToString("x2"));
            return hash.ToString();
        }

        public static bool ValidPassword(this string username, string salt, string pass, string passHash)
        {
            var isValid = ComputeSha256Hash(username, salt, password).Equals(passHash);
            return isValid;
        }
    }
}
