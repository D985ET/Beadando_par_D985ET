using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public static class ExtensionGraphics
    {
   
        #region ParametricCurve2D

        //Szekvenciális
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
        //1. megoldás:
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
        //2. megoldás:
        public static double DrawParameters2DAsync02(this Graphics g,
       Func<double, double> X, Func<double, double> Y,
       double a, double b, double scale = 1.0, double cX = 0, double cY = 0, double n = 500.0, int threadCount = 4)
        {
            if (a >= b)
            {
                throw new Exception("Invalid interval!");
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double h = (b - a) / n;
            double chunkSize = (b - a) / threadCount;
            Thread[] threads = new Thread[threadCount];
            ManualResetEventSlim[] threadCompletionEvents = new ManualResetEventSlim[threadCount];

            // Thread-safe queue to store draw actions
            ConcurrentBag<Tuple<PointF, PointF, Pen>> lineSegments = new ConcurrentBag<Tuple<PointF, PointF, Pen>>();

            for (int i = 0; i < threadCount; i++)
            {
                int threadIndex = i; // Capture loop variable
                double startT = a + threadIndex * chunkSize;
                double endT = (threadIndex == threadCount - 1) ? b : startT + chunkSize;

                threadCompletionEvents[threadIndex] = new ManualResetEventSlim(false);

                threads[threadIndex] = new Thread(() =>
                {
                    double localT = startT;
                    PointF p0 = new PointF((float)(scale * X(localT) + cX),
                                           (float)(scale * Y(localT) + cY));

                    int localRed = 0, localGreen = 255, localBlue = 0; // Ensure colors are local to each thread

                    while (localT < endT)
                    {
                        localT += h;

                        PointF p1 = new PointF((float)(scale * X(localT) + cX),
                                               (float)(scale * Y(localT) + cY));

                        if (localGreen < 255 && localRed > 0)
                        {
                            Pen pen = new Pen(Color.FromArgb(localRed, localGreen, localBlue));

                            lineSegments.Add(Tuple.Create(p0, p1, pen)); // Store line segment for later drawing

                            p0 = p1;
                        }

                        if (localRed < 255)
                        {
                            localGreen--;
                            localRed++;
                        }
                    }

                    threadCompletionEvents[threadIndex].Set(); // Mark thread as completed
                });

                threads[threadIndex].Start();
            }

            // Wait for all threads to finish
            foreach (var e in threadCompletionEvents)
            {
                e.Wait();
            }

            // Draw everything in a single thread to avoid race conditions
            foreach (var segment in lineSegments)
            {
                g.DrawLine(segment.Item3, segment.Item1, segment.Item2);
            }

            stopwatch.Stop();
            double elapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds;
            Console.WriteLine($"Drawing completed in {elapsedMilliseconds} ms with {threadCount} threads");

            return elapsedMilliseconds;
        }

        // 3. megoldás:
        //PARALLEL for
        public static double DrawParameters2DAsync03(this Graphics g,
    Func<double, double> X, Func<double, double> Y,
    double a, double b, double scale = 1.0, double cX = 0, double cY = 0, double n = 500.0, int threadCount = 4)
        {
            if (a >= b)
            {
                throw new Exception("Invalid interval!");
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double h = (b - a) / n;
            double chunkSize = (b - a) / threadCount;

            // Thread-safe collection to store drawing instructions
            ConcurrentBag<Tuple<PointF, PointF, Pen>> lineSegments = new ConcurrentBag<Tuple<PointF, PointF, Pen>>();

            // Use Parallel.For for efficient thread pool usage
            Parallel.For(0, threadCount, i =>
            {
                double startT = a + i * chunkSize;
                double endT = (i == threadCount - 1) ? b : startT + chunkSize;

                double localT = startT;
                PointF p0 = new PointF((float)(scale * X(localT) + cX),
                                       (float)(scale * Y(localT) + cY));

                int localRed = 0, localGreen = 255, localBlue = 0; // Local color variables per thread

                while (localT < endT)
                {
                    localT += h;

                    PointF p1 = new PointF((float)(scale * X(localT) + cX),
                                           (float)(scale * Y(localT) + cY));

                    if (localGreen < 255 && localRed > 0)
                    {
                        Pen pen = new Pen(Color.FromArgb(localRed, localGreen, localBlue));
                        lineSegments.Add(Tuple.Create(p0, p1, pen));
                        p0 = p1;
                    }

                    if (localRed < 255)
                    {
                        localGreen--;
                        localRed++;
                    }
                }
            });

            // Draw everything sequentially to avoid race conditions
            foreach (var segment in lineSegments)
            {
                g.DrawLine(segment.Item3, segment.Item1, segment.Item2);
            }

            stopwatch.Stop();
            double elapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds;
            Console.WriteLine($"Drawing completed in {elapsedMilliseconds} ms with {threadCount} threads (Parallel.For)");

            return elapsedMilliseconds;
        }


        #endregion
    }
}
