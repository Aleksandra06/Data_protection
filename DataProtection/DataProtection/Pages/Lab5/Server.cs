using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataProtection.PageModels.Lab5;
using Org.BouncyCastle.Math;
using DataProtection.Engine.Managers;

namespace DataProtection.Pages.Lab5
{
    public class Server
    {
        ServerModel server = new ServerModel();
        private BigInteger checkRight, checkLeft; 
        public Server()
        {
            server.bitLength = 1024;
            generate();
        }

        private void generate()
        {
            EvklidBigInteger evklid = new EvklidBigInteger();

            do {
                server.q = BigInteger.ProbablePrime(server.bitLength, new Random());
            } while (!server.q.IsProbablePrime(10));

            do {
                server.p = BigInteger.ProbablePrime(server.bitLength, new Random());
            } while (!server.p.IsProbablePrime(10));

            server.n = server.q.Multiply(server.p);

            server.f = (server.p.Subtract(BigInteger.One).Multiply(server.q.Subtract(BigInteger.One)));

            while (true) {
                server.d = BigInteger.ProbablePrime(server.bitLength, new Random());
                if (evklid.gcd(server.d, server.f).CompareTo(BigInteger.One) == 0) {
                    if (evklid.mY.CompareTo(BigInteger.Zero) < 0) {
                        server.c = evklid.mY.Add(server.f);
                    } else {
                        server.c = evklid.mY;
                    }
                    break;
                }
            }
        }
        public BigInteger getN()
        {
            return server.n;
        }
        public BigInteger getD()
        {
            return server.d;
        }

        public BigInteger getTicket(BigInteger _h)
        {
            return MyModPowBigInteger.FastModuloExponentiation(_h, server.c, server.n);
        }

        public bool checkVoice(BigInteger voice_n, BigInteger voice_s)
        {
            checkLeft = new BigInteger(Math.Abs(voice_n.GetHashCode()) + "");
            checkRight = new BigInteger(MyModPowBigInteger
                                .FastModuloExponentiation(voice_s, server.d, server.n) + "");
            if (checkLeft.CompareTo(checkRight) == 0) {
                return true;
            }
            return false;
        }

        public BigInteger getP()
        {
            return server.p;
        }
        public BigInteger getQ()
        {
            return server.q;
        }
        public BigInteger getC()
        {
            return server.c;
        }
        public BigInteger getF()
        {
            return server.f;
        }
        public BigInteger getCheckLeft()
        {
            return checkLeft;
        }
        public BigInteger getCheckRight()
        {
            return checkRight;
        }

        public void freeCheckLeft()
        {
            checkLeft = null;
        }
        public void freeCheckRight()
        {
            checkRight = null;
        }


    }
}
