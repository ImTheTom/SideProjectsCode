using System;
using System.Security.Cryptography;
using System.Text;

namespace MessengerApplicaiton {
    class Password {
        private string password;
        private int salt;

        public Password(string Password, int Salt) {
            this.password = Password;
            this.salt = Salt;
        }

        public static int CreateRandomSalt() {
            byte[] saltBytes = new byte[4];
            RNGCryptoServiceProvider randomGeneratedSalt = new RNGCryptoServiceProvider();
            randomGeneratedSalt.GetBytes(saltBytes);
            return (((int)saltBytes[0] << 24) + ((int)saltBytes[1]) << 16) + ((int)saltBytes[2] << 8) + ((int)saltBytes[3]);
        }

        public string ComputeSaltedHash() {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] secretBytes = encoder.GetBytes(password);
            byte[] saltBytes = new byte[4];
            saltBytes[0] = (byte)(salt >> 24);
            saltBytes[1] = (byte)(salt >> 16);
            saltBytes[2] = (byte)(salt >> 8);
            saltBytes[3] = (byte)(salt);
            byte[] saltedPasswordBytes = new byte[secretBytes.Length + saltBytes.Length];
            Array.Copy(secretBytes, 0, saltedPasswordBytes, 0, secretBytes.Length);
            Array.Copy(saltBytes, 0, saltedPasswordBytes, secretBytes.Length, saltBytes.Length);
            SHA256 sha256 = SHA256.Create();
            byte[] computedHash = sha256.ComputeHash(saltedPasswordBytes);
            return Convert.ToBase64String(computedHash);
        }
    }
}
