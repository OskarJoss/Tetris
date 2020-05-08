using System;

namespace TetrisDemo
{
    public class ActivePiece
    {
        public Coordinates Coordinates { get; private set; }
        public int[,] Map { get; private set; }

        public ActivePiece()
        {
            Coordinates = new Coordinates(0, 6);
            Map = Maps.RandomPiece();
        }

        public void MoveLeft()
        {
            Coordinates.X--;
        }

        public void MoveRight()
        {
            Coordinates.X++;
        }
        
        public void MoveDown()
        {
            Coordinates.Y++;
        }

        public void RotateLeft()
        {
            var size = Map.GetLength(0);
            var map = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[size - j - 1, i] = Map[i, j];
                }
            }

            Map = map;
        }
        
        public void RotateRight()
        {
            var size = Map.GetLength(0);
            var map = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[j, size - i - 1] = Map[i, j];
                }
            }

            Map = map;
        }
        
        public ActivePiece DeepCopy()
        {
            var copy = (ActivePiece) MemberwiseClone();
            copy.Coordinates = new Coordinates(Coordinates.Y, Coordinates.X);
            copy.Map = (int[,]) Map.Clone();
            return copy;
        }
    }
}