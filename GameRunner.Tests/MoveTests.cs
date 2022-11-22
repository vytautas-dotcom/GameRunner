using Moq;
using System.Linq;
using Xunit;

namespace GameRunner.Tests
{
    public class MoveTests
    {
        private Move move;
        private Labyrinth labyrinth;
        private Logic logic;
        private readonly Mock<IDataReader> _dataReader;
        private TestData testData;
        public MoveTests()
        {
            _dataReader = new Mock<IDataReader>();
            testData = new TestData();
            _dataReader.Setup(x => x.Read("path")).Returns(testData.dataLines);
            labyrinth = new Labyrinth(_dataReader.Object);
        }

        [Fact]
        public void ShouldEstimateLeftOrRightDirection()
        {
            labyrinth.CreateMap("path");

            logic = new Logic();
            move = new Move(labyrinth, logic);

            var directions = move.FindFreeDirections(labyrinth);

            var containsDirection = directions.ContainsKey(Directions.Left) ||
                directions.ContainsKey(Directions.Right);

            Assert.True(containsDirection);
        }

        [Fact]
        public void ShouldTryAllInitialDirections()
        {
            labyrinth.CreateMap("path");

            logic = new Logic();
            move = new Move(labyrinth, logic);

            move.TryAllFreeIninitialDirections();

            Assert.Equal(2, move.steps.Count);
        }

        [Fact]
        public void ShouldReturn4Steps()
        {
            labyrinth.CreateMap("path");

            logic = new Logic();
            move = new Move(labyrinth, logic);

            move.TryAllFreeIninitialDirections();

            Assert.Equal(4, move.steps.Min());
        }
    }
}