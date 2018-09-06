using System;

namespace СockroachRun
{
    public class Field
    {
        public bool isFinished = false;
        public static object locker = new object();

        public void PrintItem(string item, int itemHorizontalPosition, int itemVerticalPosition)
        {
            lock (locker)
            {
                Console.SetCursorPosition(itemHorizontalPosition, itemVerticalPosition);
                Console.CursorVisible = false;
                Console.Write(item);
            }
        }

        public void SetFinishLine()
        {
            Console.SetCursorPosition(2, 0);
            Console.WriteLine("Сockroach,run!:D");
            Console.SetCursorPosition(18, 1);
            Console.Write("|");
            Console.SetCursorPosition(18, 2);
            Console.Write("|");
            Console.SetCursorPosition(18, 3);
            Console.Write("|");
            Console.SetCursorPosition(18, 4);
            Console.Write("|");
        }
    }
}
