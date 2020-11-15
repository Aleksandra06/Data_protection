using System;
using DataProtection.PageModels.Lab1;

namespace DataProtection.Pages.Lab1
{
    public partial class OnePage
    {
        NumbersToPower NumberToPower { get; set; } = new NumbersToPower();
        private string Check { get; set; }
        readonly int MAX = 1000000000;
        Boolean mFlag = false;

        void Calculation()
        {
            if (NumberToPower.p == 0)
            {
                return;
            }
            else if (NumberToPower.p > 3 && (NumberToPower.p % 2 == 0 || NumberToPower.p % 3 == 0))
            {
                Check = "NO";
                return;
            }

            Check = System.Numerics.BigInteger.ModPow(NumberToPower.a, NumberToPower.x, NumberToPower.p).ToString();
            NumberToPower.y = Pow(NumberToPower.a, NumberToPower.x, NumberToPower.p);
        }

        long Pow(long a, long x, long p)
        {
            long y = new long();
            long s = new long();
            var bin = Convert.ToString(x, 2);

            y = 1;
            s = a % p;
            if (bin[^1] != '0')
            {
                y = s;
            }

            for (var i = bin.Length - 2; i >= 0; i--)
            {
                s = (s * s) % p;

                var t = bin[i];
                var t1 = '0';
                if (bin[i] == '0')
                {
                    continue;
                }

                y = (s * y) % p;
            }

            return y % p;
        }

        public void GenerateRandom()
        {
            mFlag = false;
            var rand = new Random();
            NumberToPower.a = rand.Next(1, MAX - 1);

            int currentP;
            do
            {
                currentP = rand.Next(1, MAX - 1);
            } while (!IsPrime(currentP));
            NumberToPower.p = currentP;
            do
            {
                NumberToPower.x = rand.Next(1, MAX - 1);
            } while (NumberToPower.x >= NumberToPower.p);
        }

        static bool IsPrime(long p)
        {
            if (p <= 1) return false;

            int b = (int)Math.Pow(p, 0.5);

            for (int i = 2; i <= b; ++i)
            {
                if ((p % i) == 0) return false;
            }

            return true;
        }
    }
}
