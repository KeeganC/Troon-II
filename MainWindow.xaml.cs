/* 
 * Keegan Chan
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
    /// and edited by Keegan
    enum GameState { SplashScreen, GameOn, GameOver }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // global variables
        bool p1Gameover;
        bool p2Gameover;
        GameState gameState;
        Player player1;
        Player player2;
        Brush brush1 = Brushes.Blue;
        Brush brush2 = Brushes.Red;
        int orientation1 = 1;
        int orientation2 = 3;
        List<Rectangle> path1 = new List<Rectangle>();
        List<Rectangle> path2 = new List<Rectangle>();
        Background background = new Background();
        int counterTimer = 0;
        MediaPlayer musicPlayer = new MediaPlayer();
        int p1Score = 0;
        int p2Score = 0;


        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            //splash screen
            canvas.Background = new ImageBrush(new BitmapImage(new Uri("TroonSplash.png", UriKind.Relative)));

            //start music, by Keegan
            musicPlayer.Open(new Uri("TRON Legacy R3CONF1GUR3D - 06 - C.L.U. (Paul Oakenfold Remix) Daft Punk.mp3", UriKind.Relative));
            musicPlayer.Play();

            // starts the game timer thingy
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);//fps
            gameTimer.Start();

        }

        private void setupGame()
        {
            //MessageBox.Show("Setting up game");

            //add background
            background.drawBackground(canvas);

            // resetting orientation
            orientation1 = 1;
            orientation2 = 3;

            // resetting the paths
            path1 = new List<Rectangle>();
            path2 = new List<Rectangle>();

            // resets gameover
            p1Gameover = false;
            p2Gameover = false;
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
            counterTimer++;
            //add animation by Keegan
            if (counterTimer % 5 == 0)
            {
                background.animateBackground();

            }

            if (gameState == GameState.GameOn)
            {
                this.Title = "Game on";
                //Updates animation


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

                // checks to see if the players have hit the blue path
                foreach (Rectangle path in path1)
                {
                    Point pathPoint = new Point(Canvas.GetLeft(path), Canvas.GetTop(path));
                    p1Gameover = player1.checkCollision(player1.location, pathPoint);

                    //Scoring fixed by Keegan
                    if (p1Gameover == true)
                    {
                        gameState = GameState.GameOver;
                        background.gameOverScreen();
                        MessageBox.Show("Player 2 wins!");
                        p2Score++;
                        MessageBox.Show("Score:\r\nP1= " + p1Score.ToString() + " P2= " + p2Score.ToString());
                        p1Gameover = false;
                        break;
                    }

                    p2Gameover = player2.checkCollision(player2.location, pathPoint);

                    if (p2Gameover == true)
                    {
                        gameState = GameState.GameOver;
                        background.gameOverScreen();
                        MessageBox.Show("Player 1 wins!");
                        p1Score++;
                        MessageBox.Show("Score:\r\nP1= " + p1Score.ToString() + " P2= " + p2Score.ToString());
                        p2Gameover = false;
                        break;
                    }
                }

                // checks to see if the players have hit the red path
                foreach (Rectangle path in path2)
                {
                    Point pathPoint = new Point(Canvas.GetLeft(path), Canvas.GetTop(path));
                    p1Gameover = player1.checkCollision(pathPoint, player1.location);

                    if (p1Gameover == true)
                    {
                        gameState = GameState.GameOver;
                        background.gameOverScreen();
                        MessageBox.Show("Player 2 wins!");
                        p2Score++;
                        MessageBox.Show("Score:\r\nP1= " + p1Score.ToString() + " P2= " + p2Score.ToString());
                        p1Gameover = false;
                        break;
                    }

                    p2Gameover = player2.checkCollision(pathPoint, player2.location);

                    if (p2Gameover == true)
                    {
                        gameState = GameState.GameOver;
                        background.gameOverScreen();
                        MessageBox.Show("Player 1 wins!");
                        p1Score++;
                        MessageBox.Show("Score:\r\nP1= " + p1Score.ToString() + " P2= " + p2Score.ToString());
                        p2Gameover = false;
                        break;
                    }
                }
            }

            else if (gameState == GameState.GameOver)
            {
                this.Title = "Game Over";
                //MessageBox.Show("Game over. Winner is: " + winner);
                for (int i = canvas.Children.Count - 1; i >= 0; i--)
                {
                    canvas.Children.RemoveAt(i);
                }
                p1Gameover = false;
                p2Gameover = false;
                if (Keyboard.IsKeyDown(Key.Space))
                {
                    //MessageBox.Show("About to set up game");
                    setupGame();
                }
            }
            /// this is the end of the code that Couper programmed


            else if (gameState == GameState.SplashScreen)
            {
                this.Title = "Splash Screen";
                //background.drawBackground(canvas);

                if (Keyboard.IsKeyDown(Key.Space))
                {
                    //MessageBox.Show("About to set up game");
                    setupGame();
                }
            }

        }


    }
}
