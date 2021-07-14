using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace PinBall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EllipseGeometry HitArea { get; set; }
        private DispatcherTimer Timer { get; set; }
        private TransformBall TransformBall { get; set; }
        private float Velocity { get; set; } = 8.4f;
        public int BallDirectionX { get; set; } = 1;
        public int BallDirectionY { get; set; } = 1;
        private Random random { get; set; }
        private bool isFirstHit = true;
        public MainWindow()
        {
            InitializeComponent();
            TransformBall = new TransformBall();
            random = new Random();
            new GameManeger(this);

            Initialize();
        }
        void Initialize()
        {
            StartButton.Click += new RoutedEventHandler(handleStart);

        }
        async void handleStart(object sender, RoutedEventArgs e)
        {
            ContainerCanvas.Children.Remove(StartButton);
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(30);
            Timer.Tick += new EventHandler(timer_Tick);
            Timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            Update();
        }

        void Update()
        {

            Point pt = new Point(TransformBall.X, TransformBall.Y);
            HitArea = new EllipseGeometry(pt, 1.0, 1.0);
            VisualTreeHelper.HitTest(ContainerCanvas, null, new HitTestResultCallback(HitTestCallback), new GeometryHitTestParameters(HitArea));
            handleBallMovement();
        }

        void handleBallMovement()
        {

                TransformBall.X -= (int)(BallDirectionX * Velocity);
            TransformBall.Y -= (int)(BallDirectionY * Velocity);
            Ball.RenderTransform = new TranslateTransform(TransformBall.X, TransformBall.Y);
            int wallRight = 1000, wallTop = 0, wallBot = 500, wallLeft = 0;
            int randomNumber = random.Next(0, 2);
            if (TransformBall.Y <= wallTop + 5)
            {
                BallDirectionY = -BallDirectionY;
                if (randomNumber == 0)
                {
                    BallDirectionX = -(BallDirectionX + 1);
                }
                    
            }
            else if (TransformBall.Y >= wallBot - 75)
            {
                BallDirectionY = -BallDirectionY;
                if (randomNumber == 0)
                {
                    BallDirectionX = -(BallDirectionX + 1);
                }
            }
            else if (TransformBall.X >= wallRight - 40)
            {
                BallDirectionX = -BallDirectionX;
                if (randomNumber == 0)
                {
                    BallDirectionY = -(BallDirectionY + 1);
                }
            }
            else if (TransformBall.X <= wallLeft + 5)
            {
                BallDirectionX = -BallDirectionX;
                if (randomNumber == 0)
                {
                    BallDirectionY = -(BallDirectionY + 1);
                }
            }
            
        }

        private void SetDefaultDirections()
        {
            BallDirectionX = 1;
            BallDirectionY = 1;
        }

        private HitTestResultBehavior HitTestCallback(HitTestResult result)
        {
            isFirstHit = false;
            BallDirectionX = -BallDirectionX;
            BallDirectionY = -BallDirectionY;

            return HitTestResultBehavior.Continue;
        }
    }
    class TransformBall
    {
        public int X { get; set; } = 950;
        public int Y { get; set; } = 420; 
    }
}
