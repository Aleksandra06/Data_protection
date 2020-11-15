using System;
using DataProtection.Engine.Managers;
using DataProtection.PageModels.Lab1;
using Evklid = DataProtection.Engine.Managers.Evklid;

namespace DataProtection.Pages.Lab1
{
    public partial class ThreePage
    {
        Diffi_Hellman APerson { get; set; } = new Diffi_Hellman();
        Diffi_Hellman BPerson { get; set; } = new Diffi_Hellman();

        MyModPow ModPow { get; set; } = new MyModPow();

        long q = 11;
        long p;
        long g;

        bool mFlag = false;
        readonly int MAX = 1000000000;

        protected override void OnInitialized()
        {
            p = q * 2 + 1;
            for (int i = 2; i < p - 1; i++)
            {
                var tmp = ModPow.Pow(i, q, p);
                if (tmp != 1)
                {
                    g = i;
                    break;
                }
            }
        }

        public void ComputeG()
        {
            int k = 20;
            var rand = new Random();

            long currentP;
            long currentQ;
            do
            {
                do
                {
                    currentQ = rand.Next(2, MAX);
                } while (!IsPrime(currentQ, k));
                q = currentQ;
                currentP = q * 2 + 1;
            } while (!IsPrime(currentP, k));
            p = currentP;
            for (int i = 2; i < p - 1; i++)
            {
                if (ModPow.Pow(i, q, p) != 1)
                {
                    q = i;
                    break;
                }
            }
            generateRandom();
        }

        void Calculation()
        {
            if (APerson.X == 0 && BPerson.X == 0)
            {
                return;
            }
            APerson.Z = ModPow.Pow(BPerson.Y, APerson.X, p);
            BPerson.Z = ModPow.Pow(APerson.Y, BPerson.X, p);
        }

        long OpenKeyA()
        {
            if (APerson.X < 0 || APerson.X > 999999999 || APerson.X >= p) return APerson.Y;

            APerson.Y = ModPow.Pow(g, APerson.X, p);

            return APerson.Y;
        }

        long OpenKeyB()
        {
            if (BPerson.X < 0 || BPerson.X > 999999999 || BPerson.X >= p) return BPerson.Y;
            BPerson.Y = ModPow.Pow(g, BPerson.X, p);
            return BPerson.Y;
        }

        public void generateRandom()
        {
            mFlag = false;
            var rand = new Random();

            APerson.X = rand.Next(1, (int)p - 1);
            BPerson.X = rand.Next(1, (int)p - 1);
        }

        bool IsPrime(long prime, int k)
        {
            Random rand = new Random();
            Evklid myAlg = new Evklid();
            if (prime == 2) return true;
            if (prime % 2 == 0) return false;
            for (int i = 0; i < k; i++)
            {
                long a = rand.Next(1, (int)prime - 1);
                if (myAlg.gcd(a, prime) != 1 || ModPow.Pow(a, prime - 1, prime) != 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
