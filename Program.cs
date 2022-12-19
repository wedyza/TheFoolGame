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
            Console.ForegroundColor = ConsoleColor.Green;

            GameMatch.SetSpecialMark(Mark.Worms);

            var table = new Table();
            Console.WriteLine("Введите ник первого игрока");
            var Player1 = new Player(Console.ReadLine());
            Console.WriteLine("Введите ник второго игрока");
            var Player2 = new Player(Console.ReadLine());


            table.Player1Moves = new MoveCards(Player1);
            table.Player2Moves = new MoveCards(Player2);

            Player bufferPlayer;

            bool moveType = true;

            for (int i = 0; i < 6; i++)
            {
                Player1.GetCards(new BoxOfCards(table.Deck.PopCard()));
                Player2.GetCards(new BoxOfCards(table.Deck.PopCard()));
            }

            while (GameMatch.Status == true)
            {
                Console.Clear();
                Console.WriteLine($"Верхняя карта колоды:  [{table.Deck.Cards.Peek()}], в колоде осталось [{table.Deck.Length} карты ].");

                Console.WriteLine($"\n{Player2.Name}, кол-во карт в руке: {Player2.CardsOnHand.Cards.Count} \n");

                Console.WriteLine($"Ходы {Player2.Name}: {table.Player2Moves.ShowCards()} ");
                Console.WriteLine($"Ходы {Player1.Name}: {table.Player1Moves.ShowCards()} \n");

                Console.WriteLine($"{Player1.Name}, кол-во карт в руке: {Player1.CardsOnHand.Length} \n   Карты в руке:");

                Player1.CardsOnHand.ShowCards();
                if (moveType)
                {
                    Console.WriteLine("Выберите карту для хода (0, чтобы закончить ход):");
                    int choose = int.Parse(Console.ReadLine());
                    if (choose != 0)
                    {
                        var thatCard = Player1.Move(choose);
                        Console.WriteLine($"Выбранная карта: {thatCard}");
                        if (table.Player2Moves.Moves.Count != 0)
                        {
                            Console.WriteLine("Напишите норм карты, которую вы хотите атаковать.");
                            var enemyCard = table.Player2Moves.Moves[int.Parse(Console.ReadLine()) - 1];
                            if (!thatCard.FightToCard(enemyCard))
                            {
                                Console.WriteLine("Эта карта не может победить вражескую, попробуйте другую.");
                            } 
                            else
                            {
                                table.Player2Moves.Moves.Remove(enemyCard);
                            }
                        }
                    }
                }
                
                if (Console.ReadLine() == "end")
                    break;
            }
        }

        public Card ChooseCard(Player Player)
        {
            Console.WriteLine("Выберите карту для хода: ");
            Player.CardsOnHand.ShowCards();
            int choose = int.Parse(Console.ReadLine());

            return Player.Move(choose);
        }

        
    }
}
