using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginHashing
{
    class Hash
    {
        //loop that hashes the password
        public static byte[] HashLoop(byte[] password, byte[] salt)
        {
            byte[] hash = password;
            for (int loop = 0; loop < 20000; loop++)
            {
                hash = Sha512Hash(hash, salt);
            }

            return hash;
        }

        //combines the password and salt
        private static byte[] Combine(byte[] password, byte[] salt)
        {
            byte[] output = new byte[password.Length + salt.Length];

            Buffer.BlockCopy(password, 0, output, 0, password.Length);
            Buffer.BlockCopy(salt, 0, output, password.Length, salt.Length);

            return output;
        }

        //hashes the password
        private static byte[] Sha512Hash(byte[] password, byte[] salt)
        {
            using (SHA512 sha = SHA512.Create())
            {
                return sha.ComputeHash(Combine(password, salt));
            }
        }
    }
}
