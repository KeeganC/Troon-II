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
        // global variables
        GameState gameState;
        Player player1;
        Player player2;
        Brush brush1 = Brushes.Blue;
        Brush brush2 = Brushes.Red;
        int orientation1 = 1;
        int orientation2 = 3;
        Rectangle path1;
        Rectangle path2;

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
            gameState = GameState.GameOn;
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
                player1.location = player1.move(orientation1, player1, player1.location);
                player2.location = player2.move(orientation2, player2, player2.location);
                path1 = new Rectangle();
                path1.Width = 5;
                path1.Height = 5;
                path1.Fill = brush1;
                path2 = new Rectangle();
                path2.Width = 5;
                path2.Height = 5;
                path2.Fill = brush2;

                canvas.Children.Add(path1);
                canvas.Children.Add(path2);
                Canvas.SetLeft(path1, player1.location.X);
                Canvas.SetTop(path1, player1.location.Y);
                Canvas.SetLeft(path2, player2.location.X);
                Canvas.SetTop(path2, player2.location.Y);

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
        }

        
    }
}
