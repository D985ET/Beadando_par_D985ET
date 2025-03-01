using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _GraphicsDLL;

namespace _GraphicsWinForm
{
    public partial class Form1 : Form
    {
        //Challenge:
        //A görbék megrajzolása színátmenetesen
        //Oldalra gombok felvétele, hogy melyik görbe látszik
        //Színátmenetet lehessen ki-, bekapcsolni

        //Challenge+:
        // + Timer: Egy kör mintha kirajzolná a görbét
        // hasonlóan ehhez: https://en.wikipedia.org/wiki/Butterfly_curve_(transcendental)#/media/File:Animated_construction_of_butterfly_curve.gif
        // a kör színe változzon ha színátmenetesen rajzol

        Graphics g;

        double r = 100;
        double scale = 50;

        PointF center;

        public Form1()
        {
            InitializeComponent();
            center = new PointF(canvas.Width / 2, canvas.Height / 2);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            /*g = e.Graphics;*/
            /*g = this.CreateGraphics();*/
            /*g.DrawLine(Pens.Black, 0, center.Y, canvas.Width, center.Y);
            g.DrawLine(Pens.Black, center.X, 0, center.X, canvas.Height);*/
            /*//r sugarú O középpontú kör
             g.DrawParameters2D(Pens.Red, t => r * Math.Cos(t) + center.X,
                                t => r * Math.Sin(t) + center.Y,
                                0, 2 * Math.PI);*/

            //színusz kör, most feladat
           /* g.DrawParameters2D(Pens.Blue,
                               t => t,
                               t => -Math.Sin(t),
                               -2 * Math.PI, 2 * Math.PI,
                               scale, center.X, center.Y);*/
            //Csiga, butterfly görbe
            //x=sin(t)*(e^(cos(t))-2cos(4t)-sin^5(t/12))
            //y=cos(t)*(e^(cos(t))-2cos(4t)-sin^5(t/12))
            //t between 0 and 12*pi. 
            //--------------------------Butterfly curve ------------------
           /* g.DrawParameters2D(Pens.Green,
                                 t => -Math.Sin(t) * ( Math.Pow(Math.E, Math.Cos(t)) - 2 * - Math.Cos(4 * t) - Math.Sin(t / 12) * 5),
                                 t => -Math.Cos(t) * (Math.Pow(Math.E, Math.Cos(t)) - 2 * - Math.Cos(4 * t) - Math.Sin(t / 12) * 5),
                                 0,
                                 12 * Math.PI,
                                 scale, center.X, center.Y);*/

            //---------------------Lissajous curve------------------
            //ordo = 0                      ordo = pí/2
            //a=1, b = 1                    a = 1, b = 1 
            //LINE                          KÖR
            //fél line
            //A,B -> konstansok, ezzel lehet növelni a magasságot
            /* int a = 5, A = 3, b = 6, B = 3 ;
             double ordo = Math.PI / 2;
              g.DrawParameters2D(Pens.Red,
                                  t => A * -Math.Sin((a * t) + ordo),
                                  t => B * -Math.Sin(b * t),
                                  -25,
                                  25,
                                  scale, center.X, center.Y);*/
            

            //------------cardioid curve-----------
            //x=a( 2 cos(t)−cos(2t)),
            //y=a( 2 sin(t)−sin(2t)).
           /* int a = 1;
            g.DrawParameters2D(/*Pens.Red,*/
                              /*t => a * -(2*Math.Cos(t)-Math.Cos(2*t)),
                              t => a * -(2 * Math.Sin(t) - Math.Sin(2 * t)),
                              -25,
                              25,
                              scale, center.X, center.Y);*/
            //------------------Logarithmic Spiral-----------
            //x	= a * e^(b*t) * cos(t) 	
            // y = a * e^(b*t) * sin(t)
            /*//a = [0,1[ 1 nincs benne
            float a = 0.61f, b = 0.35f;
            g.DrawParameters2D(Pens.Red,
                            t => a * -Math.Pow(Math.E,(b * t)) * Math.Cos(t),
                            t => a * -Math.Pow(Math.E, (b * t)) * -Math.Sin(t),
                            -10,
                            10,
                            scale, center.X, center.Y);*/
            
        }


        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            g = canvas.CreateGraphics();
            g.Clear(Color.White);
            
            g.DrawLine(Pens.Black, 0, center.Y, canvas.Width, center.Y);
            g.DrawLine(Pens.Black, center.X, 0, center.X, canvas.Height);

            double elapsedTime = 0;
            
            for (int i = 0; i < CurveListBox.CheckedItems.Count; i++)
            {

                if (CurveListBox.CheckedItems[i].ToString() == "Butterfly" && MultiThreadCheck.Checked == true)
                {
                    elapsedTime = 0;
                    timerLbl.Text += "";
                    elapsedTime = await g.DrawParameters2DAsync(t => -Math.Sin(t) * (Math.Pow(Math.E, Math.Cos(t)) - 2 * -Math.Cos(4 * t) - Math.Sin(t / 12) * 5),
                    t => -Math.Cos(t) * (Math.Pow(Math.E, Math.Cos(t)) - 2 * -Math.Cos(4 * t) - Math.Sin(t / 12) * 5),
                                 0,
                                 12 * Math.PI,
                                 scale, center.X, center.Y);
                    Console.WriteLine($"Time taken: {elapsedTime} ms");
                }
                else if (CurveListBox.CheckedItems[i].ToString() == "Lissajous" && MultiThreadCheck.Checked == true)
                {
                    elapsedTime = 0;
                    timerLbl.Text += "";
                    int a = 1, A = 3, b = 3, B = 3;
                    double ordo = Math.PI / 2;
                    elapsedTime = await g.DrawParameters2DAsync(t => A * -Math.Sin((a * t) + ordo), t => B * -Math.Sin(b * t),
                                             -25,
                                             25,
                                             scale, center.X, center.Y);
                    Console.WriteLine($"Time taken: {elapsedTime} ms");
                   
                }
                else if (CurveListBox.CheckedItems[i].ToString() == "Cardioid" && MultiThreadCheck.Checked == true)
                {
                    elapsedTime = 0;
                    timerLbl.Text += "";
                    // x = a(2 cos(t)−cos(2t)),
                    // y=a( 2 sin(t)−sin(2t)).
                    int a = 1;
                    elapsedTime = await g.DrawParameters2DAsync(
                     t => a * -(2 * Math.Cos(t) - Math.Cos(2 * t)), t => a * -(2 * Math.Sin(t) - Math.Sin(2 * t)),
                     -25,
                     25,
                     scale, center.X, center.Y);
                    Console.WriteLine($"Time taken: {elapsedTime} ms");
                    
                }
                else if (CurveListBox.CheckedItems[i].ToString() == "Logarithmic" && MultiThreadCheck.Checked == true)
                {
                    elapsedTime = 0;
                    timerLbl.Text += "";
                    float a = 0.61f, b = 0.35f;
                    elapsedTime = await g.DrawParameters2DAsync(/*Pens.Red,*/
                     t => a * -Math.Pow(Math.E, (b * t)) * Math.Cos(t),
                     t => a * -Math.Pow(Math.E, (b * t)) * -Math.Sin(t),
                     -10,
                     10,
                     scale, center.X, center.Y);
                    Console.WriteLine($"Time taken: {elapsedTime} ms");
                    
                }
                if (CurveListBox.CheckedItems[i].ToString() == "Butterfly" && MultiThreadCheck.Checked == false)
                {
                    elapsedTime = 0;
                    timerLbl.Text += "";
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    await Task.Run(() => g.DrawParameters2D(
                        t => -Math.Sin(t) * (Math.Pow(Math.E, Math.Cos(t)) - 2 * -Math.Cos(4 * t) - Math.Sin(t / 12) * 5),
                        t => -Math.Cos(t) * (Math.Pow(Math.E, Math.Cos(t)) - 2 * -Math.Cos(4 * t) - Math.Sin(t / 12) * 5),
                        0,
                        12 * Math.PI,
                        scale, center.X, center.Y));

                    stopwatch.Stop();
                    elapsedTime = stopwatch.ElapsedMilliseconds;
                }
                else if (CurveListBox.CheckedItems[i].ToString() == "Lissajous" && MultiThreadCheck.Checked == false)
                {
                    elapsedTime = 0;
                    timerLbl.Text += "";
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    int a = 1, A = 3, b = 3, B = 3;
                    double ordo = Math.PI / 2;
                    await Task.Run(() => g.DrawParameters2D(t => A * -Math.Sin((a * t) + ordo), t => B * -Math.Sin(b * t),
                                             -25,
                                             25,
                                             scale, center.X, center.Y));
                    Console.WriteLine($"Time taken: {elapsedTime} ms");
                    stopwatch.Stop();
                    elapsedTime = stopwatch.ElapsedMilliseconds;

                }
                else if (CurveListBox.CheckedItems[i].ToString() == "Cardioid" && MultiThreadCheck.Checked == false)
                {
                    elapsedTime = 0;
                    timerLbl.Text += "";
                    // x = a(2 cos(t)−cos(2t)),
                    // y=a( 2 sin(t)−sin(2t)).
                    int a = 1;
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    await Task.Run(() => g.DrawParameters2D(
                     t => a * -(2 * Math.Cos(t) - Math.Cos(2 * t)), t => a * -(2 * Math.Sin(t) - Math.Sin(2 * t)),
                     -25,
                     25,
                     scale, center.X, center.Y));
                    stopwatch.Stop();
                    elapsedTime = stopwatch.ElapsedMilliseconds;
                    Console.WriteLine($"Time taken: {elapsedTime} ms");

                }
                else if (CurveListBox.CheckedItems[i].ToString() == "Logarithmic" && MultiThreadCheck.Checked == false)
                {
                    elapsedTime = 0;
                    timerLbl.Text += "";
                    float a = 0.61f, b = 0.35f;
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    await Task.Run(() => g.DrawParameters2D(/*Pens.Red,*/
                     t => a * -Math.Pow(Math.E, (b * t)) * Math.Cos(t),
                     t => a * -Math.Pow(Math.E, (b * t)) * -Math.Sin(t),
                     -10,
                     10,
                     scale, center.X, center.Y));
                    stopwatch.Stop();
                    elapsedTime = stopwatch.ElapsedMilliseconds;
                    Console.WriteLine($"Time taken: {elapsedTime} ms");

                }
                timerLbl.Text = "";
                timerLbl.Text += elapsedTime;
                timerLbl.Text += " ms";
            }
           

            //r sugarú O középpontú kör
            /* g.DrawParameters2D(Pens.Red, t => r * Math.Cos(t) + center.X,
                                t => r * Math.Sin(t) + center.Y,
                                0, 2 * Math.PI);*/

                //színusz kör, most feladat
                /*g.DrawParameters2D(Pens.Blue,
                                    t => t,
                                    t => -Math.Sin(t),
                                    -2 * Math.PI, 2 * Math.PI,
                                    scale, center.X, center.Y);*/
                //Csiga, butterfly görbe
                /*x=sin(t)*(e^(cos(t))-2cos(4t)-sin^5(t/12))
                y=cos(t)*(e^(cos(t))-2cos(4t)-sin^5(t/12))*/
                //t between 0 and 12*pi. 
                //--------------------------Butterfly curve ------------------
                /*
               g.DrawParameters2D(//Pens.Green,
                                     t => -Math.Sin(t) * ( Math.Pow(Math.E, Math.Cos(t)) - 2 * - Math.Cos(4 * t) - Math.Sin(t / 12) * 5),
                                     t => -Math.Cos(t) * (Math.Pow(Math.E, Math.Cos(t)) - 2 * - Math.Cos(4 * t) - Math.Sin(t / 12) * 5),
                                     0,
                                     12 * Math.PI,
                                     scale, center.X, center.Y);
                */
                /*
                double elapsedTime = await g.DrawParameters2DAsync(t => -Math.Sin(t) * (Math.Pow(Math.E, Math.Cos(t)) - 2 * -Math.Cos(4 * t) - Math.Sin(t / 12) * 5),
                    t => -Math.Cos(t) * (Math.Pow(Math.E, Math.Cos(t)) - 2 * -Math.Cos(4 * t) - Math.Sin(t / 12) * 5),
                                    0,
                                    12 * Math.PI,
                                    scale, center.X, center.Y);
                Console.WriteLine($"Time taken: {elapsedTime} ms");
                */


                //---------------------Lissajous curve------------------
                //ordo = 0                      ordo = pí/2
                //a=1, b = 1                    a = 1, b = 1 
                //LINE                          KÖR
                //fél line
                //A,B -> konstansok, ezzel lehet növelni a magasságot
                /* int a = 1, A = 3, b = 3, B = 3 ;
                 double ordo = Math.PI/2;
                  g.DrawParameters2D(/*Pens.Red,*/
                /* t => A * -Math.Sin((a * t) + ordo),
                 t => B * -Math.Sin(b * t),
                 -25,
                 25,
                 scale, center.X, center.Y);*/


                //------------cardioid curve-----------
                //x=a( 2 cos(t)−cos(2t)),
                //y=a( 2 sin(t)−sin(2t)).
                /* int a = 1;
                 g.DrawParameters2D(/*Pens.Red*/
                /* t => a * -(2*Math.Cos(t)-Math.Cos(2*t)),
                 t => a * -(2 * Math.Sin(t) - Math.Sin(2 * t)),
                 -25,
                 25,
                 scale, center.X, center.Y);*/
                //------------------Logarithmic Spiral-----------
                //x	= a * e^(b*t) * cos(t) 	
                // y = a * e^(b*t) * sin(t)
                //a = [0,1[ 1 nincs benne
                /*float a = 0.61f, b = 0.35f;
                 g.DrawParameters2D(/*Pens.Red,*/
                /* t => a * -Math.Pow(Math.E,(b * t)) * Math.Cos(t),
                 t => a * -Math.Pow(Math.E, (b * t)) * -Math.Sin(t),
                 -10,
                 10,
                 scale, center.X, center.Y);*/
        }
        }
}
