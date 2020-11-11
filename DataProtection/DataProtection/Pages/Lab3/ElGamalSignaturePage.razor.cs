using DataProtection.Engine.Managers;
using DataProtection.PageModels;
using DataProtection.PageModels.Lab3;
using Microsoft.AspNetCore.Components;
using System;

namespace DataProtection.Pages.Lab3
{
    public partial class ElGamalSignaturePage
    {
        [Parameter] public DocumentModel Document { get; set; }
        ElGamalSignatureModel elGamal = new ElGamalSignatureModel();

        Random random = new Random();
        MyModPow myMod = new MyModPow();
        Evklid evklid = new Evklid();

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        public void generate()
        {
            elGamal.p = generateP();
            elGamal.p_prev = elGamal.p - 1;
            elGamal.g = generateG();
            elGamal.x = random.Next(2, (int)elGamal.p_prev);
            elGamal.y = myMod.Pow(elGamal.g, elGamal.x, elGamal.p);
            // elGamal.h = ... // Хеш - функция для message
            elGamal.k = generateK();
            elGamal.r = myMod.Pow(elGamal.g, elGamal.k, elGamal.p);


        }

        public long generateK()
        {
            long currentK;
            do {
                currentK = random.Next(2, (int)elGamal.p_prev);
            } while (evklid.gcd(currentK, elGamal.p_prev) != 1);
            return currentK;
        }

        public long generateQ()
        {
            long currentQ;
            do {
                currentQ = random.Next(1 << 7, 1 << 8);
            } while (!IsPrime.isPrime(currentQ, 1 << 10));
            return currentQ;
        }

        public long generateG()
        {
            long currentG = 1;
            for (int i = 2; i < elGamal.p_prev; i++) {
                if (myMod.Pow(i, elGamal.q, elGamal.p) != 1) {
                    currentG = i;
                    break;
                }
            }
            return currentG;
        }

        public long generateP()
        {
            long currentP;
            do {
                elGamal.q = generateQ();
                currentP = elGamal.q * 2 + 1;

            } while (!IsPrime.isPrime(currentP, 1 << 10));
            return currentP;
        }
    }
}
