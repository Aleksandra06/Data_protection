using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Math;
using DataProtection.Engine.Managers;
using System.IO;

namespace DataProtection.Pages.RGZ_Slava
{
    public class Server
    {
        static int check = 0;

        private int bitLength = 512;
        private BigInteger p { get; set; }
        private BigInteger q { get; set; }
        private BigInteger g { get; set; }
        private BigInteger n { get; set; }
        private BigInteger v { get; set; }
        private int t { get; set; }
        private BigInteger x { get; set; }
        private int e { get; set; }
        private bool ControlCheck { get; set; }

        private string path = "Resource\\RGZ_Slava\\serverData.txt";
        
        private string pathList = "Resource\\RGZ_Slava\\serverList.txt";

        string pathDataPublicServer = "Resource\\RGZ_Slava\\publicData.txt";

        public Server()
        {
            if (read(path)) {
            } else {
                generate();
                saveData();
                savePublicData();
            }
        }

        public bool checkLogin(string login)
        {
            using (StreamReader reader = new StreamReader(pathList)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    string[] split = line.Split(" ");
                    if (split[0].Equals(login)) {
                        return true;
                    }
                }
            }
            return false;
        }

        public void savePublicData()
        {
            using (StreamWriter writer = new StreamWriter(pathDataPublicServer)) {
                writer.Write(n.ToString());
            }
        }
        private void saveData()
        {
            using (StreamWriter writer = new StreamWriter(path)) {
                writer.Write(n.ToString() + "\n");
                writer.Write(p.ToString() + "\n");
                writer.Write(q.ToString());
            }
        }
        bool read(string path)
        {
            using (StreamReader reader = new StreamReader(path)) {
                string line;
                List<BigInteger> numbers = new List<BigInteger>();
                int i = 0;
                while ((line = reader.ReadLine()) != null) {
                    numbers.Add(new BigInteger(line));
                    i++;
                }
                if (i == 3) {
                    n = numbers[0];
                    p = numbers[1];
                    q = numbers[2];
                    return true;
                } else {
                    return false;
                }

            }
        }
        public void saveClient(string login, BigInteger v)
        {
            using (StreamWriter writer = new StreamWriter(pathList, true)) {
                writer.Write(login + " " + v.ToString() + "\n");
            }
        }
        /*
        * Generate P and Q
        */
        private void generate()
        {
            EvklidBigInteger evklid = new EvklidBigInteger();
            BigInteger tmp_p, tmp_q;
            do {
                do {
                    tmp_p = BigInteger.ProbablePrime(bitLength, new Random());
                    tmp_q = BigInteger.ProbablePrime(bitLength, new Random());
                } while (!tmp_p.IsProbablePrime(10) && !tmp_q.IsProbablePrime(10));
            } while (evklid.gcd(tmp_q, tmp_p).CompareTo(BigInteger.One) != 0);
            p = tmp_p;
            q = tmp_q;
            n = q.Multiply(p);
            t = 10;
        }
        public bool connect(string login, BigInteger v)
        {
            using (StreamReader reader = new StreamReader(pathList)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    string[] split = line.Split(" ");
                    if (split[0].Equals(login)) {
                        if (split[1].Equals(v.ToString())) {
                            this.v = v;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public void connect(BigInteger v)
        {
            this.v = v;
        }
        public void responseX(BigInteger x)
        {
            this.x = x;
        }
        public int sendE()
        {
            e = new Random().Next(0, 1);
            return e;
        }
        public bool responseY(BigInteger y)
        {
            if (y.CompareTo(BigInteger.Zero) == 0) {
                check++;
                return false;
            }
            BigInteger left = y.Multiply(y);
            BigInteger right = MyModPowBigInteger.FastModuloExponentiation(
                                                    x.Multiply(MyModPowBigInteger.FastModuloExponentiation(v, new BigInteger(e.ToString()), n)),
                                                    BigInteger.One,
                                                    n);
            if (left.CompareTo(right) == 0) {
                return true;
            }
            return false;
        }
        public void setV(BigInteger v)
        {
            this.v = v;
        }
        public BigInteger getV()
        {
            return v;
        }
        public BigInteger getN()
        {
            return n;
        }
        public void setT(int t)
        {
            this.t = t;
        }

        public void deleteClient(string login)
        {
            List<string> allClient = new List<string>();
            using (StreamReader reader = new StreamReader(pathList)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    string[] split = line.Split(" ");
                    if (!split[0].Equals(login)) {
                        allClient.Add(line);
                    }
                }
            }
            using (StreamWriter writer = new StreamWriter(pathList)) {
                foreach (var iter in allClient) {
                    writer.Write(iter + "\n");
                }
            }
        }

        public int getT()
        {
            return t;
        }
    }
}
