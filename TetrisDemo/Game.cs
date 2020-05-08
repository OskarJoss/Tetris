using System;
using System.Drawing;

namespace TetrisDemo
{
    class Game
    {
        ScheduleTimer _timer;
        public Board Board;

        public bool Paused { get; private set; }

        public Game()
        {
            Board = new Board();
        }
        public void Start()
        {
            Background.Fill();
            Board.Draw();
            ScheduleNextTick();
        }

        public void TitleScreen()
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Background.Fill();
            
            var posX = Console.WindowWidth / 2;
            var posY = Console.WindowHeight / 2;
            var line1 = "Press any key to start";
            var line2 = "Move: Arrow Keys";
            var line3 = "Rotate: Q, W";
            var line4 = "Pause: P";
            
            Console.SetCursorPosition(posX - line1.Length / 2, posY - 3);
            Console.Write(line1);
            Console.SetCursorPosition(posX - line2.Length / 2, posY - 1);
            Console.Write(line2);
            Console.SetCursorPosition(posX - line3.Length / 2, posY + 1);
            Console.Write(line3);
            Console.SetCursorPosition(posX - line4.Length / 2, posY + 3);
            Console.Write(line4);
        }

        public void GameOver()
        {
            Background.Fill();
            
            var posX = Console.WindowWidth / 2;
            var posY = Console.WindowHeight / 2;
            var line1 = "Game Over";
            var line2 = "Final Score: " + Stats.GetScore();
            
            Console.SetCursorPosition(posX - line1.Length / 2, posY);
            Console.Write(line1);
            Console.SetCursorPosition(posX - line2.Length / 2, posY + 2);
            Console.Write(line2);
        }

        public void Input(ConsoleKey key)
        {
            switch (key)
            {
                case(ConsoleKey.LeftArrow):
                    Board.MoveActivePiece("left");
                    break;
                case(ConsoleKey.RightArrow):
                    Board.MoveActivePiece("right");
                    break;
                case(ConsoleKey.DownArrow):
                    Board.MoveActivePiece("down");
                    break;
                case(ConsoleKey.Q):
                    Board.MoveActivePiece("rotateLeft");
                    break;
                case(ConsoleKey.W):
                    Board.MoveActivePiece("rotateRight");
                    break;
            }
        }

        public void Pause()
        {
            _timer.Pause();
            Paused = true;
        }

        public void Resume()
        {
            _timer.Resume();
            Paused = false;
        }

        public void Stop()
        {
            /*Console.WriteLine("Stop");*/
        }

        void Tick()
        {
            Board.MoveActivePiece("down");
            ScheduleNextTick();
        }

        void ScheduleNextTick()
        {
            _timer = new ScheduleTimer(Stats.GetSpeed(), Tick);
        }
    }
}