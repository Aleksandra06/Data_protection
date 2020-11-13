using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
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
        protected string mMessageResult = "";

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public void generate()
        {
            //h
            SHA256 sha256 = SHA256.Create();
            gost.h = new BigInteger(sha256.ComputeHash(Document.Data));
            gost.h = gost.h.Abs();

            //q p b
            do
            {
                gost.q = BigInteger.ProbablePrime(256, random);

                gost.b = new BigInteger(769, random);

                gost.p = new BigInteger(((gost.b.Multiply(gost.q)).Add(BigInteger.One)).ToString());
            } while (!gost.p.IsProbablePrime(50) || gost.h.CompareTo(gost.q) > 0);

            //а
            BigInteger g;
            do
            {
                g = new BigInteger(gost.p.BitLength, random);
            } while (FastModuloExponentiation(g, gost.q, gost.p).CompareTo(BigInteger.One) == 0);

            do
            {
                gost.a = FastModuloExponentiation(g, (gost.p.Subtract(BigInteger.One)).Divide(gost.q), gost.p);
            } while (gost.a.CompareTo(BigInteger.One) != 1);

            //bitLength просто меньше 256
            var bitLength = 40;
            gost.x = BigInteger.ProbablePrime(bitLength, random);
            gost.y = FastModuloExponentiation(gost.a, gost.x, gost.p);

            //gost.h = new BigInteger(Document.Data.GetHashCode().ToString());
            //gost.h = gost.h.Abs();
            do
            {
                gost.k = BigInteger.ProbablePrime(bitLength, random);
                gost.r = FastModuloExponentiation(gost.a, gost.k, gost.p).Mod(gost.q);
            } while (gost.r.CompareTo(BigInteger.Zero) == 0);

            do
            {
                gost.s = ((gost.k.Multiply(gost.h)).Add(gost.x.Multiply(gost.r))).Mod(gost.q);
            } while (gost.s.CompareTo(BigInteger.Zero) == 0);

            //проверка
            gost.h_check = new BigInteger(sha256.ComputeHash(Document.Data));
            gost.h_check = gost.h_check.Abs();
            var r = gost.r;
            var s = gost.s;
            if (r.CompareTo(gost.q) >= 0 || s.CompareTo(gost.q) >= 0)
            {
                mMessageResult = "Числа r/s не удовлетворяют диапазону 0<r<q или 0<s<q!";
                return;
            }
            var hMinOne = gost.h_check.ModInverse(gost.q);
            var blablabla = hMinOne.Multiply(gost.h_check).Mod(gost.q);
            gost.u1 = (s.Multiply(hMinOne)).Mod(gost.q);
            gost.u2 = (((r).Multiply(hMinOne)).Multiply(new BigInteger("-1"))).Mod(gost.q);
            gost.v = (FastModuloExponentiation(gost.a, gost.u1, gost.p).Multiply(FastModuloExponentiation(gost.y, gost.u2, gost.p))).Mod(gost.p).Mod(gost.q);

            if (gost.v.CompareTo(r) == 0)
            {
                mMessageResult = "Проверка подписи ГОСТ успешно пройдена!";
            }
            else
            {
                mMessageResult = "Цифровая подпись не прошла проверку! (ГОСТ)";
            }
        }

        public static BigInteger FastModuloExponentiation(BigInteger a, BigInteger x, BigInteger p)
        {
            //return a.modPow(x, p);
            BigInteger y = new BigInteger("1");
            BigInteger s = new BigInteger(a.ToString());
            String binary = x.ToString(2);

            for (int i = binary.Length - 1; i >= 0; i--)
            {
                //49 символ = единице
                var t = binary[i];
                if (binary[i] == 49) y = (y.Multiply(s)).Mod(p);
                s = (s.Multiply(s)).Mod(p);
            }

            return y;
        }
    }
}
