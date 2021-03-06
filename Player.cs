/// all of this class was programmed by couper.

using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System;

namespace u5_Troon_Couper
{
    class Player
    {

        //variables
        public Point location;
        int speed = 4;
        Rectangle player;
        Canvas canvas;
        public Rectangle path = new Rectangle();
        ImageBrush sprite;
        BitmapImage bitmapImage;
        TranslateTransform translateTransform;


        // constructor
        public Player(Point location, Canvas c, Brush b)
        {
            canvas = c;
            player = new Rectangle();

            if (b == Brushes.Red)
            {
                bitmapImage = new BitmapImage(new Uri("RedTronBikeForwardBase.png", UriKind.Relative)); ;
                
            }
            else
            {
                bitmapImage = new BitmapImage(new Uri("BlueTronBikeForwardBase.png", UriKind.Relative));
            }
            


            //load image for the spritesheet
           
            sprite = new ImageBrush(bitmapImage);

            player.Height = 22;
            player.Width = 50;

           
            
            int playeroneInt = canvas.Children.Add(player);
            Canvas.SetLeft(player, location.X);
            Canvas.SetTop(player, location.Y);
            sprite.Stretch = Stretch.None;
            sprite.AlignmentX = AlignmentX.Left;
            sprite.AlignmentY = AlignmentY.Top;
            sprite.Viewport = new Rect(0, 0, 22, 50);
            translateTransform = new TranslateTransform(0, 0);
            player.Fill = sprite;
            sprite.Transform = translateTransform;


        }
     
        // rectangle that will be displayed as the character on screen
        public Rectangle rect { get { return player; } }


        // chanages which way the player is facing, also stops player from going back on themselves
        public int turn(Key k, int orientation)
        {
            if (k == Key.Left
                || k == Key.A
                && orientation != 3)
            {
                if (orientation == 2)
                    player.LayoutTransform = new RotateTransform(180);
                if (orientation == 4)
                    player.LayoutTransform = new RotateTransform(180);
                orientation = 1;
            }
            if ((k == Key.Up
                && orientation != 4)
                || (k == Key.W
                && orientation != 4))
            {
                if (orientation == 1)
                    player.LayoutTransform = new RotateTransform(90);
                else if (orientation == 3)
                    player.LayoutTransform = new RotateTransform(-90);
                orientation = 2;
            }

            if ((k == Key.Right
                && orientation != 1)
                || (k == Key.D
                && orientation != 1))
            { if (orientation == 2)
                    player.LayoutTransform = new RotateTransform(180);
                if (orientation == 4)
                    player.LayoutTransform = new RotateTransform(-180);
                orientation = 3;
            }
        
            if ((k == Key.Down
                && orientation != 2)
                || (k == Key.S
                && orientation != 2))
            {
                if (orientation == 1)
                    player.LayoutTransform = new RotateTransform(-90);
                else if (orientation == 3)
                    player.LayoutTransform = new RotateTransform(90);
                orientation = 4;
               
            }

            return orientation;
        }
        public Point move(int orientation, Player player, Point location)
        {
            // moves player in direction of orientation
            if (orientation == 1)
            {
                location.X -= speed;
            }
            if (orientation == 2)
            {
                location.Y -= speed;
            }
            if (orientation == 3)
            {
                location.X += speed;
            }
            if (orientation == 4)
            {
                location.Y += speed;
            }

            // what happens if player is at edge of screen (moves to other side)
            if (location.X < 1)
            {
                location.X = 580;
            }
            if (location.X > 580)
            {
                location.X = 1;
            }
            if (location.Y < 1)
            {
                location.Y = 559;
            }
            if (location.Y > 559)
            {
                location.Y = 1;
            }
            return location;
        }


        // checks to see if players have hit a path or another character.
        public bool checkCollision(Point location1, Point location2)
        {
            System.Console.WriteLine("Path location is: (" + location1.X.ToString() + ", " + location1.Y.ToString() + ")");
            System.Console.WriteLine("Player location is: (" + location2.X.ToString() + ", " + location2.Y.ToString() + ")");
            if ((location1.X + 3 >= location2.X
                && location1.X + 3 <= location2.X + 3
                && location1.Y + 3 >= location2.Y
                && location1.Y + 3 <= location2.Y + 3))
            {
                return true;
            }

            if (location2.X + 3 >= location1.X
                && location2.X + 3 <= location1.X + 3
                && location2.Y + 3 >= location1.Y
                && location2.Y + 3 <= location1.Y + 3)
            {
                return true;
            }

            if (location2.Y > location1.Y
                && location2.Y < location1.Y + 3
                && location2.X > location1.X
                && location2.X < location1.X + 3)
            {
                return true;
            }

            if (location1.X + 3 > location2.X
                && location1.X + 3 < location2.X + 3
                && location1.Y > location2.Y
                && location1.Y < location2.Y + 3)
            {
                return true;
            }

            if (location2.X + 3 > location1.X
            && location2.X + 3 < location1.X + 3
            && location2.Y > location1.Y
            && location2.Y < location1.Y + 3)

            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
