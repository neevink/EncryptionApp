using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace System
{
    public static class Extensions
    {
        /// <summary>
        /// Is number prime
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPrime(this BigInteger n)
        {
            BigInteger i = 2;
            while(i*i <= n)
            {
                if(n % i == 0)
                {
                    return false;
                }
                i++;
            }
            return true;
        }

        /// <summary>
        /// Returns greatest common divisor of two numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static BigInteger GreatestCommonDivisor(this BigInteger a, BigInteger b)
        {
            return gcd(a, b);
        }

        /// <summary>
        /// Returns greatest common divisor of two numbers with coefficients
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static BigInteger SolveDiophantineEquation(this BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            return egcd(a, b, out x, out y);
        }

        /// <summary>
        /// Binary pow
        /// </summary>
        /// <param name="a"></param>
        /// <param name="power"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static BigInteger BinaryPow(this BigInteger a, BigInteger power, BigInteger module)
        {
            BigInteger ans = 1;
            while(power > 0)
            {
                if(power % 2 == 1)
                {
                    ans *= a;
                }
                a *= a;
                a %= module;
                ans %= module;
                power >>= 1;
            }
            return ans;
        }

        // алгоритм Евклида
        private static BigInteger gcd(BigInteger a, BigInteger b)
        {
            return b == 0 ? a : gcd(b, a % b);
        }

        // расширенный алгоритм Евклида, решает уравнение a*x + b*y = gcd(a, b);
        private static BigInteger egcd(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (a == 0)
            {
                x = 0; y = 1;
                return b;
            }
            BigInteger x1, y1;
            BigInteger d = egcd(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;

            y = x1;
            return d;
        }
    }
}
