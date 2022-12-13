using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFoolGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameMatch.SetSpecialMark(Mark.Worms);

            var table = new Table();

            Console.WriteLine("Введите ник первого игрока");
            var Player1 = new Player(Console.ReadLine());
            Console.WriteLine("Введите ник второго игрока");
            var Player2 = new Player(Console.ReadLine());

            Player bufferPlayer;

            for (int i = 0; i < 6; i++)
            {
                Player1.GetCards(new BoxOfCards(table.Deck.PopCard()));
                Player2.GetCards(new BoxOfCards(table.Deck.PopCard()));
            }

            while (GameMatch.Status == true)
            {
                Console.WriteLine($"Верхняя карта колоды:  [{table.Deck.Cards.Peek()}], в колоде осталось [{table.Deck.Length} карты ].");

                Console.WriteLine($"\n\n {Player2.Name}, кол-во карт в руке: {Player2.CardsOnHand.Cards.Count} \n");
                Console.WriteLine($"Ходы {Player2.Name}: {table.Move2} ");

                Console.WriteLine($"Ходы {Player1.Name}: {table.Move1}");
                Console.WriteLine($"{Player1.Name}, кол-во карт в руке: {Player1.CardsOnHand.Length} \n Карты в руке:");
                foreach(var i in Player1.CardsOnHand.Cards)
                    Console.WriteLine($"[{i}]");
                Console.ReadLine();
                Console.Clear();ss
            }
        }
    }
}
