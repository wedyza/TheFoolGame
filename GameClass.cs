using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TheFoolGame
{

    public enum Mark
    {
        Ruby = 1,
        Peaks = 2,
        Worms = 3,
        Crosses = 4
    }


    public static class CardOperations
    {
        private static Random rng = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        public static bool IsCardSameMark(Card card1, Card card2)
        {
            return card1.Mark == card2.Mark;
        }

        public static bool FightToCard(this Card mainCard, Card otherCard)
        {
            if (mainCard.IsSpecialMark())
            {
                if (IsCardSameMark(mainCard, otherCard))
                    return mainCard.Score > otherCard.Score;
                return true;
            }
            else
                return mainCard.Score > otherCard.Score;
        }

        public static List<Card> ShuffleDeck(this List<Card> deck)
        {
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }

            return deck;
        }
    }

    public class Card
    {
        int score;
        Mark mark;

        public int Score { get; }
        public Mark Mark { get; }

        public Card(int score, Mark mark)
        {
            Score = score;
            Mark = mark;
        }

        public bool IsSpecialMark() => Mark == GameMatch.SpecialCardMark;

        public override string ToString()
        {
            var showScore = "";
            if (Score == 11)
                showScore = "Валет";
            else if (Score == 12)
                showScore = "Дама";
            else if (Score == 13)
                showScore = "Король";
            else if (Score == 14)
                showScore = "Туз";
            if (showScore == "")
                return string.Format($"{Score}, {Mark}");
            else
                return string.Format($"{showScore}, {Mark}");

        }
    }

    public static class GameRules
    {
        static string rules = "";
    }

    public class Player
    {
        string name;
        BoxOfCards cardsOnHand;

        public Card Move(int numbersOfCard)
        {
            var thatCard = cardsOnHand.Cards[numbersOfCard-1];
            CardsOnHand.Cards.Remove(thatCard);
            return thatCard;
        }

        public void GetCards(BoxOfCards takenCards)
        {
            foreach (var i in takenCards.Cards)
                cardsOnHand.Cards.Add(i);
        }


        public string Name { get { return name; } }
        public BoxOfCards CardsOnHand { get { return cardsOnHand; } }

        public Player(string name)
        {
            this.name = name;
            cardsOnHand= new BoxOfCards();
        }
    }

    public static class GameMatch
    {
        static Mark specialCardMark;
        static List<Player> players;
        static bool status;
        
        public static void AddPlayers(params Player[] playersToAdd)
        {
            foreach(var i in playersToAdd)
                Players.Add(i);
        }

        public static void SetSpecialMark(Mark specialCardMark)
        {
            if (SpecialCardMark == 0)
                SpecialCardMark = specialCardMark;
        }

        public static string MatchEnd(Player player)
        {
            status = false;
            return string.Format($"The winner is {0}", player);
        }

        public static List<Player> Players { get; }
        public static Mark SpecialCardMark { get; set; }
        public static bool Status { get; }

        static GameMatch()
        {
            Status = true;
        }
    }

    public class Table
    {
        Deck deck;

        MoveCards player1Moves;
        MoveCards player2Moves;

        BoxOfCards beaten;

        public Deck Deck { get; set; }
        public MoveCards Player1Moves { get; set; }
        public MoveCards Player2Moves { get; set; }
        public BoxOfCards Beaten { get; set; }
        

        public Table()
        {
            Deck = new Deck();
            Beaten = new BoxOfCards();
        }
    }

    public class BoxOfCards
    {
        List<Card> cards;
        public int Length => cards.Count;

        public List<Card> Cards { get { return cards; } }

        public BoxOfCards()
        {
            cards = new List<Card>();
        }

        public void AddCards(Card[] theCards)
        {
            cards = new List<Card>();
            foreach (var i in theCards)
                cards.Add(i);
        }

        public BoxOfCards(params Card[] theCards)
        {
            AddCards(theCards);
        }

        public void ShowCards()
        {
            var cardNum = 0;
            foreach (var i in Cards)
            {
                cardNum++;
                Console.WriteLine($"{cardNum}-[{i}]");
            }
        }
    }

    public class Deck
    {
        public Stack<Card> cards;
        public int Length => Cards.Count;

        public Stack<Card> Cards { get; set; }

        public Card PopCard()
        {
            return Cards.Pop();
        }

        public Deck ()
        {
            var basicDeck = new List<Card>();
            var finalDeck = new Stack<Card>();
            for (int i = 6 ; i < 15; i++)
            {
                basicDeck.Add(new Card(i, Mark.Worms));
                basicDeck.Add(new Card(i, Mark.Peaks));
                basicDeck.Add(new Card(i, Mark.Ruby));
                basicDeck.Add(new Card(i, Mark.Crosses));
            }

            basicDeck = basicDeck.ShuffleDeck();

            foreach (var i in basicDeck)
                finalDeck.Push(i);

            Cards = finalDeck;
        }
    }

    public class MoveCards
    {
        List<Card> moves;
        Player connectedPlayer;

        public List<Card> Moves { get; set; }
        public Player ConnectedPlayer { get; set; }

        public string ShowCards()
        {
            string returning = "";
            foreach (var i in moves)
                returning += string.Format($"[{i}] ");
            return returning;
        }
        public MoveCards(Player player)
        {
            moves = new List<Card>();
            connectedPlayer = player;
        }
    }
}
