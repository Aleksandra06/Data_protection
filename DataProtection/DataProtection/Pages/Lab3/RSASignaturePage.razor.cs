using DataProtection.Engine.Managers;
using DataProtection.PageModels;
using DataProtection.PageModels.Lab3;
using Microsoft.AspNetCore.Components;
using System;

using System.Reflection.Metadata;
using Org.BouncyCastle.Math;
using System.Security.Cryptography;

namespace DataProtection.Pages.Lab3
{
    public partial class RSASignaturePage
    {
        [Parameter] public DocumentModel Document { get; set; }
        public RSASignatureModel rsa = new RSASignatureModel();
        EvklidBigInteger evklid = new EvklidBigInteger();

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        public void generate()
        {
            rsa.p = generateP(); // and generate Q
            rsa.n = rsa.p.Multiply(rsa.q);
            rsa.phi = generatePhi();
            rsa.d = generateD();
            rsa.c = generateDREVERS();
            
            rsa.y = generateH();

            rsa.s = rsa.y.ModPow(rsa.c, rsa.n);
            rsa.w = rsa.s.ModPow(rsa.d, rsa.n);
        }
        public BigInteger generatePhi()
        {
            BigInteger tmp_p = rsa.p.Subtract(BigInteger.One);
            BigInteger tmp_q = rsa.q.Subtract(BigInteger.One);
            return tmp_p.Multiply(tmp_q);
        }
        public BigInteger generateH()
        {
            SHA256 sha256 = SHA256.Create();
            BigInteger h = new BigInteger(sha256.ComputeHash(Document.Data));
            if (h.SignValue < 0) {
                h = h.Abs();
            }
            return h;
        }
        public BigInteger generateP()
        {
            BigInteger currentP;
            do {
                rsa.q = generateQ();
                currentP = BigInteger.ProbablePrime(264, new Random());
            } while (!currentP.IsProbablePrime(50) && evklid.gcd(currentP, rsa.q) == BigInteger.One);
            return currentP;
        }
        public BigInteger generateQ()
        {
            BigInteger currentQ;
            do {
                currentQ = BigInteger.ProbablePrime(264, new Random());
            } while (!currentQ.IsProbablePrime(50));
            return currentQ;
        }
        public BigInteger generateD()
        {
            BigInteger currentD;
            do {
                currentD = BigInteger.ProbablePrime(264, new Random());
            } while (currentD.Gcd(rsa.phi).CompareTo(BigInteger.One) != 0);
            return currentD;
        }
        public BigInteger generateDREVERS()
        {
            evklid.gcd(rsa.d, rsa.phi);

            if (evklid.mY.CompareTo(BigInteger.Zero) < 0) {
                evklid.mY = evklid.mY.Add(rsa.phi);
            }
            return evklid.mY;
        }
    }
}
