using Moq;
using System.Collections.Generic;
using Xunit;

namespace GameRunner.Tests
{
    public class LogicTests
    {
        private TestData testData;
        private Logic logic;
        private Labyrinth labyrinth;
        private readonly Mock<IDataReader> _dataReader;
        public LogicTests()
        {
            _dataReader = new Mock<IDataReader>();
            testData = new TestData();
            _dataReader.Setup(x => x.Read("path")).Returns(testData.dataLines);
            labyrinth = new Labyrinth(_dataReader.Object);
            logic = new Logic();
        }

        [Fact]
        public void ShouldShooseRightOrLeftDirection()
        {
            var direction = logic.ChooseDirection(testData.directions);

            var isSlectedDirectionCorrect = direction == Directions.Left || direction == Directions.Right;

            Assert.True(isSlectedDirectionCorrect);
        }

        [Theory]
        [InlineData(3, 4, true)]
        [InlineData(3, 3, false)]
        public void ShouldReturnTrueIfGivenPointIsExit(int pointX, int pointY, bool expectedIsItExit)
        {
            labyrinth.CreateMap("path");

            var actualIsItExit = logic.IsItExit(labyrinth.map, pointX, pointY);


            Assert.Equal(expectedIsItExit, actualIsItExit);
        }

        [Fact]
        public void ShouldReturnTrueIfPlayerReachesDeadEnd()
        {
            var isDeadEnd = logic.IsItDeadEnd(testData.deadEnd);

            Assert.Equal(true, isDeadEnd);
        }


        [Fact]
        public void ShouldReturnTrueIfInterseptionIsAlradySeen()
        {
            logic.Intersections = testData.Intersections;
            logic.currentPointX = 2;
            logic.currentPointY = 1;
            var isInterseptionSeen = logic.IsItSeenIntersection();

            Assert.Equal(true, isInterseptionSeen);
        }
    }
}
