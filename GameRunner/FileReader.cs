using System.IO;

namespace GameRunner
{
    public class FileReader : IDataReader
    {
        public string[] Read(string path)
        {
            string[] lines = File.ReadAllLines(path);

            return lines;
        }
    }
}