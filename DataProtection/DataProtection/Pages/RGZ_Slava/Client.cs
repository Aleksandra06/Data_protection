using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Math;
using DataProtection.Engine.Managers;
using System.IO;

namespace DataProtection.Pages.RGZ_Slava
{
    public class Client
    {
        private int bitLength = 510;
        private BigInteger n { get; set; }
        private BigInteger s { get; set; }
        private BigInteger v { get; set; }
        private BigInteger r { get; set; }
        private BigInteger y { get; set; }
        private int e { get; set; }
        private string login { get; set; }

        private string pathDataPublicServer = "Resource\\RGZ_Slava\\publicData.txt";

        private string pathDataPublicClient = "Resource\\RGZ_Slava\\publicDataClient.txt";

        private string pathPasswords = "Resource\\RGZ_Slava\\clientPasswords.txt";

        public Client(string login, string password, Server server) {
        }
        public Client()
        {
            read(pathDataPublicClient);
        }
        public void saveLogin(string login)
        {
            this.login = login;
        }
        public void generateV(string password)
        {
            if (password == null || password == "" || !checkOnSymbol(password)) {
                s = new BigInteger("2");
            } else {
                s = new BigInteger(password);
            }
            v = MyModPowBigInteger.FastModuloExponentiation(s, BigInteger.Two, n);
        }

        private bool checkOnSymbol(string password)
        {
            for (int i = 0; i < password.Length; i++) {
                if (password[i] > '9' || password[i] < '0') {
                    return false;
                }
            }
            return true;
        }

        public string getLogin()
        {
            return login;
        }
        public void connect(BigInteger n)
        {
            this.n = n;
        }
        public void generateSV()
        {
            EvklidBigInteger evklid = new EvklidBigInteger();
            BigInteger tmp_s;
            do {
                tmp_s = BigInteger.ProbablePrime(bitLength, new Random());
            } while (!tmp_s.IsProbablePrime(10) && evklid.gcd(tmp_s, n).CompareTo(BigInteger.One) != 0);
            s = tmp_s;
            v = MyModPowBigInteger.FastModuloExponentiation(s, BigInteger.Two, n);
        }
        public void savePublicData()
        {
            using (StreamWriter writer = new StreamWriter(pathDataPublicClient)) {
                writer.Write(n.ToString());
            }
        }
        public bool read(string path)
        {
            string clientData;
            using (StreamReader reader = new StreamReader(path)) {
                clientData = reader.ReadLine();
            }
            if (clientData != null && !clientData.Equals("")) {
                n = new BigInteger(clientData);
                return true;
            }
            return false;
        }
        public bool checkNaboutServer(Server server)
        {
            string clientData, serverData;
            using (StreamReader reader = new StreamReader(pathDataPublicClient)) {
                clientData = reader.ReadLine();
            }
            using (StreamReader reader = new StreamReader(pathDataPublicServer)) {
                serverData = reader.ReadLine();
            }
            if (clientData != null && serverData != null && !clientData.Equals("") && !serverData.Equals("") && clientData.Equals(serverData)) {
                n = new BigInteger(clientData);
                return true;
            }
            return false;
        }
        public void savePassword()
        {
            using (StreamWriter writer = new StreamWriter(pathPasswords, true)) {
                writer.Write(login + " " + s.ToString() + "\n");
            }
        }
        private void generateR()
        {
            r = BigInteger.ProbablePrime(bitLength - 1, new Random());
        }
        public BigInteger sendX()
        {
            generateR();
            return MyModPowBigInteger.FastModuloExponentiation(r, BigInteger.Two, n);
        }
        public void responseE(int e)
        {
            this.e = e;
        }
        public BigInteger sendY()
        {
            if (e == 0) {
                return r;
            } else if (e == 1) {
                return MyModPowBigInteger.FastModuloExponentiation(
                            r.Multiply(MyModPowBigInteger.FastModuloExponentiation(s, BigInteger.One, n)), 
                            BigInteger.One,
                            n);
            }
            return BigInteger.Zero;
        }
        private void setN(BigInteger n)
        {
            this.n = n;
        }
        public BigInteger getV()
        {
            return v;
        }
        public BigInteger getS()
        {
            return s;
        }
        public void deletePassword()
        {
            List<string> allClient = new List<string>();
            using (StreamReader reader = new StreamReader(pathPasswords)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    string[] split = line.Split(" ");
                    if (!split[0].Equals(login)) {
                        allClient.Add(line);
                    }
                }
            }
            using (StreamWriter writer = new StreamWriter(pathPasswords)) {
                foreach (var iter in allClient) {
                    writer.Write(iter + "\n");
                }
            }
        }
    }
}
