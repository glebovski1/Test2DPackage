using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RectanglePacking;

namespace TestPackage
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            for (int i = 0; i < 5; i++)
            {
                rectangles.Add(new Rectangle(5, 5));
            }
            int Hight;
            int Width;
            Console.WriteLine("Write Hight");
            Hight = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Write Width");
            Width = Convert.ToInt32(Console.ReadLine());
            RectanglePacker rectanglePacker = new RectanglePacker(Hight, Width);
            if(rectanglePacker.PackRectangleWithNFDH(rectangles))
            {
                Console.WriteLine("Its fit");
            }
            else
            {
                Console.WriteLine("Cant fit");
            }
            Console.ReadKey();

        }
    }
}
