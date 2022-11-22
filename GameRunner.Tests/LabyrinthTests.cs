using Moq;
using Xunit;

namespace GameRunner.Tests
{
    public class LabyrinthTests
    {
        private Labyrinth labyrinth;
        private TestData testData;
        private readonly Mock<IDataReader> _dataReader;
        public LabyrinthTests()
        {
            _dataReader = new Mock<IDataReader>();
            testData = new TestData();
            _dataReader.Setup(x => x.Read("path")).Returns(testData.dataLines);

            labyrinth = new Labyrinth(_dataReader.Object);
        }

        [Fact]
        public void ShouldReadDataAndReturnLabyrinth()
        {

            labyrinth.CreateMap("path");

            Assert.Equal(testData.map, labyrinth.map);
        }
        [Fact]
        public void ShouldEstimatePlayersPositionInLabyrinth()
        {

            labyrinth.CreateMap("path");

            Assert.Equal(2, labyrinth.playerPositionX);
            Assert.Equal(1, labyrinth.playerPositionY);
        }
    }
}
