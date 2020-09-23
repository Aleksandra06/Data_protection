using System.Collections.Generic;
using System.Linq;
using DataProtection.Engine.Models;

namespace DataProtection.Engine.Managers
{
    public class Evklid
    {
        List<GeneralizedEvklid> TablEvklid { get; set; } = new List<GeneralizedEvklid>();
        long mX, mY;
        bool mCheck = true;

        public long gcd(long a, long b)
        {
            if (a < b) {
                var tmp = a;
                a = b;
                b = tmp;
            }

            TablEvklid.Add(new GeneralizedEvklid());
            TablEvklid.Last().U = new EvklidModel(a, 1, 0);
            TablEvklid.Last().V = new EvklidModel(b, 0, 1);

            while (TablEvklid.Last().V.A != 0) {
                var E = TablEvklid.Last();
                var q = E.U.A / E.V.A;

                E.T = new EvklidModel();
                E.T.A = E.U.A % E.V.A;
                E.T.B = E.U.B - (q * E.V.B);
                E.T.R = E.U.R - (q * E.V.R);

                TablEvklid.Add(new GeneralizedEvklid());
                TablEvklid.Last().U = E.V;
                TablEvklid.Last().V = E.T;
            }

            mX = TablEvklid.Last().U.B;
            mY = TablEvklid.Last().U.R;
            mCheck = ((a * mX) + (b * mY)) == TablEvklid.Last().U.A;

            return TablEvklid.Last().U.A;
        }
    }
}
