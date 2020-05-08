namespace TetrisDemo
{
    public static class Stats
    {
        private static int _speed = 500;
        private static int _completedLines = 0;
        private static int _level = 1;
        private static int _score = 0;
        
        public static void AddCompletedLines(int numberOfLines)
        {
            _completedLines += numberOfLines;

            if (numberOfLines == 4)
            {
                _score += 800;
            }
            else
            {
                _score += numberOfLines * 100;
            }

            _level = _completedLines / 5 + 1;
            _speed = 540 - (_level * 40);
        }

        public static string GetScore()
        {
            return _score.ToString("000000");
        }
        
        public static string GetLevel()
        {
            return _level.ToString("00");
        }
        
        public static int GetSpeed()
        {
            return _speed;
        }
    }
}