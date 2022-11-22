namespace GameRunner
{
    public class Move
    {
        private Logic logic;
        private Labyrinth labyrinth;
        private Directions currentDirection;
        private Dictionary<Directions, List<int>> freeDirections;
        private Dictionary<Directions, List<int>> initialFreeDirections;
        public List<int> steps;

        public Move(Labyrinth labyrinth, Logic logic)
        {
            this.logic = logic;
            this.labyrinth = labyrinth;
            this.logic.previousPointX = this.labyrinth.playerPositionX;
            this.logic.previousPointY = this.labyrinth.playerPositionY;
            this.logic.currentPointX = this.labyrinth.playerPositionX;
            this.logic.currentPointY = this.labyrinth.playerPositionY;
            initialFreeDirections = new Dictionary<Directions, List<int>>();
            initialFreeDirections = FindFreeDirections(this.labyrinth);
            steps = new List<int>();
        }

        public void TryAllFreeIninitialDirections()
        {
            var initialDirections = new Dictionary<Directions, List<int>>(initialFreeDirections);

            foreach (var direction in initialDirections)
            {
                var directions = new Dictionary<Directions, List<int>>(initialFreeDirections);

                if (direction.Value.Count != 0)
                {
                    directions[direction.Key] = new List<int>();
                    freeDirections = directions;

                    GoBackToInitialPosition();
                    GoToExit();
                }
            }
        }

        public Dictionary<Directions, List<int>> FindFreeDirections(Labyrinth labyrinth)
        {
            freeDirections = new Dictionary<Directions, List<int>>();

            freeDirections.Add(Directions.Right, new List<int>());
            freeDirections.Add(Directions.Left, new List<int>());
            freeDirections.Add(Directions.Up, new List<int>());
            freeDirections.Add(Directions.Down, new List<int>());

            if (labyrinth.map[logic.currentPointY][logic.currentPointX + 1] == LabyrinthConstants.FreePath ||
                labyrinth.map[logic.currentPointY][logic.currentPointX + 1] == LabyrinthConstants.PlayersPosition)
            {
                freeDirections[Directions.Right].Add(logic.currentPointX + 1);
                freeDirections[Directions.Right].Add(logic.currentPointY);
            }
            if (labyrinth.map[logic.currentPointY][logic.currentPointX - 1] == LabyrinthConstants.FreePath ||
                labyrinth.map[logic.currentPointY][logic.currentPointX - 1] == LabyrinthConstants.PlayersPosition)
            {
                freeDirections[Directions.Left].Add(logic.currentPointX - 1);
                freeDirections[Directions.Left].Add(logic.currentPointY);
            }
            if (labyrinth.map[logic.currentPointY - 1][logic.currentPointX] == LabyrinthConstants.FreePath ||
                labyrinth.map[logic.currentPointY - 1][logic.currentPointX] == LabyrinthConstants.PlayersPosition)
            {
                freeDirections[Directions.Up].Add(logic.currentPointX);
                freeDirections[Directions.Up].Add(logic.currentPointY - 1);
            }
            if (labyrinth.map[logic.currentPointY + 1][logic.currentPointX] == LabyrinthConstants.FreePath ||
                labyrinth.map[logic.currentPointY + 1][logic.currentPointX] == LabyrinthConstants.PlayersPosition)
            {
                freeDirections[Directions.Down].Add(logic.currentPointX);
                freeDirections[Directions.Down].Add(logic.currentPointY + 1);
            }

            return freeDirections;
        }

        private void GoRight()
        {
            logic.currentPointX++;
        }

        private void GoLeft()
        {
            logic.currentPointX--;
        }

        private void GoUp()
        {
            logic.currentPointY--;
        }

        private void GoDown()
        {
            logic.currentPointY++;
        }

        private void Go()
        {
            logic.previousPointX = logic.currentPointX;
            logic.previousPointY = logic.currentPointY;
            logic.stepsBetweenIntersections++;

            switch (currentDirection)
            {
                case Directions.Right:
                    GoRight();
                    break;
                case Directions.Left:
                    GoLeft();
                    break;
                case Directions.Up:
                    GoUp();
                    break;
                case Directions.Down:
                    GoDown();
                    break;
                default:
                    break;
            }
        }

        private void GoToExit()
        {
            while (!logic.IsItExit(labyrinth.map, logic.currentPointX, logic.currentPointY))
            {
                currentDirection = logic.ChooseDirection(freeDirections);
                Go();

                if (!logic.IsItExit(labyrinth.map, logic.currentPointX, logic.currentPointY))
                {
                    FindFreeDirections(labyrinth);
                }
                else
                {
                    steps.Add(logic.CountSteps());
                }
            }
        }

        private void GoBackToInitialPosition()
        {
            logic.currentPointX = labyrinth.playerPositionX;
            logic.currentPointY = labyrinth.playerPositionY;
            logic.previousPointX = labyrinth.playerPositionX;
            logic.previousPointY = labyrinth.playerPositionY;
            logic.stepsBetweenIntersections = 0;
            logic.stepsList = new List<int>();
            logic.Intersections = new List<(int PointX, int PointY, int PreviousX, int PreviousY, Dictionary<Directions, List<int>> Directions)>();
        }
    }
}
