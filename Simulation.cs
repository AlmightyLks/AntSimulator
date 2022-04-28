using System;
using System.Drawing;
using System.Threading;

namespace AntSimulator
{
    public class Simulation
    {
        private Random _random;
        private Size _screenSize;
        private AntHill _antHill;
        private Point _foodPosition;
        private int _sleepTime;
        public int DayCount;
        public int StepsPerDay;
        public int AntCount;

        public Simulation(Size screenSize, int antCount, int dayCount, int stepsPerDay, bool fast)
        {
            _antHill = new AntHill(screenSize, new Point(screenSize.Width / 2, screenSize.Height / 2), antCount);
            _random = new Random();
            _screenSize = screenSize;
            DayCount = dayCount;
            StepsPerDay = stepsPerDay;
            AntCount = antCount;

            Console.SetWindowSize(_screenSize.Width, _screenSize.Height);
            Console.SetBufferSize(_screenSize.Width, _screenSize.Height);

            if (fast)
                _sleepTime = 0;
            else
                _sleepTime = 25;

            var width = _random.Next(0, Console.WindowWidth);
            var height = _random.Next(0, Console.WindowHeight);
            _foodPosition.X = width;
            _foodPosition.Y = height;
        }

        public void PlaySimulation()
        {
            for (int day = 0; day < DayCount; day++)
            {
                for (int step = 0; step < StepsPerDay; step++)
                {

                    _antHill.Update(_foodPosition);
                    RepositionFood();
                    DrawFood();
                    DrawStats(day);

                    Thread.Sleep(_sleepTime);
                }
            }
        }
        public int GetAntHillScore()
        {
            return _antHill.Score;
        }
        private void RepositionFood()
        {
            for (int i = 0; i < _antHill.Ants.Length; i++)
            {
                if (_antHill.Ants[i].Position == _foodPosition)
                {
                    // Overwrite old spot
                    Console.ResetColor();
                    Console.SetCursorPosition(_foodPosition.X, _foodPosition.Y);
                    Console.Write(" ");

                    var width = _random.Next(0, Console.WindowWidth);
                    var height = _random.Next(0, Console.WindowHeight);
                    _foodPosition.X = width;
                    _foodPosition.Y = height;
                    break;
                }
            }
        }
        private void DrawFood()
        {
            Console.SetCursorPosition(_foodPosition.X, _foodPosition.Y);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("F");
            Console.ResetColor();
        }
        private void DrawStats(int day)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("Score: " + _antHill.Score);
            Console.Write(" | ");
            Console.Write("Tag: " + day + "/" + DayCount);
        }
    }
}
