using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int[] BlueBall = new int[70];
        int[] RedBall = new int[43];
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string line;
            string BallNum;
            string[] Output = new string[120];
            int counter = 0;
            int WB1, WB2, WB3, WB4, WB5, PB = 0;
            int[] numBlueBalls = new int[70];
            int[] numRedBalls = new int[70];
            WebClient data = new WebClient();
            Stream stream = data.OpenRead("http://www.powerball.com/powerball/winnums-text.txt");
            StreamReader Numbers = new StreamReader(stream); 
            while((line = Numbers.ReadLine()) != null)
            {
                //Basic Output
                toolStripStatusLabel2.Text = line;
                counter++;

                //Convert Str to Int
                if (line.Contains("Draw Date")) continue;
                BallNum = line.Substring(12, 2);
                WB1 = Int32.Parse(BallNum);

                BallNum = line.Substring(16, 2);
                WB2 = Int32.Parse(BallNum);

                BallNum = line.Substring(20, 2);
                WB3 = Int32.Parse(BallNum);

                BallNum = line.Substring(24, 2);
                WB4 = Int32.Parse(BallNum);

                BallNum = line.Substring(28, 2);
                WB5 = Int32.Parse(BallNum);

                BallNum = line.Substring(32, 2);
                PB = Int32.Parse(BallNum);

                BlueBall[WB1]++;
                BlueBall[WB2]++;
                BlueBall[WB3]++;
                BlueBall[WB4]++;
                BlueBall[WB5]++;
                RedBall[PB]++;

            }
            label1.Text = "Winner Time!";
            toolStripStatusLabel2.Text = "The last data is " + toolStripStatusLabel2.Text;
            toolStripStatusLabel1.Text = counter + " data received!";

            Numbers.Close();

            Output[0] = "There have been " + counter + " games, for blue balls,";
            for(int i = 1;i < 70; i++)
            {
                Output[i] = i + " has appeared " + BlueBall[i] + " times";
            }
            Output[70] = "for red balls,\n";
            for(int i = 1; i < 43; i++)
            {
                Output[i + 70] = i + " has appeared " + RedBall[i] + " times";
            }
            Output[113] = "So please carefully pick your number!";

            textBox1.Text = String.Join(Environment.NewLine, Output);

            for (int i = 1; i < 70; i++) numBlueBalls[i] = i;
            Array.Sort(BlueBall,numBlueBalls);

            for (int i = 1; i < 43; i++) numRedBalls[i] = i;
            Array.Sort(RedBall,numRedBalls);

            label1.Text = "The 5 most possible blueball is \n" + numBlueBalls[69] + ", " + numBlueBalls[68] + ", " + numBlueBalls[67]
                + ", " + numBlueBalls[66] + ", " + numBlueBalls[65] + "\nThe most possible red ball is " + numRedBalls[42];
        }

    }
}
