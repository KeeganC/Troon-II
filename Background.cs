/// all of this class was done by keegan
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace u5_Troon_Couper
{
    class Background
    {
        //create elements of changing background
        private Rectangle backGround = new Rectangle();
        private int counter = 0;

        //adds background
        public void drawBackground(Canvas canvas)
        {
            backGround.Height = 561;
            backGround.Width = 584;
            backGround.Fill = new ImageBrush(new BitmapImage(new Uri("Back1.png", UriKind.Relative)));
            canvas.Children.Add(backGround);
        }
        public void animateBackground()
        {
            //cycle through four frames
            counter++;
            if (counter == 5)
            {
                counter = 1;
            }

            //add frames
            if (counter == 1)
            {
                backGround.Fill = new ImageBrush(new BitmapImage(new Uri("Back1.png", UriKind.Relative)));
            }
            else if (counter == 2)
            {
                backGround.Fill = new ImageBrush(new BitmapImage(new Uri("Back2.png", UriKind.Relative)));
            }
            else if (counter == 3)
            {
                backGround.Fill = new ImageBrush(new BitmapImage(new Uri("Back3.png", UriKind.Relative)));
            }
            else if (counter == 4)
            {
                backGround.Fill = new ImageBrush(new BitmapImage(new Uri("Back4.png", UriKind.Relative)));
            }
        }
        
        public void gameOverScreen()
        {
            backGround.Fill = new ImageBrush(new BitmapImage(new Uri("TroonGameOver.png", UriKind.Relative)));
        }
    }
}