using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;
using XSystem.Security.Cryptography;

namespace mersetaWebAPI.Helpers
{
    public class CryptographyProcessor
    {
        public byte[] CreateSalt()
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }

        public string GenerateHash(string password, byte[]  salt)
        {
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        //public bool AreEqual(string plainTextInput, string hashedInput, string salt)
        //{
        //    string newHashedPin = GenerateHash(plainTextInput, salt);
        //    return newHashedPin.Equals(hashedInput);
        //}
    }
}
