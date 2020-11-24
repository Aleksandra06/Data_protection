using DataProtection.Engine.Managers;
using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;

namespace DataProtection.PageModels.Lab4
{
    public class Player
    {
        public int bitLength = 30;
        public BigInteger c { get; set; }
        public BigInteger d { get; set; }
        public List<BigInteger> cards { get; set; }   
        public Player(BigInteger p_prev)
        {
            EvklidBigInteger evklid = new EvklidBigInteger();
            do {
                c = BigInteger.ProbablePrime(bitLength, new Random());
                if (evklid.gcd(c, p_prev).CompareTo(BigInteger.One) == 0) {
                    if (evklid.mY.CompareTo(BigInteger.Zero) < 0) {
                        d = evklid.mY.Add(p_prev);
                    } else {
                        d = evklid.mY;
                    }
                    break;
                }
            } while (true);
        }
    }
}