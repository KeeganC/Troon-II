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
    /// the following code was done by Couper
    enum GameState { SplashScreen, GameOn, GameOver }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // global variables
        GameState gameState;
        Player player1;
        Player player2;
        Brush brush1 = Brushes.Blue;
        Brush brush2 = Brushes.Red;
        int orientation1 = 1;
        int orientation2 = 3;
        List <Rectangle> path1 = new List<Rectangle>();
        List <Rectangle> path2 = new List<Rectangle>();


        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            
            // starts the game timer thingy
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);//fps
            gameTimer.Start();

        }

        private void setupGame()
        {
            // changes gamestate
            gameState = GameState.GameOn;

            // constructs the players and initializes their locations
            player1 = new Player(new Point(550, 300), canvas, brush1);
            player2 = new Player(new Point(50, 300), canvas, brush2);
            player1.location = new Point(550, 300);
            player2.location = new Point(50, 300);
        }

        // when a key is pressed
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // turns player 1
            if (e.Key == Key.Left
                || e.Key == Key.Up
                || e.Key == Key.Down
                || e.Key == Key.Right)
            {
                orientation1 = player1.turn(e.Key, orientation1);
            }

            // turns player 2
            if (e.Key == Key.A
               || e.Key == Key.W
               || e.Key == Key.S
               || e.Key == Key.D)
            {
                orientation2 = player2.turn(e.Key, orientation2);
            }
            //MessageBox.Show("Key was pressed. Shoud've turned someone");
        }

        // what happens every tick
        private void gameTimer_Tick(object sender, EventArgs e)
        {

            if (gameState == GameState.GameOn)
            {
                this.Title = "Game on";

                // creates a path behind the players
                player1.path = new Rectangle();
                player1.path.Width = 5;
                player1.path.Height = 5;
                player1.path.Fill = brush1;
                player2.path = new Rectangle();
                player2.path.Width = 5;
                player2.path.Height = 5;
                player2.path.Fill = brush2;

                // adding rectangles to the list for when we check for collisions
                path1.Add(player1.path);
                path2.Add(player2.path);

                // draws the path rectangles on the screen
                canvas.Children.Add(player1.path);
                canvas.Children.Add(player2.path);
                Canvas.SetLeft(player1.path, player1.location.X);
                Canvas.SetTop(player1.path, player1.location.Y);
                Canvas.SetLeft(player2.path, player2.location.X);
                Canvas.SetTop(player2.path, player2.location.Y);

                // the players move every tick, you can change the speed of the players in the player class if you want
                player1.location = player1.move(orientation1, player1, player1.location);
                player2.location = player2.move(orientation2, player2, player2.location);


                // moves the player rectangle
                Canvas.SetLeft(player1.rect, player1.location.X);
                Canvas.SetTop(player1.rect, player1.location.Y);
                Canvas.SetLeft(player2.rect, player2.location.X);
                Canvas.SetTop(player2.rect, player2.location.Y);
            }

            else if (gameState == GameState.GameOver)
            {
                this.Title = "Game Over";
                
            }
            /// this is the end of the code that Couper programmed


            else if (gameState == GameState.SplashScreen)
            {
                this.Title = "Splash Screen";
                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    setupGame();
                }
            }

        }


    }
}
