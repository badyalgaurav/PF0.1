using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.Presentation.UI.Common
{
   public class Utility
    {
        private const string Alg = "HmacSHA256";
        private const string Salt = "rz8LuOtFBXphj9WQfvFh";

        /// <summary>
        /// Returns a hashed password + salt, to be used in generating a token.
        /// </summary>
        /// <param name="password">string - user's password</param>
        /// <returns>string - hashed password</returns>
        public static string GetHashedPassword(string password)
        {
            ////store password by encrypt form
            ////Read work factor http://wildlyinaccurate.com/bcrypt-choosing-a-work-factor/
            //var PASSWORD_BCRYPT_COST = 8; // work factor
            //var PASSWORD_SALT = GetSalt(); //generated random salt
            //var salt = "$2a$" + PASSWORD_BCRYPT_COST + "$" + PASSWORD_SALT;
            //var pwdToHash = password + salt;
            //var hashToStoreInDatabase = BCrypt.Net.BCrypt.HashPassword(pwdToHash, BCrypt.Net.BCrypt.GenerateSalt());
            var key = string.Join(":", new string[] { password, Salt });

            using (HMAC hmac = HMACSHA256.Create(Alg))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(Salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));

                return Convert.ToBase64String(hmac.Hash);
            }
        }

        public static string GetSalt()
        {
            var random = new RNGCryptoServiceProvider();

            // Maximum length of salt
            var max_length = 32;

            // Empty salt array
            var salt = new byte[max_length];

            // Build the random bytes
            random.GetNonZeroBytes(salt);

            // Return the string encoded salt
            return Convert.ToBase64String(salt);
        }
    }
}
