using System;

namespace TetrisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            
            game.TitleScreen();

            while (true)
            {
                if (Console.KeyAvailable)
                    break;
            }

            game.Start();

            // game loop
            while (true)
            {
                // listen to key presses
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        // pause and unpause the game with P
                        case ConsoleKey.P:

                            if (game.Paused)
                                game.Resume();
                            else
                                game.Pause();

                            break;

                        // send key strokes to the game if it's not paused
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.Q:
                        case ConsoleKey.W:

                            if (!game.Paused)
                                game.Input(key.Key);

                            break;

                        // end the game with ESC
                        case ConsoleKey.Escape:

                            game.Stop();
                            return;
                    }
                }

                if (game.Board.GameOver)
                {
                    game.GameOver();
                    return;
                }
            }
        }
    }
}