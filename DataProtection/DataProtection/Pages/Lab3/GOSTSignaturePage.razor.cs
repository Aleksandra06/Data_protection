using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DataProtection.PageModels;
using DataProtection.PageModels.Lab3;
using DataProtection.Pages.Lab3;
using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Math;

namespace DataProtection.Pages.Lab3
{
    public partial class GOSTSignaturePage
    {
        [Parameter] public DocumentModel Document { get; set; }
        public GOSTSignatureModel gost = new GOSTSignatureModel();
        Random random = new Random();
        //private const int b = Math 767;

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        public void generate()
        {
            SHA256 sha256 = SHA256.Create();
            // gost.h = new BigInteger(sha256.ComputeHash(Document.Data));
            gost.p = BigInteger.ProbablePrime(1024, random);
            BigInteger tmp = gost.p.Subtract(BigInteger.One);
            BigInteger t;
            do
            {
                gost.q = BigInteger.ProbablePrime(256, random);
                t = tmp.Mod(gost.q);

            } while (t.CompareTo(BigInteger.Zero) != 0);
            gost.b = tmp.Divide(gost.q);

            //a
            //var stepen = new BigInteger(b.ToString()).Mod(gost.q);
            //do
            //{
            //    gost.a = new BigInteger(random.Next().ToString());
            //} while (gost.a.ModPow(gost.q, gost.p).CompareTo(BigInteger.One) != 0);
        }
    }
}
