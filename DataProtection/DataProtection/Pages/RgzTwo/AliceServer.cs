using System;
using System.Collections.Generic;
using System.Linq;
using Org.BouncyCastle.Math;
using System.Threading.Tasks;
using DataProtection.Engine.Managers;

namespace DataProtection.Pages.RgzTwo
{
    public class AliceServer
    {
        public int N, M;
        private List<int> mGamilton = new List<int>();
        public Graph G = new Graph();
        private Graph H = new Graph();
        private Graph H1 = new Graph();
        public Graph F = new Graph();
        public List<Edge> mEdges { get; set; }

        public BigInteger Nchifr { get; set; }
        public BigInteger dchifr { get; set; }

        public List<int> Numeric;

        public Graph LookGraph(string str)
        {
            mGamilton = new List<int>();
            G = new Graph();
            mEdges = new List<Edge>();

            var strTmp = GetDataFromFile(str);
            SetGemilton(strTmp);
            CreateMatrixGraphG();
            CreateF();

            return G;
        }
        private string GetDataFromFile(string str)
        {
            int n, m;
            str = str.Replace("\r", "");
            var strTmp = str.Substring(0, str.IndexOf("\n"));
            var indexNumber = str.IndexOf(",") >= 0 ? str.IndexOf(",") : str.IndexOf(" ");
            var strNumber = str.Substring(0, indexNumber);
            n = Convert.ToInt32(strNumber);
            strNumber = strTmp.Substring(indexNumber + 1).Replace(" ", "");
            m = Convert.ToInt32(strNumber);
            if (n >= 1001 || m >= Math.Pow(n, 2) || n <= 0 || m <= 0)
            {
                throw new Exception("ошибочные данные в первой строке!");
            }

            strTmp = str.Substring(str.IndexOf("\n") + 1).Replace(" ", "");
            for (int i = 0; i < m; i++)
            {
                string strStr;
                var indexStr = strTmp.IndexOf("\n");
                strStr = strTmp.Substring(0, indexStr).Replace(" ", "");
                strTmp = strTmp.Substring(indexStr + 1);
                var tmpMas = strStr.Split(",");
                mEdges.Add(new Edge()
                {
                    Vertex1 = Convert.ToInt32(tmpMas[0]),
                    Vertex2 = Convert.ToInt32(tmpMas[1])
                });
            }

            N = n;
            M = m;
            return strTmp;
        }
        private void SetGemilton(string strTmp)
        {
            strTmp = strTmp.Replace("\n", "");
            var tmpMas2 = strTmp.Split(",");
            for (int i = 0; i < tmpMas2.Length; i++)
            {
                mGamilton.Add(Convert.ToInt32(tmpMas2[i]));
            }
        }

        private void CreateMatrixGraphG()
        {
            G = InitGraph(N);
            G.M = mEdges.Count;
            foreach (var edge in mEdges)
            {
                G.Data[edge.Vertex1 - 1][edge.Vertex2 - 1] = new BigInteger("1");
                G.Data[edge.Vertex2 - 1][edge.Vertex1 - 1] = new BigInteger("1");
            }
        }

        public Graph InitGraph(int n)
        {
            var g = new Graph();
            g.N = N;
            g.Data = new List<List<BigInteger>>();
            for (int i = 0; i < n; i++)
            {
                g.Data.Add(new List<BigInteger>());
                for (int j = 0; j < n; j++)
                {
                    g.Data.LastOrDefault()?.Add(new BigInteger("0"));
                }
            }

            return g;
        }

        private void CreateF()
        {
            Izomorfizaziya();
            AddNumber();
            ShifrRSA();
        }
        private void Izomorfizaziya()
        {
            Random rnd = new Random();
            List<int> a = new List<int>();
            for (int j = 0; j < N; j++)
            {
                a.Add(j);
            }
            for (int i = 0; i < a.Count; i++)
            {
                int tmp = a[0];
                a.RemoveAt(0);
                a.Insert(rnd.Next(a.Count), tmp);
            }

            H = InitGraph(N);
            H.M = mEdges.Count;
            foreach (var edge in mEdges)
            {
                H.Data[a[edge.Vertex1 - 1]][a[edge.Vertex2 - 1]] = new BigInteger("1");
                H.Data[a[edge.Vertex2 - 1]][a[edge.Vertex1 - 1]] = new BigInteger("1");
            }

            Numeric = a;
        }
        private void AddNumber()
        {
            Random rnd = new Random();
            H1 = InitGraph(N);
            H1.M = M;
            for (var i = 0; i < H.Data.Count; i++)
            {
                var str = H.Data[i];
                for (var index = 0; index < str.Count; index++)
                {
                    var r = (rnd.Next(5) + 1).ToString();
                    H1.Data[i][index] = new BigInteger(r + str[index].ToString());
                }
            }
        }

        private void ShifrRSA()
        {
            //if (F.CompareTo(d) > 0)
            //{
            //    if (evklid.mY.CompareTo(BigInteger.Zero) < 0)
            //    {
            //        evklid.mY = evklid.mY.Add(F);
            //    }
            //    c = evklid.mY;
            //}
            //else
            //{
            //    if (evklid.mX.CompareTo(BigInteger.Zero) < 0)
            //    {
            //        evklid.mX = evklid.mX.Add(F);
            //    }
            //    c = evklid.mX;
            //}

            //
            this.F = InitGraph(this.N);
            this.F.M = this.M;
            for (var i = 0; i < H1.Data.Count; i++)
            {
                var str = H1.Data[i];
                for (var index = 0; index < str.Count; index++)
                {
                    this.F.Data[i][index] = str[index].ModPow(dchifr, Nchifr);
                }
            }
        }

        public void SetKey(BigInteger n, BigInteger d)
        {
            Nchifr = n;
            dchifr = d;
        }

        public Tuple<List<int>, Graph> DokazyIzomorf()
        {
            return new Tuple<List<int>, Graph>(Numeric, H1);
        }

        public List<EdgeCod> DokazyGamiltonov()
        {
            var egdeCod = CreateEdgeCod();
            return egdeCod;
        }

        private List<EdgeCod> CreateEdgeCod()
        {
            var edgeCod = new List<EdgeCod>();
            for (var index = 0; index < mGamilton.Count - 1; index++)
            {
                var newvertix1 = Numeric[mGamilton[index] - 1];
                var newvertix2 = Numeric[mGamilton[index + 1] - 1];
                var data = H1.Data[newvertix1][newvertix2];
                edgeCod.Add(new EdgeCod()
                {
                    Data = data,
                    Vertix1 = newvertix1,
                    Vertix2 = newvertix2
                });
            }

            return edgeCod;
        }
    }
}
