using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Math;

namespace DataProtection.Engine.Managers
{
    public static class CompareBigInteger
    {
        public static bool Comparer(BigInteger a, BigInteger b)
        {
            if (a.CompareTo(b) < 0) {
                return true;
            } else if (a.CompareTo(b) >= 0) {
                return false;
            }
            return false;
        }
    }
}
