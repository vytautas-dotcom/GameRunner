namespace GameRunner
{
    public class Labyrinth
    {
        private readonly IDataReader _reader;
        public List<char[]> map;
        public int playerPositionX;
        public int playerPositionY;
        public Labyrinth(IDataReader reader)
        {
            _reader = reader;
            map = new List<char[]>();
        }

        public void CreateMap(string path)
        {
            var data = _reader.Read(path);
            int lineCount = 0;

            foreach (string line in data)
            {
                var chars = line.ToCharArray();
                map.Add(chars);
                int playersXCoordinate = FindPlayersXCoordinate(line);

                if (playersXCoordinate != -1)
                {
                    playerPositionX = playersXCoordinate;
                    playerPositionY = lineCount;
                }
                lineCount++;
            }
        }

        private static int FindPlayersXCoordinate(string line)
            => line.IndexOf(LabyrinthConstants.PlayersPosition);
    }
}
