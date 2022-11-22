using System.Collections.Generic;

namespace GameRunner.Tests
{
    public class TestData
    {
        public List<char[]> map;
        public string[] dataLines;
        public Dictionary<Directions, List<int>> directions;
        public List<Directions> deadEnd;
        public List<(int PointX, int PointY, int PreviousX, int PreviousY, Dictionary<Directions, List<int>>)> Intersections;
        public TestData()
        {
            map = new List<char[]>();
            map.Add(new char[] { '1', '1', '1', '1', '1' });
            map.Add(new char[] { '1', ' ', 'X', ' ', '1' });
            map.Add(new char[] { '1', ' ', '1', ' ', '1' });
            map.Add(new char[] { '1', ' ', ' ', ' ', '1' });
            map.Add(new char[] { '1', '1', '1', ' ', '1' });

            dataLines = new string[5];
            dataLines[0] = "11111";
            dataLines[1] = "1 X 1";
            dataLines[2] = "1 1 1";
            dataLines[3] = "1   1";
            dataLines[4] = "111 1";

            directions = new Dictionary<Directions, List<int>>();
            directions.Add(Directions.Right, new List<int>() { 1, 3 });
            directions.Add(Directions.Left, new List<int>() { 1, 1 });
            directions.Add(Directions.Up, new List<int>());
            directions.Add(Directions.Down, new List<int>());

            deadEnd = new List<Directions>();

            Intersections = new List<(int PointX, int PointY, int PreviousX, int PreviousY, Dictionary<Directions, List<int>>)>();
            var firstInterseption = new Dictionary<Directions, List<int>>();
            firstInterseption.Add(Directions.Left, new List<int>() { 1, 1 });
            firstInterseption.Add(Directions.Right, new List<int>() { 3, 1 });
            Intersections.Add((2, 1, 2, 1, firstInterseption));
            var secondInterseption = new Dictionary<Directions, List<int>>();
            secondInterseption.Add(Directions.Up, new List<int>() { 3, 2 });
            secondInterseption.Add(Directions.Left, new List<int>() { 2, 3 });
            secondInterseption.Add(Directions.Down, new List<int>() { 3, 4 });
            Intersections.Add((3, 3, 2, 3, secondInterseption));
        }
    }
}
