﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using RectanglePacking;

namespace TestAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int boxHight;
        int boxWidth;

        int rectangleHight;
        int rectangleWidth;
        int numberOfRectangles;

        RectanglePacker rectanglePacker = null;

        public MainWindow()
        {
            InitializeComponent();


            RectangleGeometry rectangle = new RectangleGeometry(new Rect(0, 0, 80, 80));
            Path myPath = new Path();
            myPath.Fill = Brushes.LemonChiffon;
            myPath.Stroke = Brushes.Black;
            myPath.StrokeThickness = 1;
         
           
            myPath.Data = rectangle;
           
           
        }
        
        private void Button_Calculate_Click(object sender, RoutedEventArgs e)
        {
            Drowing.Children.Clear();
            try
            {
                boxHight = Convert.ToInt32(MainHight.Text);
                boxWidth = Convert.ToInt32(MainWidth.Text);
                rectangleHight = Convert.ToInt32(MinorHight.Text);
                rectangleWidth = Convert.ToInt32(MinorWidth.Text);
                numberOfRectangles = Convert.ToInt32(Number.Text);

                rectanglePacker = new RectanglePacker(boxHight, boxWidth);

                bool result;

                List<RectanglePacking.Rectangle> rectangles = new List<RectanglePacking.Rectangle>();
                for (int i = 0; i < numberOfRectangles; i++)
                {
                    rectangles.Add(new RectanglePacking.Rectangle(rectangleHight, rectangleWidth));
                }

                result = rectanglePacker.PackRectangleWithNFDH(rectangles);

                if(!result)
                {
                    MassageBox.Text = "Cant fit";
                }
                if(result)
                {
                    MassageBox.Text = "successfully fited";

                    int h = rectanglePacker.Hight;
                    int w = rectanglePacker.Width;
                    RectangleGeometry rectangle = new RectangleGeometry(new Rect(0, 0, w, h));
                    
                    Path myPath = new Path();
                    myPath.HorizontalAlignment = HorizontalAlignment.Left;
                    myPath.VerticalAlignment = VerticalAlignment.Bottom;
                    
                    
                    myPath.Stroke = Brushes.Black;
                    myPath.StrokeThickness = 3;
                    
                    GeometryGroup geometryGroup = new GeometryGroup();
                    geometryGroup.Children.Add(rectangle);
                    


                    foreach (var item in rectanglePacker.PackedRectengles)
                    {
                        var cord = item.Key;
                        var rect = item.Value;
                        var rectangle2 = new RectangleGeometry(new Rect(cord.X, cord.Y, rect.Width, rect.Hight));
                        



                        geometryGroup.Children.Add(rectangle2);
                      

                    }
                    myPath.Data = geometryGroup;
                    Drowing.Children.Add(myPath);
                }
               

            }
            catch(Exception ex)
            {
                MassageBox.Text = ex.Message;
            }





        }


       



    }
}
