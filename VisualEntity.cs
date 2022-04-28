using System.Drawing;

namespace AntSimulator
{
    public class VisualEntity
    {
        public char Symbol;
        public Point Position;

        public VisualEntity(char symbol, Point position)
        {
            Symbol = symbol;
            Position = position;
        }
    }
}
