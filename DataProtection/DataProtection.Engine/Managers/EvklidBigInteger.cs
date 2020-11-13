using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataProtection.Engine.Models;
using Org.BouncyCastle.Math;

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
            if (a.CompareTo(b) < 0) {
                var tmp = a; a = b; b = tmp;
            }

            TabEvklid.Add(new GeneralizedBigIntegerEvklid());
            TabEvklid.Last().U = new EvklidBigIntegerModel(a, BigInteger.One, BigInteger.Zero);
            TabEvklid.Last().V = new EvklidBigIntegerModel(b, BigInteger.Zero, BigInteger.One);

            while (TabEvklid.Last().V.A.CompareTo(BigInteger.Zero) != 0) {
                var E = TabEvklid.Last();
                var q = E.U.A.Divide(E.V.A);

                E.T = new EvklidBigIntegerModel();
                E.T.A = E.U.A.Mod(E.V.A);
                E.T.B = E.U.B.Subtract(q.Multiply(E.V.B));
                E.T.R = E.U.R.Subtract(q.Multiply(E.V.R));

                TabEvklid.Add(new GeneralizedBigIntegerEvklid());
                TabEvklid.Last().U = E.V;
                TabEvklid.Last().V = E.T;
            }

            mX = TabEvklid.Last().U.B;
            mY = TabEvklid.Last().U.R;
            BigInteger tmpCheck = a.Multiply(mX);
            tmpCheck = tmpCheck.Add(b.Multiply(mY));
            mCheck = tmpCheck.CompareTo(TabEvklid.Last().U.A) == 0;

            return TabEvklid.Last().U.A;
        }

    }
}
