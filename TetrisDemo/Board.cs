using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisDemo
{
    public class Board
    {
        private int[,] _map;
        private ActivePiece _activePiece;
        public bool GameOver;

        public Board()
        {
            _map = Maps.EmptyBoard();
            _activePiece = new ActivePiece();
            GameOver = false;
        }

        public void Draw()
        {
            var map = GetCompleteMap();
            var mapWidth = map.GetLength(1);
            var mapHeight = map.GetLength(0);
            var windowWidth = Console.WindowWidth;
            var windowHeight = Console.WindowHeight;
            var posX = windowWidth / 2 - mapWidth;
            var posY = windowHeight / 2 - mapHeight / 2;

            Console.CursorVisible = false;
            
            /*
            fill background except for board and score/level, to enable black background when resizing during game.
            Causes problems.
            
            Console.BackgroundColor = ConsoleColor.Black;
            for (var i = 0; i < windowHeight; i++)
            {
                for (var j = 0; j < windowWidth; j++)
                {
                    if (i >= posY && i < posY + mapHeight && j >= posX && j < posX + mapWidth * 2)
                    {
                        continue;
                    }

                    if (i == 0 && j < 13 || i == 1 && j < 9) 
                    {
                        continue;
                    }
                    
                    Console.SetCursorPosition(j, i);
                    Console.Write(" ");
                }
            }
            */

            for (var i = 0; i < mapHeight; i++)
            {
                for (var j = 0; j < mapWidth; j++)
                {
                    
                    ConsoleColor color;
                    
                    switch (map[i, j])
                    {
                        case (0):
                            color = ConsoleColor.Black;
                            break;
                        case(1):
                            color = ConsoleColor.White;
                            break;
                        case(2):
                            color = ConsoleColor.Cyan;
                            break; 
                        case(3):
                            color = ConsoleColor.Blue;
                            break; 
                        case(4):
                            color = ConsoleColor.DarkYellow;
                            break; 
                        case(5):
                            color = ConsoleColor.Yellow;
                            break; 
                        case(6):
                            color = ConsoleColor.Green;
                            break; 
                        case(7):
                            color = ConsoleColor.DarkMagenta;
                            break; 
                        case(8):
                            color = ConsoleColor.Red;
                            break; 
                        default:
                            color = ConsoleColor.Black;
                            break;
                    }

                    Console.BackgroundColor = color;
                    Console.SetCursorPosition(posX + (j * 2), posY + i);
                    Console.Write("  ");
                }
            }
     
            var scoreText = "Score: " + Stats.GetScore();
            var levelText = "Level: " + Stats.GetLevel();
            
            Console.ForegroundColor = ConsoleColor.White;
            
            for (var i = 0; i < scoreText.Length; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(scoreText[i]);
                
            }
            
            for (var i = 0; i < levelText.Length; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write(levelText[i]);
                
            }
        }

        private int[,] GetCompleteMap()
        {
            var completeMap = (int[,]) _map.Clone();
            var pieceMap = _activePiece.Map;
            var width = pieceMap.GetLength(1);
            var height = pieceMap.GetLength(0);
            var x = _activePiece.Coordinates.X;
            var y = _activePiece.Coordinates.Y;
            
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (pieceMap[i, j] != 0)
                        completeMap[y + i, x + j] = pieceMap[i, j];
                }
            }
            
            return completeMap;
        }
        
        private bool IsValidPosition()
        {
            var boardMap = _map;
            var pieceMap = _activePiece.Map;
            var width = pieceMap.GetLength(0);
            var height = pieceMap.GetLength(1);
            var x = _activePiece.Coordinates.X;
            var y = _activePiece.Coordinates.Y;
            
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (pieceMap[i, j] != 0 && boardMap[y + i, x + j] != 0)
                        return false;
                }
            }

            return true;
        }

        private int[,] RemoveCompleteLines(int[,] map)
        {
            
            var height = map.GetLength(0);
            var width = map.GetLength(1);
            var completeLines = new List<int>();
            var counter = 0;

            for (var i = 0; i < height - 3; i++)
            {
                for (var j = 3; j < width - 3; j++)
                {
                    if (map[i, j] != 0)
                        counter++;

                    if (counter == 10)
                        completeLines.Add(i);
                }

                counter = 0;
            }
            
            if (!completeLines.Any())
                return map;

            foreach (var line in completeLines)
            {
                var partialMap = new List<List<int>>();
                var row = new List<int>();
                
                for (var i = 0; i < line ; i++)
                {
                    for (var j = 3; j < width - 3; j++)
                    {
                        row.Add(map[i, j]);
                    }
                    partialMap.Add(row);
                    row = new List<int>();
                }
                
                for (var i = 0; i <= line ; i++)
                {
                    for (var j = 3; j < width - 3; j++)
                    {
                        if (i == 0)
                        {
                            map[i, j] = 0;
                        }
                        else
                        {
                            map[i, j] = partialMap[i - 1][j - 3];
                        }
                    }
                }
            }
            
            Stats.AddCompletedLines(completeLines.Count);
            return map;
        }

        private void LockActivePiece()
        {
            _map = RemoveCompleteLines(GetCompleteMap());
            _activePiece = new ActivePiece();
            Draw();
            
            if (!IsValidPosition())
            {
                GameOver = true;
            }
        }
        

        public void MoveActivePiece(string movement)
        {
            var previousPosition = _activePiece.DeepCopy();

            switch (movement)
            {
                case("left"):
                    _activePiece.MoveLeft();
                    break;
                case("right"):
                    _activePiece.MoveRight();
                    break;
                case("rotateLeft"):
                    _activePiece.RotateLeft();
                    break;
                case("rotateRight"):
                    _activePiece.RotateRight();
                    break;
                case("down"):
                    _activePiece.MoveDown();
                    break;
                default:
                    _activePiece = previousPosition;
                    return;
            }

            if (IsValidPosition())
            {
                Draw();
                return;
            }
            
            _activePiece = previousPosition;

            if (movement == "down")
            {
                LockActivePiece();
            }
        }
    }
}