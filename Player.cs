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
        int orientation;
        int speed = 2;
        Rectangle player;
        Canvas canvas;

        public Player(Point location, Canvas c, Brush b)
        {
            canvas = c;
            player = new Rectangle();
            player.Fill = b;
            player.Height = 15;
            player.Width = 15;
            int playerInt = canvas.Children.Add(player);
            Canvas.SetLeft(player, location.X);
            Canvas.SetTop(player, location.Y);
        }

        public Rectangle rect { get { return player; } }

        // chanages which way the player is facing
        public int turn (Key k)
        {
            orientation = 0;

            if (k == Key.Left
                ||k == Key.A)
            {
                orientation = 1;
            }

            if (k == Key.Up
                || k == Key.W)
            {
                orientation = 2;
            }

            if (k == Key.Right
                || k == Key.D)
            {
                orientation = 3;
            }

            if (k == Key.Down
                || k == Key.S)
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

            // what happens if player is at edge of screen
            if (location.X < 0)
            {
                location.X = 600;
            }
            if (location.X > 600)
            {
                location.X = 0;
            }
            if (location.Y < 0)
            {
                location.Y = 600;
            }
            if (location.Y > 600)
            {
                location.Y = 0;
            }
            return location;
        }

        public void checkCollision(Point location1, Point location2)
        {

        }
    }
}
