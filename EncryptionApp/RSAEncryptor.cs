using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EncryptionApp
{
    class RSAEncryptor
    {
        /// <summary>
        /// Public key, pair N and E
        /// </summary>
        public Pair<BigInteger, BigInteger> PublicKey { get; private set; }

        /// <summary>
        /// Private key, pair N and D
        /// </summary>
        public Pair<BigInteger, BigInteger> PrivateKey { get; private set; }

        public RSAEncryptor(BigInteger p, BigInteger q)
        {
            PublicKey = new Pair<BigInteger, BigInteger>();
            PrivateKey = new Pair<BigInteger, BigInteger>();
            PrivateKey.First = PublicKey.First = p * q;

            // Euler function
            BigInteger f = (p - 1) * (q - 1);

            // Открытая экспонента, обычно одно из простых чисел Ферма 3, 5, 17, 257, 65537
            BigInteger e = 257;
            PublicKey.Second = e;

            BigInteger x, y;
            e.SolveDiophantineEquation(f, out x, out y);

            PrivateKey.Second = f + x;
        }

        public BigInteger[] Encrypt(Pair<BigInteger, BigInteger> otherPublicKey, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            BigInteger[] vals = new BigInteger[bytes.Length];

            BigInteger c = 0;
            for(int i = 0; i < bytes.Length; i++)
            {
                vals[i] = ((BigInteger)bytes[i]).BinaryPow(PublicKey.Second, PublicKey.First);
                vals[i] = (vals[i] + c) % PublicKey.First;
                c = vals[i];
            }

            return vals;
        }

        public string Decrypt(BigInteger[] encrypdedMessage)
        {
            byte[] bytes = new byte[encrypdedMessage.Length];
            
            for (int i = 0; i < encrypdedMessage.Length-1; i++)
            {
                if(i == 0)
                {
                    bytes[i] = (byte)encrypdedMessage[i].BinaryPow(PrivateKey.Second, PrivateKey.First);
                }
                else
                {
                    bytes[i] = (byte)(((encrypdedMessage[i] - encrypdedMessage[i - 1] + PrivateKey.First) % PrivateKey.First).BinaryPow(PrivateKey.Second, PrivateKey.First) % 256);
                }
            }
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
