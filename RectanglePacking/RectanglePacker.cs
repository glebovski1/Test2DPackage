using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
            if (rectangles[0].Hight > this.Hight || rectangles[0].Width > this.Width)
            {
                return false;
            }
            var rectanglesOrderedByWidth = rectangles.OrderByDescending(rectangle => rectangle.Width).ToList();
            if (rectanglesOrderedByWidth[0].Width > this.Width)
            {
                return false;
            }
            Vlvl = rectangles[0].Hight;
            foreach (var item in rectangles)
            {

                if (Hlvl + item.Width <= this.Width)
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
        // pack rectangles unsing First Fit Decreasing High algorithm
        public bool PackRectangleWithFFDH(List<Rectangle> rectangles)
        {
            PackedRectengles = new Dictionary<Coordinates, Rectangle>();
            int lvl = 0;
            Dictionary<int, int> HightOfLvl = new Dictionary<int, int>();
            Dictionary<int, int> WidthOfLvl = new Dictionary<int, int>();
            Coordinates lastCoord = new Coordinates(0, 0);
            var orderedByHightRectangles = rectangles.OrderByDescending(item => item.Hight).ToList();
            var orderedByWidthRectangles = rectangles.OrderByDescending(item => item.Width).ToList();
            if (orderedByHightRectangles[0].Hight > this.Hight || orderedByWidthRectangles[0].Width > this.Width)
            {
                return false;
            }
            PackedRectengles.Add(lastCoord, orderedByHightRectangles[0]);

            HightOfLvl.Add(lvl, orderedByHightRectangles[0].Hight);
            WidthOfLvl.Add(lvl, orderedByHightRectangles[0].Width);

            for (int i = 1; i < orderedByHightRectangles.Count; i++)
            {
                bool placeIsFoundOnExistingLvl = false;
                for (int j = 0; j <= lvl; j++)
                {
                    if ((orderedByHightRectangles[i].Hight <= HightOfLvl.FirstOrDefault(item => item.Key == j).Value) &&
                        (orderedByHightRectangles[i].Width <= this.Width - WidthOfLvl.FirstOrDefault(item => item.Key == j).Value))
                    {
                        int tempWidth = WidthOfLvl.Where(item => item.Key == j).FirstOrDefault().Value;
                        lastCoord.X = WidthOfLvl.FirstOrDefault(item => item.Key == j).Value;
                        lastCoord.Y = HightOfLvl.FirstOrDefault(item => item.Key == j-1).Value;
                        WidthOfLvl.Remove(j);
                        WidthOfLvl.Add(j, tempWidth + orderedByHightRectangles[i].Width);
                        placeIsFoundOnExistingLvl = true;
                        break;
                    }
                };
                if (placeIsFoundOnExistingLvl == false)
                {

                    lastCoord.X = 0;
                    lastCoord.Y = HightOfLvl.FirstOrDefault(item => item.Key == lvl).Value;
                    lvl++;
                    HightOfLvl.Add(lvl, lastCoord.Y + orderedByHightRectangles[i].Hight);
                    WidthOfLvl.Add(lvl, lastCoord.X + orderedByHightRectangles[i].Width);
                   
                }
                PackedRectengles.Add(lastCoord, orderedByHightRectangles[i]);
                placeIsFoundOnExistingLvl = false;

                if (HightOfLvl.FirstOrDefault(item => item.Key == lvl).Value > this.Hight)
                {
                    return false;
                }
            }


            return true;

            //Dictionary<int, int> VHlevels = new Dictionary<int, int>();

            //Coordinates lastRectangleCoordinates = new Coordinates(0, 0);

            //rectangles = rectangles.OrderByDescending(rectangle => rectangle.Hight).ToList();
            //PackedRectengles = new Dictionary<Coordinates, Rectangle>();

            //var rectanglesOrderedByWidth = rectangles.OrderByDescending(rectangle => rectangle.Width).ToList();
            //if (rectangles[0].Hight > this.Hight || rectangles[0].Width > this.Width || rectanglesOrderedByWidth[0].Width > this.Width)
            //{
            //    return false;
            //}


            //VHlevels.Add(0, 0);
            //foreach (var item in rectangles)
            //{
            //    foreach (var dict in VHlevels)
            //    {
            //        bool result = false;
            //        if (dict.Value + item.Width <= this.Width)
            //        {
            //            KeyValuePair<int, int> valuePair = new KeyValuePair<int, int>(dict.Key, dict.Value + item.Width);
            //            VHlevels.Remove(dict.Key);
            //            VHlevels.Add(valuePair.Key, valuePair.Value);
            //            result = true;
            //        }

            //        else
            //        {
            //            var Hlvl = dict.Value + item.Hight;
            //            lastRectangleCoordinates.Y += Hlvl;
            //            var Vlvl = dict.Key + item.Hight;
            //            KeyValuePair<int, int> valuePair = new KeyValuePair<int, int>(Vlvl, Hlvl);
            //            VHlevels.Remove(dict.Key);
            //            VHlevels.Add(valuePair.Key, valuePair.Value);

            //            lastRectangleCoordinates.X = 0;
            //            result = true;
            //        }
            //        PackedRectengles.Add(lastRectangleCoordinates, item);

            //        lastRectangleCoordinates.X += item.Width;
            //        if (result)
            //        {
            //            break;
            //        }
            //    }
            //    if (VHlevels.OrderByDescending(x => x.Key).FirstOrDefault().Key > this.Hight)
            //        return false;





        }

    }
}
