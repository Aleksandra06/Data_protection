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

        EvklidBigInteger evklid = new EvklidBigInteger();
        BigInteger check, check_left, check_right;


        public void generate()
        {
            SHA256 sha256 = SHA256.Create();
            elGamal.h = new BigInteger(sha256.ComputeHash(Document.Data));


            BigInteger currentQ;
            BigInteger currentP;
            do {
                currentQ = BigInteger.ProbablePrime(264, new Random());
                currentP = currentQ.Multiply(BigInteger.Two);
                currentP = currentP.Add(BigInteger.One);
            } while (!currentP.IsProbablePrime(50));

            elGamal.p = currentP;
            elGamal.p_prev = elGamal.p.Subtract(BigInteger.One);
            elGamal.q = currentQ;

            for (BigInteger i = BigInteger.Two; i.CompareTo(elGamal.p_prev) < 0; i = i.Add(BigInteger.One)) {
                if (i.ModPow(elGamal.q, elGamal.p).CompareTo(BigInteger.One) != 0) {
                    elGamal.g = i;
                    break;
                }
            }

            elGamal.x = new BigInteger(256, new Random());
            elGamal.y = elGamal.g.ModPow(elGamal.x, elGamal.p);

            BigInteger currentK;
            do {
                currentK = new BigInteger(256, new Random());
            } while (currentK.Gcd(elGamal.p_prev).CompareTo(BigInteger.One) != 0);
            elGamal.k = currentK;

            elGamal.r = elGamal.g.ModPow(elGamal.k, elGamal.p);
            BigInteger tmp_hxr = elGamal.h.Subtract(elGamal.x.Multiply(elGamal.r));
            elGamal.u = tmp_hxr.Mod(elGamal.p_prev);

            check = evklid.gcd(elGamal.k, elGamal.p_prev);
            if (evklid.mY.CompareTo(BigInteger.Zero) < 0) {
                elGamal.k_revers = evklid.mY.Add(elGamal.p_prev);
            } else {
                elGamal.k_revers = evklid.mY;
            }

            BigInteger tmp_k_revers_u = elGamal.k_revers.Multiply(elGamal.u);
            elGamal.s = tmp_k_revers_u.Mod(elGamal.p_prev);

            BigInteger tmp_yr = elGamal.y.ModPow(elGamal.r, elGamal.p);
            BigInteger tmp_rs = elGamal.r.ModPow(elGamal.s, elGamal.p);
            BigInteger tmp_yrs = tmp_yr.Multiply(tmp_rs).Mod(elGamal.p);
            BigInteger tmp_ghp = elGamal.g.ModPow(elGamal.h, elGamal.p);
            check_left = tmp_yrs;
            check_right = tmp_ghp;
        }

    }
}
