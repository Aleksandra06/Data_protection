using DataProtection.Engine.Managers;

using DataProtection.PageModels;
using DataProtection.PageModels.Lab3;
using Microsoft.AspNetCore.Components;
using System;

using System.Security.Cryptography;
using Org.BouncyCastle.Math;


namespace DataProtection.Pages.Lab3
{
    public partial class ElGamalSignaturePage
    {
        [Parameter] public DocumentModel Document { get; set; }
        ElGamalSignatureModel elGamal = new ElGamalSignatureModel();

        Random random = new Random();
        MyModPow myMod = new MyModPow();
        Evklid evklid = new Evklid();

        
        public void generate()
        {
            SHA256 sha256 = SHA256.Create();
            elGamal.h = new BigInteger(sha256.ComputeHash(Document.Data));


            BigInteger currentQ;
            BigInteger currentP;
            do {
                currentQ = BigInteger.ProbablePrime(264, new Random());
                currentP = currentQ.Multiply(BigInteger.Two).Add(BigInteger.Two);
            } while (!currentP.IsProbablePrime(50));

            elGamal.p = currentP;
            elGamal.q = currentQ;

        }

    }
}
