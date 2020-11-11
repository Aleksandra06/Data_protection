using DataProtection.Engine.Managers;
using DataProtection.PageModels;
using DataProtection.PageModels.Lab3;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace DataProtection.Pages.Lab3
{
    public partial class RSASignaturePage
    {
        [Parameter] public DocumentModel Document { get; set; }
        public RSASignatureModel rsa = new RSASignatureModel();

        Random random = new Random();
        MyModPow myMod = new MyModPow();
        Evklid evklid = new Evklid();
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        public void generate()
        {
            rsa.p = generateP(); // and generate Q
            rsa.n = rsa.p * rsa.q;
            rsa.phi = (rsa.p - 1) * (rsa.q - 1);
            rsa.d = generateD();
            rsa.c = generateDREVERS();
            // rsa.y = h(message) - hash
            //rsa.s = myMod.Pow(rsa.y, rsa.c, rsa.n);
            //rsa.w = myMod.Pow(rsa.s, rsa.d, rsa.n);
        }

        public long generateP()
        {
            long currentP;
            do {
                rsa.q = generateQ();
                currentP = random.Next(1, 1 << 10);

            } while (!IsPrime.isPrime(currentP, 1 << 10) && evklid.gcd(currentP, rsa.q) == 1);
            return currentP;
        }
        public long generateQ()
        {
            long currentQ;
            do {
                currentQ = random.Next(1, 1 << 10);
            } while (!IsPrime.isPrime(currentQ, 1 << 10));
            return currentQ;
        }
        public long generateD()
        {
            long currentD;
            do {
                currentD = random.Next(2, (int)rsa.phi - 1);
            } while (evklid.gcd(currentD, rsa.phi) != 1);
            return currentD;
        }
        public long generateDREVERS()
        {
            evklid.gcd(rsa.d, rsa.phi);

            if (evklid.mY < 0) {
                evklid.mY += rsa.phi;
            }
            return evklid.mY;
        }
    }
}
