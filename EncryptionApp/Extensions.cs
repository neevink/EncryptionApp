using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class Extensions
    {
        /// <summary>
        /// Is number prime
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPrime(this int n)
        {
            int i = 2;
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
        public static int GreatestCommonDivisor(this int a, int b)
        {
            return gcd(a, b);
        }

        /// <summary>
        /// Returns greatest common divisor of two numbers with coefficients
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int SolveDiophantineEquation(this int a, int b, out int x, out int y)
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
        public static int BinaryPow(this int a, int power, int module)
        {
            int ans = 1;
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
        private static int gcd(int a, int b)
        {
            return b == 0 ? a : gcd(b, a % b);
        }

        // расширенный алгоритм Евклида, решает уравнение a*x + b*y = gcd(a, b);
        private static int egcd(int a, int b, out int x, out int y)
        {
            if (a == 0)
            {
                x = 0; y = 1;
                return b;
            }
            int x1, y1;
            int d = egcd(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;

            y = x1;
            return d;
        }
    }
}
