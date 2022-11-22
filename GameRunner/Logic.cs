namespace GameRunner
{
    public class Logic
    {
        private Random random;

        public int stepsBetweenIntersections = 0;
        public List<int> stepsList;

        public int currentPointX;
        public int currentPointY;
        public int previousPointX;
        public int previousPointY;
        public List<(int PointX, int PointY, int PreviousX, int PreviousY, Dictionary<Directions, List<int>> Directions)> Intersections;
        public Logic()
        {
            random = new Random();
            stepsList = new List<int>();
            Intersections = new List<(int PointX, int PointY, int PreviousX, int PreviousY, Dictionary<Directions, List<int>> Directions)>();
        }
        public Directions ChooseDirection(Dictionary<Directions, List<int>> freeDirections)
        {
            var directions = new List<Directions>();

            foreach (var direction in freeDirections)
            {
                if (direction.Value.Count != 0 && (
                    direction.Value[0] != previousPointX ||
                    direction.Value[1] != previousPointY))
                {
                    directions.Add(direction.Key);
                }
            }

            if (IsItDeadEnd(directions) || IsItSeenIntersection())
            {
                GoBackToPreviousIntersection();

                return ChooseDirection(Intersections[Intersections.Count - 1].Directions);
            }

            if (directions.Count > 1 || (stepsBetweenIntersections == 0 && stepsList.Count == 0))
                SetLastIntersectionPoint(freeDirections);

            var directionIndex = random.Next(0, directions.Count);

            return directions[directionIndex];
        }

        public bool IsItExit(List<char[]> map, int currentPointX, int currentPointY)
        {
            if (currentPointY == 0 || currentPointY == map.Count - 1 ||
               currentPointX == 0 || currentPointX == map[currentPointX].Length - 1)
                return true;
            return false;
        }

        public bool IsItDeadEnd(List<Directions> directions)
        {
            if (directions.Count == 0)
                return true;
            return false;
        }

        public bool IsItSeenIntersection()
        {
            if (Intersections.Count == 0)
                return false;

            var intersectionsExceptLast = new List<(int PointX, int PointY, int PreviousX, int PreviousY, Dictionary<Directions, List<int>>)>();
            intersectionsExceptLast.AddRange(Intersections);
            intersectionsExceptLast.Remove(Intersections[Intersections.Count - 1]);

            foreach (var intersection in intersectionsExceptLast)
            {
                if (intersection.PointX == currentPointX && intersection.PointY == currentPointY)
                    return true;
            }
            return false;
        }

        public int CountSteps()
        {
            int totalSteps = 0;
            foreach (var step in stepsList)
            {
                totalSteps += step;
            }
            return totalSteps + stepsBetweenIntersections;
        }

        private void SetLastIntersectionPoint(Dictionary<Directions, List<int>> directions)
        {
            if (Intersections.Count != 0 &&
               Intersections[Intersections.Count - 1].PointX == currentPointX &&
               Intersections[Intersections.Count - 1].PointY == currentPointY)
                return;

            Intersections.Add((currentPointX, currentPointY, previousPointX, previousPointY, directions));
            stepsList.Add(stepsBetweenIntersections);
            stepsBetweenIntersections = 0;
        }

        private void GoBackToPreviousIntersection()
        {
            currentPointX = Intersections[Intersections.Count - 1].PointX;
            currentPointY = Intersections[Intersections.Count - 1].PointY;
            previousPointX = Intersections[Intersections.Count - 1].PreviousX;
            previousPointY = Intersections[Intersections.Count - 1].PreviousY;

            stepsBetweenIntersections = 0;
        }
    }
}
