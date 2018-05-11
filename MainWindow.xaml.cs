/* 
 * Couper Ebbs-picken
 * 5/9/2018
 * Make Troon
 */
using System;
using System.Collections.Generic;
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

namespace u5_Troon_Couper
{
    enum GameState { SplashScreen, GameOn, GameOver }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameState gameState;
        Player player1;
        Player player2;
        Point location1 = new Point(50, 300);
        Point location2 = new Point(550, 300);
        Brush brush1 = Brushes.Blue;
        Brush brush2 = Brushes.Red;
        int orientation1 = 3;
        int orientation2 = 1;
        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);//fps
            gameTimer.Start();

        }

        private void setupGame()
        {
            gameState = GameState.GameOn;
            player1 = new Player(location1, canvas, brush1);
            player2 = new Player(location2, canvas, brush2);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

            if (gameState == GameState.SplashScreen)
            {
                this.Title = "Splash Screen";
                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    setupGame();
                }
            }

            else if (gameState == GameState.GameOn)
            {
                this.Title = "Game on";
                location1 = player1.move(orientation1, player1,location1);
                location2 = player2.move(orientation2, player2, location2);

                Canvas.SetLeft(player1.rect, location1.X);
                Canvas.SetTop(player1.rect, location1.Y);
                Canvas.SetLeft(player2.rect, location2.X);
                Canvas.SetTop(player2.rect, location2.Y);
            }

            else if (gameState == GameState.GameOver)
            {
                this.Title = "Game Over";
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left
                || e.Key == Key.Up
                || e.Key == Key.Down
                || e.Key == Key.Right)
            {
                orientation1 = player1.turn(e.Key);
            }

            if (e.Key == Key.A
               || e.Key == Key.W
               || e.Key == Key.S
               || e.Key == Key.D)
            {
                orientation2 = player2.turn(e.Key);
            }


        }
    }
}


            // the spaceship location will change
            Canvas.SetLeft(spaceship1.rect, location1.X);
            Canvas.SetTop(spaceship1.rect, location1.Y);
            Canvas.SetLeft(spaceship2.rect, location2.X);
            Canvas.SetTop(spaceship2.rect, location2.Y);

            // check if the spaceships have crashed
            spaceship1.checkCollision(location1, location2);
        }
    }
}
