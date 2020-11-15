using System;
using DataProtection.Engine.Managers;
using DataProtection.PageModels.Lab1;

namespace DataProtection.Pages.Lab1
{
    public partial class FourPage
    {
        private DisLog myLog { get; set; } = new DisLog();
        MyModPow Power { get; set; } = new MyModPow();
        private long mCheck;
        struct StepBaby
        {
            public long[] value;
            public long[] index;
        };
        StepBaby mStepBaby = new StepBaby();
        bool mIsShowTable = false;

        long[] mStepBabyShow;
        long[] mStepGiant;
        long[] mIndex;
        long mIBaby = -1, mJGiant = -1;
        readonly int MAX = 1000000000;
        long mHardPractice = 0, mHardTeory = 0;

        void Calculator()
        {
            if (myLog.a < 1 || myLog.p < 1 || myLog.y < 1 || myLog.m < 1 || myLog.k < 1)
            {
                return;
            }

            bool flagOnOne = false;
            int k = 1000;
            int i = 0;

            long tmp_m = myLog.m,
                    tmp_k = myLog.k;

            StepBg(myLog.a, myLog.y, myLog.p, tmp_m, tmp_k);
            //while ((iBaby == -1 || jGiant == -1) && i < k)
            //{
            //    if (tmp_m == tmp_k) {
            //        if (!flagOnOne) {
            //            tmp_m++;
            //            flagOnOne = true;
            //        } else {
            //            tmp_k++;
            //            flagOnOne = false;
            //        }
            //    } else if (tmp_m > tmp_k) {
            //        tmp_k++;
            //    } else if (tmp_m < tmp_k) {
            //        tmp_m++;
            //    }
            //    stepBG(myLog.a, myLog.y, myLog.p, tmp_m, tmp_k);
            //    i++;
            //}

            myLog.k = tmp_k;
            myLog.m = tmp_m;

            mHardTeory = (long)(Math.Sqrt(myLog.p) * Math.Pow(Math.Log2(myLog.p), 2));

            StateHasChanged();
        }

        public long StepBg(long a, long y, long p, long m, long k)
        {
            mHardPractice = 0; mHardTeory = 0;
            mStepBaby.value = new long[m];
            mStepBaby.index = new long[m];

            mStepBabyShow = new long[m];
            mStepGiant = new long[k + 1];
            //index = new long[m];

            for (int i = 0; i < m; i++)
            {
                //index[i] = i;
                mStepBaby.index[i] = i;
            }

            mStepBaby.value[0] = mStepBabyShow[0] = y;
            for (long i = 1; i < m; i++)
            {
                mStepBaby.value[i] = (Power.Pow(a, i, p) * y) % p;
                mStepBabyShow[i] = mStepBaby.value[i];
            }

            Sorting(mStepBaby, 0, m - 1);


            mStepGiant[0] = 1;
            for (long i = 1; i <= k; i++)
            {
                mStepGiant[i] = Power.Pow(a, i * m, p);
                mIBaby = BinSearch(mStepBaby, mStepGiant[i], 0, m);
                if (mIBaby > -1)
                {
                    mJGiant = i;
                    break;
                }
            }
            mCheck = mJGiant * m - mIBaby;
            return mCheck;
        }

        public void GenerateRandom()
        {
            var rand = new Random();
            myLog.a = rand.Next(2, MAX);

            int currentP;
            do
            {
                currentP = rand.Next(2, MAX);
            } while (!IsPrime(currentP));
            myLog.p = currentP;

            int currentY;
            do
            {
                currentY = rand.Next(1, MAX);
            } while (currentY >= myLog.p);
            myLog.y = currentY;

            myLog.m = (long)Math.Ceiling(Math.Sqrt((double)myLog.p));
            myLog.k = myLog.m;
        }

        static bool IsPrime(long p)
        {
            if (p <= 1) return false;

            int b = (int)Math.Pow(p, 0.5);

            for (int i = 2; i <= b; ++i)
            {
                if ((p % i) == 0) return false;
            }

            return true;
        }

        void Sorting(StepBaby arr, long first, long last)
        {
            long p = arr.value[(last - first) / 2 + first];
            long temp;
            long i = first, j = last;
            while (i <= j)
            {
                while (arr.value[i] < p && i <= last) ++i;
                while (arr.value[j] > p && j >= first) --j;
                mHardPractice++;
                if (i <= j)
                {
                    temp = arr.index[i];
                    arr.index[i] = arr.index[j];
                    arr.index[j] = temp;
                    mHardPractice += 6;
                    temp = arr.value[i];
                    arr.value[i] = arr.value[j];
                    arr.value[j] = temp;
                    ++i; --j;
                }
            }
            mHardPractice++;
            if (j > first) Sorting(arr, first, j);
            mHardPractice++;
            if (i < last) Sorting(arr, i, last);
        }

        long BinSearch(StepBaby array, long searchedValue, long left, long right)
        {
            while (left < right)
            {
                long middle = (left + right) / 2;
                mHardPractice++;
                if (searchedValue == array.value[middle])
                {
                    return array.index[middle];
                }
                else if (searchedValue < array.value[middle])
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle + 1;
                }
            }
            return -1;
        }
    }
}
