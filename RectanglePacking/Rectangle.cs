using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectanglePacking
{
    public class Rectangle
    {
        public int Hight { get; set; }
        public int Width { get; set; }

        public int Square
        {
            get
            {
                return Hight * Width;
            }
        }
        public Rectangle(int hight, int width)
        {
            Hight = hight;
            Width = width;
        }
        public Rectangle()
        {

        }

    }

    public struct Coordinates
    {
        public int X;
        public int Y;
    }

    
}
