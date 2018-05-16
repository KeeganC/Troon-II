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


namespace u5_Troon_Couper
{
    class Player
    {
        /// following code programmed by Couper
        //variables
        public Point location;
        int speed = 4;
        Rectangle player;
        Canvas canvas;
        public Rectangle path = new Rectangle();

        // constructor
        public Player(Point location, Canvas c, Brush b)
        {
            canvas = c;
            player = new Rectangle();
            player.Fill = b;
            player.Height = 5;
            player.Width = 5;
            int playerInt = canvas.Children.Add(player);
            Canvas.SetLeft(player, location.X);
            Canvas.SetTop(player, location.Y);
        }

        // rectangle that will be displayed as the character on screen
        public Rectangle rect { get { return player; } }

        // chanages which way the player is facing, also stops player from going back on themselves
        public int turn (Key k, int orientation)
        {
            if (k == Key.Left
                ||k == Key.A
                && orientation != 3)
            {
                orientation = 1;
            }

            if ((k == Key.Up
                && orientation != 4)
                || (k == Key.W
                && orientation != 4))
            {
                orientation = 2;
            }

            if ((k == Key.Right
                && orientation != 1)
                || (k == Key.D
                && orientation != 1))
            {
                orientation = 3;
            }

            if ((k == Key.Down
                && orientation != 2)
                || (k == Key.S
                && orientation != 2))
            {
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
            if (location.X < 0)
            {
                location.X = 700;
            }
            if (location.X > 700)
            {
                location.X = 0;
            }
            if (location.Y < 0)
            {
                location.Y = 700;
            }
            if (location.Y > 700)
            {
                location.Y = 0;
            }
            return location;
        }
        /// this is the end of the code Couper programmed

        // checks to see if players have hit a path or another character.
        /// programmed by Keegan and Ian
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
