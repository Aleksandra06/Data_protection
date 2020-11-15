using System;
using System.Collections.Generic;
using System.Linq;
using DataProtection.PageModels.Lab1;

namespace DataProtection.Pages.Lab1
{
    public partial class TwoPage
    {
        Evklid EvklidNumbers { get; set; } = new Evklid();
        long mX, mY;
        bool mIsShowTable = false;
        int num = 1;
        long qForTableShow;
        int MAX = 1000000000;
        Boolean flag = false;

        List<GeneralizedEvklid> TablEvklid { get; set; } = new List<GeneralizedEvklid>();

        bool Check = true;

        void Calculation()
        {
            TablEvklid.Clear();
            EvklidNumbers.R = Evklid(EvklidNumbers.A, EvklidNumbers.B);
        }

        long Evklid(long a, long b)
        {
            if (a < b)
            {
                var tmp = a;
                a = b;
                b = tmp;
            }

            TablEvklid.Add(new GeneralizedEvklid());
            TablEvklid.Last().U = new Evklid(a, 1, 0);
            TablEvklid.Last().V = new Evklid(b, 0, 1);

            while (TablEvklid.Last().V.A != 0)
            {
                var E = TablEvklid.Last();
                var q = E.U.A / E.V.A;

                E.T = new Evklid();
                E.T.A = E.U.A % E.V.A;
                E.T.B = E.U.B - (q * E.V.B);
                E.T.R = E.U.R - (q * E.V.R);

                TablEvklid.Add(new GeneralizedEvklid());
                TablEvklid.Last().U = E.V;
                TablEvklid.Last().V = E.T;
                num++;
            }

            mX = TablEvklid.Last().U.B;
            mY = TablEvklid.Last().U.R;
            Check = ((a * mX) + (b * mY)) == TablEvklid.Last().U.A;

            return TablEvklid.Last().U.A;
        }
        public void generateRandom()
        {
            flag = false;
            var rand = new Random();
            do
            {
                EvklidNumbers.A = rand.Next(MAX);
                EvklidNumbers.B = rand.Next(MAX);
            } while (EvklidNumbers.A < EvklidNumbers.B);
        }
    }
}
