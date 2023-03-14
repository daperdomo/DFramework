using DFramework.Application.Common.Interfaces.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace DFramework.Application.Services.Authentication
{
    public class Md5PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }
    }
}
