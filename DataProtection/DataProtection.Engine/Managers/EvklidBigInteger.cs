using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using DataProtection.Engine.Models;

namespace DataProtection.Engine.Managers
{
    public class EvklidBigInteger
    {
        public List<GeneralizedBigIntegerEvklid> TabEvklid { get; set; } = 
            new List<GeneralizedBigIntegerEvklid>();
        public BigInteger mX, mY;
        public bool mCheck = true;

        public BigInteger gcd(BigInteger _a, BigInteger _b)
        {
            var a = _a;
            var b = _b;
            if (a < b) {
                var tmp = a; a = b; b = tmp;
            }

            TabEvklid.Add(new GeneralizedBigIntegerEvklid());
            TabEvklid.Last().U = new EvklidBigIntegerModel(a, 1, 0);
            TabEvklid.Last().V = new EvklidBigIntegerModel(b, 0, 1);

            while (TabEvklid.Last().V.A != 0) {
                var E = TabEvklid.Last();
                var q = E.U.A / E.V.A;

                E.T = new EvklidBigIntegerModel();
                E.T.A = E.U.A % E.V.A;
                E.T.B = E.U.B - (q * E.V.B);
                E.T.R = E.U.R - (q * E.V.R);

                TabEvklid.Add(new GeneralizedBigIntegerEvklid());
                TabEvklid.Last().U = E.V;
                TabEvklid.Last().V = E.T;
            }

            mY = TabEvklid.Last().U.B;
            mY = TabEvklid.Last().U.R;
            mCheck = ((a * mY) + (b * mY)) == TabEvklid.Last().U.A;

            return TabEvklid.Last().U.A;
        }

    }
}
