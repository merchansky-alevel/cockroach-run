using System;
using System.Threading;
using System.Threading.Tasks;
using Сockroach;

namespace СockroachRun
{
    class Game
    {
        private static Random rnd = new Random();
        private static readonly object locker = new object();
        private static bool haveWinner = false;
        private static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

        public void Run()
        {
            Task t1 = new Task(() =>
            {
                Сockroach<string> c1 = new Сockroach<string>("#");
                int HorizontalPosition = 0;
                Field field = new Field();

                while (HorizontalPosition >= 10 ? cancelTokenSource.IsCancellationRequested : true)
                {
                    string value = HorizontalPosition == 0 ? "|" : SetSpaces(HorizontalPosition) + c1.Logo;
                    Thread.Sleep(rnd.Next(1000, 2000));
                    HorizontalPosition = HorizontalPosition + 1;
                    field.PrintItem(value, 1, HorizontalPosition);
                    SetWinner(HorizontalPosition, c1.Logo);
                }
            }, cancelTokenSource.Token);
            t1.Start();

            Task t2 = new Task(() =>
            {
                Сockroach<string> c2 = new Сockroach<string>("$");
                int HorizontalPosition = 0;
                Field field = new Field();

                while (HorizontalPosition >= 10 ? cancelTokenSource.IsCancellationRequested : true)
                {
                    string value = HorizontalPosition == 0 ? "|" : SetSpaces(HorizontalPosition) + c2.Logo;
                    HorizontalPosition = HorizontalPosition + 1;
                    Thread.Sleep(rnd.Next(1000, 2000));
                    field.PrintItem(value, 2, HorizontalPosition);
                    SetWinner(HorizontalPosition, c2.Logo);
                }
            }, cancelTokenSource.Token);
            t2.Start();

            Task t3 = new Task(() =>
            {
                Сockroach<string> c3 = new Сockroach<string>("@");
                int HorizontalPosition = 0;
                Field field = new Field();

                while (HorizontalPosition >= 10 ? cancelTokenSource.IsCancellationRequested : true)
                {
                    string value = HorizontalPosition == 0 ? "|" : SetSpaces(HorizontalPosition) + c3.Logo;
                    Thread.Sleep(rnd.Next(1000, 2000));
                    HorizontalPosition = HorizontalPosition + 1;
                    field.PrintItem(value, 3, HorizontalPosition);
                    SetWinner(HorizontalPosition, c3.Logo);
                }
            }, cancelTokenSource.Token);
            t3.Start();

            Task t4 = new Task(() =>
            {
                Сockroach<string> c4 = new Сockroach<string>("&");
                int HorizontalPosition = 0;
                Field field = new Field();

                while (HorizontalPosition >= 10 ? cancelTokenSource.IsCancellationRequested : true)
                {
                    string value = HorizontalPosition == 0 ? "|" : SetSpaces(HorizontalPosition) + c4.Logo;
                    Thread.Sleep(rnd.Next(1000, 2000));
                    HorizontalPosition = HorizontalPosition + 1;
                    field.PrintItem(value, 4, HorizontalPosition);
                    SetWinner(HorizontalPosition, c4.Logo);
                }
            }, cancelTokenSource.Token);
            t4.Start();

            new Field().SetFinishLine();
        }

        private static void SetWinner(int currentPosition, string playerLogo)
        {
            lock (locker)
            {
                if (haveWinner)
                {
                    return;
                }
                else
                {
                    if (currentPosition >= 10)
                    {
                        haveWinner = true;
                        Console.SetCursorPosition(0, 7);
                        Console.WriteLine($" Winner is: '{playerLogo}', Congratulations!");
                    }
                }
            }
        }

        private static string SetSpaces(int currentPosition)
        {
            lock (locker)
            {
                string empties = "";

                for (int i = 0; i < currentPosition; i++)
                {
                    empties += " ";
                }

                return empties;
            }
        }
    }
}
