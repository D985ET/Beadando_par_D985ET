using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public static class ExtensionGraphics
    {
   
        #region ParametricCurve2D

        public static async Task DrawParameters2D(this Graphics g,
      Func<double, double> X, Func<double, double> Y,
      double a, double b, double scale = 1.0, double cX = 0, double cY = 0, double n = 500.0)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            if (a >= b)
            {
                throw new Exception("Invalid interval!");
            }

            int red = 0, green = 255, blue = 0;
            double t = a;
            double h = (b - a) / n;

            PointF p0 = new PointF((float)(scale * X(t) + cX),
                                   (float)(scale * Y(t) + cY));

            while (t < b)
            {
                t += h;
                await Task.Delay(2);

                PointF p1 = new PointF((float)(scale * X(t) + cX),
                                       (float)(scale * Y(t) + cY));

                if (green < 255 && red > 0)
                {
                    Pen pen = new Pen(Color.FromArgb(red, green, blue));
                    lock (g)
                    {
                        g.DrawLine(pen, p0, p1);
                    }
                    p0 = p1;
                }

                if (red < 255)
                {
                    green--;
                    red++;
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"Drawing completed in {stopwatch.Elapsed.TotalMilliseconds} ms");
        }
        public static async Task<double> DrawParameters2DAsync(this Graphics g,
     Func<double, double> X, Func<double, double> Y,
     double a, double b, double scale = 1.0, double cX = 0, double cY = 0, double n = 500.0, int threadCount = 4)
        {
            if (a >= b)
            {
                throw new Exception("Invalid interval!");
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int red = 0, green = 255, blue = 0;
            double h = (b - a) / n;

           
            double chunkSize = (b - a) / threadCount;
            var tasks = new Task[threadCount];

        
            ConcurrentBag<Pen> pens = new ConcurrentBag<Pen>();

            for (int i = 0; i < threadCount; i++)
            {
                double startT = a + i * chunkSize;
                double endT = (i == threadCount - 1) ? b : startT + chunkSize;

                tasks[i] = Task.Run(() =>
                {
                    double localT = startT;
                    PointF p0 = new PointF((float)(scale * X(localT) + cX),
                                           (float)(scale * Y(localT) + cY));

                    while (localT < endT)
                    {
                        localT += h;

                        PointF p1 = new PointF((float)(scale * X(localT) + cX),
                                               (float)(scale * Y(localT) + cY));

                        if (green < 255 && red > 0)
                        {
                            Pen pen = new Pen(Color.FromArgb(red, green, blue));
                            pens.Add(pen);

                            lock (g)
                            {
                                g.DrawLine(pen, p0, p1);
                            }

                            p0 = p1;
                        }

                        if (red < 255)
                        {
                            green--;
                            red++;
                        }
                    }
                });
            }

            await Task.WhenAll(tasks);

            stopwatch.Stop();
            double elapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds;
            Console.WriteLine($"Drawing completeeeed in {elapsedMilliseconds} ms with {threadCount} threads");

            return elapsedMilliseconds;
        }



        #endregion
    }
}
