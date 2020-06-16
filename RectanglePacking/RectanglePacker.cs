using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectanglePacking
{
    public class RectanglePacker
    {
        public int Hight { get; set; }
        public int Width { get; set; }
        public Dictionary<Coordinates, Rectangle> PackedRectengles { get; private set; }
        public int Square
        {
            get
            {
                return Hight * Width;
            }
        }

        public int TakenSquare { get; set; } = 0;

        public RectanglePacker(int h, int w)
        {
            Hight = h;
            Width = w;
        }
        public RectanglePacker(Rectangle rectangle)
        {
            Hight = rectangle.Hight;
            Width = rectangle.Width;
        }

        // pack rectangle using Next Fit Deacreasing Hight algorithm
        public bool PackRectangleWithNFDH(List<Rectangle> rectangles)
        {
            int Vlvl = 0;
            int Hlvl = 0;
            Coordinates lastRectangleCoordinates = new Coordinates()
            {
                X = 0,
                Y = 0
            };
            rectangles = rectangles.OrderByDescending(rectangle => rectangle.Hight).ToList();
            PackedRectengles = new Dictionary<Coordinates, Rectangle>();
            if (rectangles[0].Hight > this.Hight && rectangles[0].Width > this.Width)
            {
                return false;
            }
            Vlvl = rectangles[0].Hight;
            foreach(var item in rectangles)
            {
               
                if(Hlvl + item.Width <= this.Width)
                {
                    Hlvl += item.Width;
                   
                }
                else
                {
                    Hlvl = item.Width;
                    lastRectangleCoordinates.Y = Vlvl;
                    Vlvl += item.Hight;
                   lastRectangleCoordinates.X = 0;
                }
                PackedRectengles.Add(lastRectangleCoordinates, item);
                
                lastRectangleCoordinates.X += item.Width;

                if (Vlvl > this.Hight)
                    return false;
                {
                }
            }

            return true;
        }

    }
}
