using System;
using System.Threading;
using System.Threading.Tasks;
using Сockroach;

namespace СockroachRun
{
    class Game
    {
        private static Random _rnd = new Random();
        private static CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private static bool _cancellationRequested = _cancelTokenSource.IsCancellationRequested;
        private static readonly object _locker = new object();
        private static bool _haveWinner = false;
        private static int _currentLead = 0;

        public void Run()
        {
            new Field().SetFinishLine();
            _cancellationRequested = true;

            Task[] tasks = new Task[]
            {
                new Task(() =>
                {
                    Field field = new Field();
                    Сockroach<string> c1 = new Сockroach<string>("#");
                    int HorizontalPosition = 0;

                    while (_cancellationRequested)
                    {
                        CheckMoves(ref HorizontalPosition, 1, field, c1.Logo, _rnd.Next(50, 100));
                    }
                }),
                new Task(() =>
                {
                    Field field = new Field();
                    Сockroach<string> c2 = new Сockroach<string>("$");
                    int HorizontalPosition = 0;

                    while (_cancellationRequested)
                    {
                        CheckMoves(ref HorizontalPosition, 2, field, c2.Logo, _rnd.Next(50, 100));
                    }
                }),
                new Task(() =>
                {
                    Field field = new Field();
                    Сockroach<string> c3 = new Сockroach<string>("@");
                    int HorizontalPosition = 0;

                    while (_cancellationRequested)
                    {
                        CheckMoves(ref HorizontalPosition, 3, field, c3.Logo, _rnd.Next(50, 100));
                    }
                }),
                new Task(() =>
                {
                    Field field = new Field();
                    Сockroach<string> c4 = new Сockroach<string>("&");
                    int HorizontalPosition = 0;

                    while (_cancellationRequested)
                    {
                        CheckMoves(ref HorizontalPosition, 4, field, c4.Logo, _rnd.Next(50, 100));
                    }
                })
            };

            foreach (var item in tasks)
            {
                item.Start();
            }
        }

        private void CheckMoves(ref int horizontalPosition, int verticalPosition, Field field, string itemLogo, int sleep)
        {
            _currentLead = horizontalPosition;

            lock (_locker)
            {
                if (_currentLead == 10)
                {
                    _cancellationRequested = false;
                }
                else
                {
                    string value = horizontalPosition == 0 ? "|" : SetSpaces(horizontalPosition) + itemLogo;
                    Thread.Sleep(sleep);
                    horizontalPosition = horizontalPosition + 1;
                    field.PrintItem(value, horizontalPosition, verticalPosition);
                    SetWinner(horizontalPosition, itemLogo);
                }
            }
        }

        private static void SetWinner(int currentPosition, string playerLogo)
        {
            lock (_locker)
            {
                if (!_haveWinner)
                {
                    if (currentPosition >= 10)
                    {
                        _haveWinner = true;
                        Console.SetCursorPosition(0, 7);
                        Console.WriteLine($" Winner is: '{playerLogo}', Congratulations!");
                    }
                }
            }
        }

        private static string SetSpaces(int currentPosition)
        {
            lock (_locker)
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
