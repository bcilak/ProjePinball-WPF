using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PinBall
{
    public class GameManeger
    {
        public MainWindow mw { get; set; }
        public static int AngleOne { get; set; } = 0;
        public static int AngleTwo { get; set; } = 0;
        public GameManeger(MainWindow mainWindow)
        {
            mw = mainWindow;
            mw.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            mw.KeyUp += new KeyEventHandler(OnButtonKeyUp);
        }
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    
                    //var a = (Rect)mw.TestRect;
                    if (GameManeger.AngleOne >= -40)
                    {
                        GameManeger.AngleOne -= 2;
                        RotateTransform rotate = new RotateTransform(GameManeger.AngleOne);
                        mw.One.RenderTransform = rotate;
                    }
                    break; 
                case Key.Right:
                    if (GameManeger.AngleTwo <= 40)
                    {
                        GameManeger.AngleTwo += 2;
                        RotateTransform rotate = new RotateTransform(GameManeger.AngleTwo);
                        mw.Two.RenderTransform = rotate;
                    }
                    break;
                default:
                    break;
            }
        }
        private async void OnButtonKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:

                    while ( GameManeger.AngleOne <= 0)
                    {
                        await Task.Delay(30);
                        GameManeger.AngleOne += 2;
                        RotateTransform rotate = new RotateTransform(GameManeger.AngleOne);
                        mw.One.RenderTransform = rotate;
                    }
                    break;

                    case Key.Right:
                    while (GameManeger.AngleTwo >= 0)
                    {
                        await Task.Delay(30);
                        GameManeger.AngleTwo -= 2;
                        RotateTransform rotate = new RotateTransform(GameManeger.AngleTwo);
                        mw.Two.RenderTransform = rotate;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
