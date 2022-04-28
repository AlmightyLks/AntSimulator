using System;
using System.Drawing;

namespace AntSimulator
{
    public class Ant : VisualEntity
    {
        public Point HomePosition;
        public Point TargetPosition;
        public bool HasTarget;
        public bool IsExploring;
        public bool HasFood;

        private Random _random;
        private int _stamina;
        private int _maxStamina;

        public Ant(Size mapSize, Point homePosition) : base('A', new Point())
        {
            _random = new Random();
            Position = new Point();
            HomePosition = homePosition;
            _maxStamina = mapSize.Width + mapSize.Height / 2;
            _stamina = _maxStamina;
        }
        public Ant(Size mapSize, Point position, Point homePosition) : base('A', position)
        {
            _random = new Random();
            Position = position;
            HomePosition = homePosition;
            _maxStamina = mapSize.Width + mapSize.Height / 2;
            _stamina = _maxStamina;
        }

        public void Update(Point foodPosition)
        {
            ErasePosition();
            Move(foodPosition);
            DrawPosition();
        }
        private void Move(Point foodPosition)
        {
            if (Position.X != 0 && Position.X > TargetPosition.X)
                Position.X--;
            else if (Position.X != Console.WindowWidth - 1 && Position.X < TargetPosition.X)
                Position.X++;
            else if (Position.Y != 0 && Position.Y > TargetPosition.Y)
                Position.Y--;
            else if (Position.Y != Console.WindowHeight - 1 && Position.Y < TargetPosition.Y)
                Position.Y++;

            AdjustTargetPosition(foodPosition);

            _stamina--;
        }
        private void AdjustTargetPosition(Point foodPosition)
        {
            if (Position == foodPosition)
            {
                IsExploring = false;
                HasTarget = true;
                TargetPosition = HomePosition;
                HasFood = true;
            }
            else if (Position == TargetPosition)
            {
                IsExploring = false;
                HasTarget = false;
            }

            if (HasFood)
            {
                TargetPosition = HomePosition;
                IsExploring = false;
            }
            else if (_stamina <= 0)
            {
                // wenn zuhaus angekommen, resette die Stamina
                if (Position == HomePosition)
                {
                    _stamina = _maxStamina;

                    HasTarget = false;
                    HasFood = false;
                    IsExploring = false;
                }
                // sonst, mach dich auf den Weg nachhause
                else
                {
                    HasTarget = true;
                    TargetPosition = HomePosition;
                    HasFood = false;
                    IsExploring = false;
                }
            }
            // if food is within a 5 block range
            else if (Math.Abs(Position.X - foodPosition.X) < 5 && Math.Abs(Position.Y - foodPosition.Y) < 5)
            {
                TargetPosition = foodPosition;
                HasTarget = true;
                IsExploring = false;
            }
            // random direction, if not exploring already
            else if (!IsExploring)
            {
                var x = _random.Next(0, Console.WindowWidth);
                var y = _random.Next(0, Console.WindowHeight);

                IsExploring = true;
                HasTarget = false;
                TargetPosition = new Point(x, y);
            }
        }
        private void DrawPosition()
        {
            if (HasFood)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            else if (_stamina <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write("A");
            Console.ResetColor();
        }
        private void ErasePosition()
        {
            Console.ResetColor();
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(" ");
        }
    }
}
