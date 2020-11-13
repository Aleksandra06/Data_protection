using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using DataProtection.Engine.Models;
using Org.BouncyCastle.Math;

namespace DataProtection.Engine.Managers
{
    public class EvklidBigInteger
    {
        public List<GeneralizedBigIntegerEvklid> TabEvklid { get; set; } = 
            new List<GeneralizedBigIntegerEvklid>();
        public Org.BouncyCastle.Math.BigInteger mX, mY;
        public bool mCheck = true;

        public Org.BouncyCastle.Math.BigInteger gcd(Org.BouncyCastle.Math.BigInteger _a, Org.BouncyCastle.Math.BigInteger _b)
        {
            var a = _a;
            var b = _b;
            if (a.CompareTo(b) < 0) {
                var tmp = a; a = b; b = tmp;
            }

            TabEvklid.Add(new GeneralizedBigIntegerEvklid());
            TabEvklid.Last().U = new EvklidBigIntegerModel(a, new Org.BouncyCastle.Math.BigInteger(1.ToString()), new Org.BouncyCastle.Math.BigInteger(0.ToString()));
            TabEvklid.Last().V = new EvklidBigIntegerModel(b, new Org.BouncyCastle.Math.BigInteger(1.ToString()), new Org.BouncyCastle.Math.BigInteger(0.ToString()));

            while (TabEvklid.Last().V.A.CompareTo(0) != 0) {
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

            mY = TabEvklid.Last().U.B;
            mY = TabEvklid.Last().U.R;
            mCheck = (a.Multiply(mY).Add(b.Multiply(mY))).CompareTo(TabEvklid.Last().U.A) == 0;

            return TabEvklid.Last().U.A;
        }

    }
}
