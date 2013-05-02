using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace CAH
{
    public class Game
    {
        private List<Card> WhiteCardDeck=new List<Card>(),BlackCardDeck=new List<Card>();
        private Dictionary<string,int> players = new Dictionary<string,int>();
        private Server server = new Server();
        Client c;

        public Game()
        {
            setupDecks();
            c = new Client("game");
            c.connect("127.0.0.1");
            c.playerAdded += c_playerAdded;
            setupDecks();
            shuffleDecks();
        }

        void c_playerAdded(object sender, String e)
        {
            addPlayer(e);
        }

        private void setupDecks()
        {
            StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("CAH.WhiteCards.txt"));
            String read = "";
            while ((read = sr.ReadLine()) != null)
                WhiteCardDeck.Add(new Card(read,false));
            sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("CAH.BlackCards.txt"));
            read = "";
            while ((read = sr.ReadLine()) != null)
                BlackCardDeck.Add(new Card(read,true));

            
        }

        public void shuffleDecks()
        {
            shuffle(WhiteCardDeck);
            shuffle(BlackCardDeck);
        }

        private void shuffle(List<Card> list)
        {
                Random r = new Random();
                int i = list.Count;
                while (i > 1)
                {
                    i--;
                    int k = r.Next(i + 1);
                    Card c = list[i];
                    list[i] = list[k];
                    list[k] = c;

                }
        }

        public void addPlayer(String name)
        {
            name = name.TrimEnd('\0');
            players.Add(name, 0);
            Debug.WriteLine("player" + name + " has joined the game, there are now " + players.Count + " players in the game");
        }

        public void playerWin(string name)
        {
            int loc =players.Keys.ToList().BinarySearch(name);
            players.Values.ToArray()[loc]++;
        }

        public void deal()
        {
            for (int i = 0; i < 7; i++)
            {
                foreach (String s in players.Keys)
                {
                    c.sendACard(s.Trim(),WhiteCardDeck.First());
                    WhiteCardDeck.Remove(WhiteCardDeck.First());
                    

                }
            }
        }

        public void startGame(){
            c.gameSendStart();
            deal();
        }

        public String getScores()
        {
            string scores="";
            for (int i = 0; i < players.Count; i++)
            {
                scores += players.Keys.ToArray()[i];
                scores += ",";
                scores += players.Values.ToArray()[i];
                scores += ";";
            }
            return scores;
        }
    }
}
