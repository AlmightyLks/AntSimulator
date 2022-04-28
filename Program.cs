using System;
using System.Drawing;

namespace AntSimulator
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            int stepsPerDay = 10;

            Console.WriteLine("Wie breit soll die Map sein?");
            int width = int.Parse(Console.ReadLine());

            Console.WriteLine("Wie hoch soll die Map sein?");
            int height = int.Parse(Console.ReadLine());

            Console.WriteLine("Wie viele Ameisen soll es geben?");
            int antCount = int.Parse(Console.ReadLine());

            Console.WriteLine("Wie viele Tage sollen simuliert werden?");
            int dayCount = int.Parse(Console.ReadLine());

            Console.WriteLine("Soll die Simulation im schnell Ablauf durchlaufen? (Y/N)");
            bool fast = Console.ReadLine() == "Y";

            Console.Clear();
            Console.CursorVisible = false;

            Size mapSize = new Size(width, height);
            Simulation simulation = new Simulation(mapSize, antCount, dayCount, stepsPerDay, fast);

            simulation.PlaySimulation();

            PrintResult(simulation);

            Console.WriteLine("Press Any Key to Exit...");
            Console.ReadKey();
        }
        private static void PrintResult(Simulation simulation)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WindowHeight = Console.WindowHeight + 2;

            int score = simulation.GetAntHillScore();
            Console.WriteLine($"Die {simulation.AntCount} Ameisen fanden {score} Essensstücke innerhalb von {simulation.DayCount} Tagen");
        }
    }
}
