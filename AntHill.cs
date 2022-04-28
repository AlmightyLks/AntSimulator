using System;
using System.Drawing;

namespace AntSimulator
{
    public class AntHill : VisualEntity
    {
        private char _borderSymbol;
        public Ant[] Ants;
        public int Score;

        public AntHill(Size mapSize, Point position, int antCount) : base('0', position)
        {
            _borderSymbol = '#';
            Ants = new Ant[antCount];

            for (int i = 0; i < antCount; i++)
                Ants[i] = new Ant(mapSize, Position, Position);
        }

        public void Update(Point foodPosition)
        {
            UpdateAnts(foodPosition);
            ErasePosition();
            DrawPosition();
        }
        private void DrawPosition()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int y = 0; y < 3; y++)
            {
                Console.SetCursorPosition(Position.X - 2, Position.Y - 1 + y);
                for (int x = 0; x < 5; x++)
                {
                    Console.Write(_borderSymbol);
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(Symbol);
            Console.ResetColor();
        }
        private void ErasePosition()
        {
            Console.ResetColor();
            for (int y = 0; y < 3; y++)
            {
                Console.SetCursorPosition(Position.X - 2, Position.Y - 1 + y);
                for (int x = 0; x < 5; x++)
                {
                    Console.Write(' ');
                }
            }
        }
        private void UpdateAnts(Point foodPosition)
        {
            for (int i = 0; i < Ants.Length; i++)
            {
                Ants[i].Update(foodPosition);

                if (Ants[i].HasFood && Ants[i].Position == Position)
                {
                    Score++;
                    Ants[i].HasFood = false;
                }
            }
        }
    }
}
