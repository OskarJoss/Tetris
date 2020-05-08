using System;

namespace TetrisDemo
{
    public static class Background
    {
        public static void Fill()
        {
            var height = Console.WindowHeight;
            var width = Console.WindowWidth;

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(j, i);
                    Console.Write(" ");
                }
            }
        }
    }
}