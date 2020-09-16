using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataProtection.Engine
{
    public class GeneralizedEvklid
    {
        public Evklid V { get; set; }
        public Evklid T { get; set; }
        public Evklid U { get; set; }
    }
    public class Evklid
    {
        public long A { get; set; }
        public long B { get; set; }
        public long R { get; set; }

        public Evklid()
        {
        }
        public Evklid(long a, long b, long r)
        {
            A = a;
            B = b;
            R = r;
        }
        List<GeneralizedEvklid> TablEvklid { get; set; } = new List<GeneralizedEvklid>();
        long mX, mY;
        bool Check = true;

        public long gcd(long a, long b)
        {
            if (a < b) {
                var tmp = a;
                a = b;
                b = tmp;
            }

            TablEvklid.Add(new GeneralizedEvklid());
            TablEvklid.Last().U = new Evklid(a, 1, 0);
            TablEvklid.Last().V = new Evklid(b, 0, 1);

            while (TablEvklid.Last().V.A != 0) {
                var E = TablEvklid.Last();
                var q = E.U.A / E.V.A;

                E.T = new Evklid();
                E.T.A = E.U.A % E.V.A;
                E.T.B = E.U.B - (q * E.V.B);
                E.T.R = E.U.R - (q * E.V.R);

                TablEvklid.Add(new GeneralizedEvklid());
                TablEvklid.Last().U = E.V;
                TablEvklid.Last().V = E.T;
            }

            mX = TablEvklid.Last().U.B;
            mY = TablEvklid.Last().U.R;
            Check = ((a * mX) + (b * mY)) == TablEvklid.Last().U.A;

            return TablEvklid.Last().U.A;
        }
    }
}
