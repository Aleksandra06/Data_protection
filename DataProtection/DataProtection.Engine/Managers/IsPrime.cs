﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using DataProtection.Engine.Managers;

namespace DataProtection.Engine.Managers
{
    public static class IsPrime
    {
        public static bool isPrime(long prime, int k)
        {
            Random rand = new Random();
            Evklid myAlg = new Evklid();
            MyModPow modPow = new MyModPow();
            if (prime == 2) return true;
            if (prime % 2 == 0) return false;
            for (int i = 0; i < k; i++) {
                long a = rand.Next(1, (int)prime - 1);
                if (myAlg.gcd(a, prime) != 1 || modPow.Pow(a, prime - 1, prime) != 1) {
                    return false;
                }
            }

            return true;
        }
    }
}
