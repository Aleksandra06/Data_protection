using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using DataProtection.PageModels.Lab5;
using DataProtection.Engine.Managers;
using Org.BouncyCastle.Math;
using System.IO;

namespace DataProtection.Pages.Lab5
{
    public class VoiceViewModel : ComponentBase
    {
        protected int Voise { get; set; } = 1;
        protected string Text = "";
        protected string titleName = "Введите имя";
        protected string checkResult = "Голосуйте, мы совсем не мешаем";
        
        public VoiceModel voice = new VoiceModel();
        public Server server = new Server();

        Dictionary<int, string> key = new Dictionary<int, string> {
            { 1, "Да" },
            { 2, "Нет" },
            { 3, "Воздержался" }
        };

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected void Vote()
        {
            EvklidBigInteger evklid = new EvklidBigInteger();

            if (string.IsNullOrEmpty(voice.name)) {
                titleName = "Введите имя";
                checkResult = "Голосуйте, мы вам не мешаем";
                destruct();
                return;
            } else {
                titleName = "Здравствуйте, " + voice.name;
            }

            if (Voise == 0)
            {
                Text = "Введите свой голос!";
                return;
            }

            string filename = "Resource/people.txt";
            try {
                using (StreamReader reader = new StreamReader(filename)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        if (voice.name == line) {
                            checkResult = "Не голосуйте повторно";
                            return;
                        }
                    }
                    reader.Close();
                }
                using (StreamWriter writer = new StreamWriter(filename, true)) {
                    string line = voice.name + "\n";
                    writer.Write(line);
                    writer.Close();
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }


            voice.bitLength = 512;
            voice.n = BigInteger.ProbablePrime(voice.bitLength, new Random());

            int h_key = key[Voise].GetHashCode();
            voice.n = voice.n.Add(new BigInteger(h_key.ToString()));

            BigInteger tmp_r;
            do {
                tmp_r = BigInteger.ProbablePrime(32, new Random());
            } while (evklid.gcd(tmp_r, server.getN()).CompareTo(BigInteger.One) != 0);
            voice.r = tmp_r;

            voice.h = new BigInteger(Math.Abs(voice.n.GetHashCode()) + "");
            voice._h = voice.h.Multiply(MyModPowBigInteger.FastModuloExponentiation(voice.r, server.getD(), server.getN()));

            voice._s = server.getTicket(voice._h);
            voice.s = voice._s.Multiply(voice.r.ModInverse(server.getN()));


            if (server.checkVoice(voice.n, voice.s)) {
                checkResult = "Благодарим, вы успешно проголосовали";
            } else {
                checkResult = "Ошибка";
            }

            filename = "Resource/resultVoises.txt";
            try {
                using (StreamReader reader = new StreamReader(filename)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        string lineInput = voice.n.ToString() + " " + voice.s.ToString();
                        if (lineInput == line) {
                            checkResult = "У разных Имен совпали подписи";
                            return;
                        }
                    }
                    reader.Close();
                }
                using (StreamWriter writer = new StreamWriter(filename, true)) {
                    string line = voice.n + " " + voice.s + "\n";
                    writer.Write(line);
                    writer.Close();
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

        }

        private void destruct()
        {
            voice.n = null;
            voice.h = null;
            voice._h = null;
            voice.r = null;
            voice.s = null;
            voice._s = null;
            server.freeCheckLeft();
            server.freeCheckRight();
        }
    }
}
