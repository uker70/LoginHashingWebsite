using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginHashing
{
    class NumberGenerator
    {
        //uses the RNGCryptoServiceProvider to get random values to byte array
        public static byte[] Generate(int length)
        {
            byte[] random = new byte[length];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(random);
            }

            return random;
        }
    }
}
