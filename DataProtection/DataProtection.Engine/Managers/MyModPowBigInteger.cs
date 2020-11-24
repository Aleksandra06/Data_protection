using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Math;

namespace DataProtection.Engine.Managers
{
    public class MyModPowBigInteger
    {
        public static BigInteger FastModuloExponentiation(BigInteger a, BigInteger x, BigInteger p)
        {
            //return a.modPow(x, p);
            BigInteger y = new BigInteger("1");
            BigInteger s = new BigInteger(a.ToString());
            String binary = x.ToString(2);

            for (int i = binary.Length - 1; i >= 0; i--) {
                //49 символ = единице
                var t = binary[i];
                if (binary[i] == 49) y = (y.Multiply(s)).Mod(p);
                s = (s.Multiply(s)).Mod(p);
            }

            return y;
        }
    }
}
