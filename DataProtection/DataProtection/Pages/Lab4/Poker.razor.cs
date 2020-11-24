using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataProtection.PageModels.Lab4;
using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Math;
using DataProtection.Engine.Managers;

namespace DataProtection.Pages.Lab4
{
    public class PokerViewModel : ComponentBase
    {
        protected PockerModel Model { get; set; } = new PockerModel();
        protected override void OnInitialized()
        {
            Model.cards = new Dictionary<int, string>();
            string filename = "Resource/text_card.txt";
            try {
                using (StreamReader reader = new StreamReader(filename)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        Model.cards.Add(line.GetHashCode(), line);
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            Model.deck = new List<BigInteger>();
            foreach (int i in Model.cards.Keys) {
                Model.deck.Add(new BigInteger(i.ToString()));
            }
            Model.players = new LinkedList<Player>();
            base.OnInitialized();
        }

        public void generate()
        {
            BigInteger tmp;
            do {
                tmp = BigInteger.ProbablePrime(40, new Random());
            } while (!tmp.IsProbablePrime(50));
            Model.p = tmp;
            Model.p_prev = tmp.Subtract(BigInteger.One);
            Random random = new Random();
            Model.CountPeople = random.Next(2, 9);

            fillPlayers();
            encrypt();
        }

        private void encrypt()
        {
            MyModPowBigInteger myMod = new MyModPowBigInteger();
            foreach (Player player in Model.players) {
                for (int i = 0; i < Model.deck.Count; i++) {
                    //Model.deck[i] = 
                }
            }
            throw new NotImplementedException();
        }

        public void fillPlayers()
        {
            for (int i = 0; i < Model.CountPeople + 1; i++) {
                Model.players.AddLast(new Player(Model.p_prev));
            }
        }
    }
}
