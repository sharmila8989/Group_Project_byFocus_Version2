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
using Lego.Ev3.Core;
using Lego.Ev3.Desktop;
using Lego;
using Lego.Ev3;

namespace LegoProject2017_byFocus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int corner;
        int colour;
        LegoBot legobot = new LegoBot();
        public bool stopButtonClicked = false;
        
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();

            //legobot = new LegoBot();
            await LegoBot.MainBrick.ConnectToBrick();

            legobot.ColourSensor.setMode();
            legobot.GyroSensor.setGyroMode();
            await LegoBot.MainBrick.Brick.DirectCommand.ClearAllDevicesAsync();
            
            LegoBot.MainBrick.Brick.BrickChanged += Brick_BrickChanged;


                
        }

        private void  Brick_BrickChanged(object sender, BrickChangedEventArgs e)
        {
            //throw new NotImplementedException();
            gyroTextBox.Text = legobot.GyroSensor.SIValue.ToString();
            colourTextBox.Text = legobot.ColourSensor.SIValue.ToString();
            ultraTextBox.Text = legobot.UltrasonicSensor.SIValue.ToString();
           
        }

        private async void forwardButton_Click(object sender, RoutedEventArgs e)
        {
            //float distance = legobot.UltrasonicSensor.SIValue;
            while (legobot.UltrasonicSensor.SIValue >= 5 )
            {
                await legobot.MoveForward();
            } await legobot.Stop();
            
        }

        private async void backwardButton_Click(object sender, RoutedEventArgs e)
        {
            float distance = legobot.UltrasonicSensor.SIValue;

            
            while(legobot.UltrasonicSensor.SIValue <= distance + 5)
            { await legobot.MoveBackward(); }
                
            

        }

        private async void turnLeftButton_Click(object sender, RoutedEventArgs e)
        {
            await legobot.TurnLeft();
            //await legobot.stepMotor();
            await legobot.clearValues();
            
            

        }

        private async void turnRightButton_Click(object sender, RoutedEventArgs e)
        {
            await legobot.TurnRight();
            await legobot.clearValues();
        }

        private async void turnAroundButton_Click(object sender, RoutedEventArgs e)
        {
            await legobot.turnAround();
        }

        private async void stopButton_Click(object sender, RoutedEventArgs e)
        {
            //stopButtonClicked = true;
            await legobot.Stop();
        }

        private async void taskOneButton_Click(object sender, RoutedEventArgs e)
        {
            //staring of from a corner, the legotbot will halt at home base
            //List<float> wallColour = new List<float>();
            //bool red = false;
            //bool black = false;
            //corner = Convert.ToInt32(cornerNumberTextBox.Text);
            //while (red == false && black == false)
            //{

            //    while (legobot.UltrasonicSensor.SIValue >= 5)
            //    { await legobot.MoveForward(); }

            //    await legobot.Stop();
            //    float colour = legobot.ColourSensor.getColorSIValue();
            //    wallColour.Add(colour);
            //    red = wallColour.Contains(5);
            //    black = wallColour.Contains(0) || wallColour.Contains(1);

            //    if (corner == 1 && wallColour[0] == 5|| corner == 3 && wallColour[0] == 4|| corner == 2 && wallColour[0] == 2 || corner == 4 && wallColour[1] == 1)
            //    { await legobot.TurnRight(); }
            //    else
            //    { await legobot.TurnLeft(); }

            //}
            float color;
            do
            {
                while (legobot.UltrasonicSensor.SIValue >= 5)
                { await legobot.MoveForward(); }
                await legobot.Stop();
                color = legobot.ColourSensor.getColorSIValue();

                if (color != 5)
                {
                    await legobot.TurnRight();
                }

            } while (color != 5);


        }

        private async void taskTwoButton_Click(object sender, RoutedEventArgs e)
        {
            // staring from a corner, legotbot will travel to all 4 corners of arena
            corner = Convert.ToInt32(cornerNumberTextBox.Text);
            do { await legobot.MoveForward(); } while (legobot.UltrasonicSensor.SIValue <= 5);
            colour = Convert.ToInt32(legobot.ColourSensor.SIValue);
            int colour1 = colour;
            if (corner == 1 && colour == 2|| corner == 3 && colour == 4|| corner == 2 && colour == 2|| corner == 4 && colour == 1)
            {
                for(int i=0; i<3; i++)
                {
                    await legobot.TurnRight();
                    do { await legobot.MoveForward(); } while (legobot.UltrasonicSensor.SIValue <= 3);

                }
                
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    await legobot.TurnLeft();
                    do { await legobot.MoveForward(); } while (legobot.UltrasonicSensor.SIValue <= 3);

                }
                
            }



        }

        private void taskThreeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void resetButton_Click(object sender, RoutedEventArgs e)
        {
            await legobot.clearValues();
        }


      
    }
}
