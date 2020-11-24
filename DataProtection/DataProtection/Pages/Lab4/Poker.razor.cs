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
        protected bool IsShowTable = false;
        protected override void OnInitialized()
        {
            Model.cards = new Dictionary<BigInteger, string>();
            string filename = "Resource/text_card.txt";
            try {
                using (StreamReader reader = new StreamReader(filename)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        Model.cards.Add(new BigInteger(Math.Abs(line.GetHashCode()).ToString()), line);
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            Model.deck = new List<BigInteger>();
            foreach (BigInteger i in Model.cards.Keys) {
                Model.deck.Add(i);
            }
            Model.players = new LinkedList<Player>();
            base.OnInitialized();
        }
        protected void Calc()
        {
            Model.players = new LinkedList<Player>();
            Model.deck = new List<BigInteger>();
            foreach (BigInteger i in Model.cards.Keys) {
                Model.deck.Add(i);
            }

            BigInteger tmp;
            do {
                tmp = BigInteger.ProbablePrime(40, new Random());
            } while (!tmp.IsProbablePrime(50));
            Model.p = tmp;
            Model.p_prev = tmp.Subtract(BigInteger.One);
        }
        public void generate()
        {
            Model.players = new LinkedList<Player>();
            Model.deck = new List<BigInteger>();
            foreach (BigInteger i in Model.cards.Keys) {
                Model.deck.Add(i);
            }

            BigInteger tmp;
            do {
                tmp = BigInteger.ProbablePrime(40, new Random());
            } while (!tmp.IsProbablePrime(50));
            Model.p = tmp;
            Model.p_prev = tmp.Subtract(BigInteger.One);
            Random random = new Random();
            Model.CountPeople = random.Next(2, 23);
        }

        public void compute()
        {
            fillPlayers();
            encrypt();
            foreach (Player player in Model.players) {
                takeCards(player);
            }

            for (int i = 0; i < 3; i++) {
                takeCardReferee();
            }
        }

        public void takeCardReferee()
        {
            Player referee = Model.players.Last();
            foreach (Player p_iter in Model.players) {
                if (p_iter == referee) {
                    continue;
                }
                Model.deck[0] = MyModPowBigInteger.FastModuloExponentiation(Model.deck[0], p_iter.d, Model.p);
            }

            referee.cards.Add(MyModPowBigInteger.FastModuloExponentiation(Model.deck[0], referee.d, Model.p));
            Model.deck.RemoveAt(0);
        }

        public void takeCards(Player player)
        {
            List<BigInteger> cards = new List<BigInteger>();
            foreach (Player p_iter in Model.players) {
                if (p_iter == player) {
                    continue;
                }
                Model.deck[0] = MyModPowBigInteger.FastModuloExponentiation(Model.deck[0], p_iter.d, Model.p);
                Model.deck[1] = MyModPowBigInteger.FastModuloExponentiation(Model.deck[1], p_iter.d, Model.p);
            }
            cards.Add(MyModPowBigInteger.FastModuloExponentiation(Model.deck[0], player.d, Model.p));
            cards.Add(MyModPowBigInteger.FastModuloExponentiation(Model.deck[1], player.d, Model.p));
            Model.deck.RemoveAt(0);
            Model.deck.RemoveAt(0);

            player.cards = cards;
        }

        public void encrypt()
        {
            foreach (Player player in Model.players) {
                Model.deck = Model.deck.Select(x => MyModPowBigInteger.FastModuloExponentiation(x, player.c, Model.p)).ToList();
                //Model.deck = Model.deck.OrderBy().ToList();
                BigInteger min = Model.deck[0];
                int num = 0;
                for (int i = 0; i < Model.deck.Count; i++) {
                    min = Model.deck[i];
                    num = i;
                    for (int j = i + 1; j < Model.deck.Count - 1; j++) {
                        if (min.CompareTo(Model.deck[j]) > 0) {
                            min = Model.deck[j];
                            num = j;
                        }
                    }
                    BigInteger tmp = Model.deck[i]; Model.deck[i] = Model.deck[num]; Model.deck[num] = tmp;
                }
            }
        }

        public void fillPlayers()
        {
            for (int i = 0; i < Model.CountPeople + 1; i++) {
                Model.players.AddLast(new Player(Model.p_prev));
            }
        }
    }
}
