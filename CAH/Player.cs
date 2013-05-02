using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAH
{


    class Player
    {
        private String name = "";
        private Client client;
        private List<Card> cardsInHand = new List<Card>(5);
        public event EventHandler onACardSelected, cardChange;

        public Player(String name)
        {
            client = new Client(name);
            client.recivedWhiteCard += client_recivedWhiteCard;
        }

        void client_recivedWhiteCard(object sender, EventArgs e)
        {
            givePlayerCard(client.getLatestCard());
        }


        public void setName(String s){
            name=s;
        }
        public List<Card> getCards()
        {
            return cardsInHand;
        }

        public void givePlayerCard(Card c)
        {
           
            cardsInHand.Add(c);
            if(c != null)
            c.onSelected += c_onSelected;
            if (new Random().NextDouble() > .5)
                c.flip();
            //cardChange(this, EventArgs.Empty);
        }



        void c_onSelected(object sender, EventArgs e)
        {
            Card c = (Card) sender;
        }

        public Client getClient()
        {
            return client;
        }
    }
}
