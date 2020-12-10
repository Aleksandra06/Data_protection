using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorInputFile;
using DataProtection.Engine.Managers;
using DataProtection.PageModels;
using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Math;

namespace DataProtection.Pages.RgzTwo
{
    public class RgzTwoViewModel : ComponentBase
    {
        [Inject] public AliceServer Server { get; set; }
        protected List<string> FileErrorsList { get; set; }
        protected DocumentModel Document { get; set; }

        protected bool mIsReadFile = false;

        protected string StatusString = "";
        //-----
        public Graph mGraph = new Graph();
        public Graph mF = new Graph();
        //-----
        //protected int NumAnswer = 1;
        //protected List<int> Variant { get; set; } = new List<int> { 1, 2 };
        //key---
        BigInteger d, N;
        BigInteger P, Q, F, c;
        //----

        protected async void SetData(IFileListEntry[] files)
        {
            try
            {
                StatusString = "";
                await HandleFileSelected(files);
                string textFromFile = System.Text.Encoding.Default.GetString(Document.Data);
                CreateKey();
                Server.SetKey(N, d);
                mGraph = Server.LookGraph(textFromFile);
                mF = Server.F;
                StateHasChanged();
            }
            catch (Exception e)
            {
                if (!string.IsNullOrEmpty(e.Message))
                {
                    FileErrorsList.Add($"Ошибка: {e.Message}");
                }
            }
        }

        protected async Task HandleFileSelected(IFileListEntry[] files)
        {
            FileErrorsList = new List<string>();
            mIsReadFile = false;

            if (files != null && files.Count() > 0)
            {
                var file = files.FirstOrDefault();
                try
                {
                    var doc = await UploadFile(file);
                    if (doc != null)
                    {
                        Document = doc;
                    }
                    mIsReadFile = true;
                }
                catch (Exception e)
                {
                    FileErrorsList.Add($"Файл: {file.Name} не загружен, потому что {e.Message}");
                    throw new Exception();
                }
                finally
                {
                    StateHasChanged();
                }
            }
            StateHasChanged();
        }

        private async Task<DocumentModel> UploadFile(IFileListEntry file)
        {
            if (file != null)
            {
                byte[] result;
                MemoryStream sourceStream = await file.ReadAllAsync((int)file.Size);
                result = new byte[file.Data.Length];
                await sourceStream.ReadAsync(result, 0, (int)file.Data.Length);

                DocumentModel doc = new DocumentModel()
                {
                    FileName = file.Name,
                    Data = result,
                    ContentType = file.Type
                };

                return doc;

            }
            return null;
        }

        //protected void ChangeAnswer(ChangeEventArgs e)
        //{
        //    NumAnswer = int.Parse(e.Value.ToString());
        //}

        protected void CreateKey()
        {
            EvklidBigInteger evklid = new EvklidBigInteger();
            Random Rand = new Random();
            int bit = 512;
            do
            {
                Q = BigInteger.ProbablePrime(bit, new Random());
                //Q = new BigInteger(Rand.Next(1, (int)Math.Pow(10, 4)), Rand);
            } while (Q.IsProbablePrime(1 << 10) == false);

            do
            {
                P = BigInteger.ProbablePrime(bit, new Random());
                //P = new BigInteger(Rand.Next(1, (int)Math.Pow(10, 4)), Rand);
            } while (P.IsProbablePrime(1 << 10) == false);
            N = P.Multiply(Q);
            F = (P.Subtract(BigInteger.One)).Multiply((Q.Subtract(BigInteger.One)));
            while (true)
            {
                var dCount = Rand.Next(1, F.ToString().Length - 1);
                d = new BigInteger(dCount, Rand);
                var gcd = F.Gcd(d); // max, min
                if (gcd.CompareTo(BigInteger.One) == 0)
                {
                    break;
                }
            }

            c = d.ModInverse(F);
        }

        protected async void Dokazatelstvo()
        {
            StatusString = "";
            Random rand = new Random();
            var count = rand.Next(3, 20);
            for (int i = 0; i < count; i++)
            {
                var num = rand.Next(2) + 1;
                switch (num)
                {
                    case 1:
                        var answerIz = Server.DokazyIzomorf();
                        if (!CheckIzomorf(answerIz.Item1, answerIz.Item2))
                        {
                            StatusString = "Алиса не прошла проверку";
                            return;
                        }
                        break;
                    case 2:
                        var answerG = Server.DokazyGamiltonov();
                        if (!CheckGamiltonov(answerG))
                        {
                            StatusString = "Алиса не прошла проверку";
                            return;
                        }
                        break;
                    default: break;
                }
            }
            StatusString = "Алиса прошла проверку";
        }

        private bool CheckGamiltonov(List<EdgeCod> answerG)
        {
            foreach (var edge in answerG)
            {
                var cod = mF.Data[edge.Vertix1][edge.Vertix2];
                var decod = cod.ModPow(c, N);
                if (decod.CompareTo(edge.Data) != 0)
                {
                    return false;
                }
            }

            var numbers1 = new List<int>();
            for (int i = 0; i < mGraph.N; i++)
            {
                numbers1.Add(i);
            }
            var numbers2 = numbers1.ToList();

            foreach (var edge in answerG)
            {
                if (!numbers1.Any(x => x == edge.Vertix1))
                {
                    return false;
                }

                numbers1.Remove(edge.Vertix1);

                if (!numbers2.Any(x => x == edge.Vertix2))
                {
                    return false;
                }

                numbers2.Remove(edge.Vertix2);
            }

            return true;
        }

        private bool CheckIzomorf(List<int> numer, Graph h1)
        {
            for (var i = 0; i < h1.Data.Count; i++)
            {
                var str = h1.Data[i];
                for (var index = 0; index < str.Count; index++)
                {
                    var decod = mF.Data[i][index].ModPow(c, N);
                    if (decod.CompareTo(h1.Data[i][index]) != 0)
                    {
                        return false;
                    }
                }
            }

            var g1 = Server.InitGraph(h1.N);
            g1.M = h1.M;
            foreach (var edge in Server.mEdges)
            {
                g1.Data[numer[edge.Vertex1 - 1]][numer[edge.Vertex2 - 1]] = new BigInteger("1");
                g1.Data[numer[edge.Vertex2 - 1]][numer[edge.Vertex1 - 1]] = new BigInteger("1");
            }

            for (var i = 0; i < h1.Data.Count; i++)
            {
                var str = h1.Data[i];
                for (var index = 0; index < str.Count; index++)
                {
                    var num = new BigInteger(str[index].ToString().Substring(1));
                    if (num.CompareTo(g1.Data[i][index]) != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}