using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionApp
{
    class RSAEncryptor
    {
        /// <summary>
        /// Public key, pair N and E
        /// </summary>
        public Pair<int, int> PublicKey { get; private set; }

        /// <summary>
        /// Private key, pair N and D
        /// </summary>
        public Pair<int, int> PrivateKey { get; private set; }

        public RSAEncryptor(int p, int q)
        {
            PublicKey = new Pair<int, int>();
            PrivateKey = new Pair<int, int>();
            PublicKey.First = p * q;
            PrivateKey.First = PublicKey.First;

            // Euler function
            int f = (p - 1) * (q - 1);

            // Открытая экспонента, обычно одно из простых чисел Ферма 3, 5, 17, 257, 65537
            int e = 257;
            PublicKey.Second = e;

            int x, y;
            e.SolveDiophantineEquation(f, out x, out y);

            int d = f + x;
            PrivateKey.Second = d;
        }

        public int[] Encrypt(Pair<int, int> otherPublicKey, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            int[] vals = new int[bytes.Length];
            
            for(int i = 0; i < bytes.Length; i++)
            {
                vals[i] = ((int)bytes[i]).BinaryPow(PublicKey.Second, PublicKey.First);
            }

            return vals;
        }

        public string Decrypt(int[] encrypdedMessage)
        {
            byte[] bytes = new byte[encrypdedMessage.Length];

            for (int i = 0; i < encrypdedMessage.Length; i++)
            {
                bytes[i] = (byte)encrypdedMessage[i].BinaryPow(PrivateKey.Second, PrivateKey.First);
            }
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
