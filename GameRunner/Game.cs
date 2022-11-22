namespace GameRunner;

public class Game : IGame
{
    private readonly IDataReader _reader;

    public Labyrinth labyrinth;
    public Move move;
    public Logic logic;
    public Game(IDataReader reader)
    {
        _reader = reader;
        labyrinth = new Labyrinth(_reader);
        logic = new Logic();
    }

    public int Run(string filePath)
    {
        labyrinth.CreateMap(filePath);

        move = new Move(labyrinth, logic);

        move.TryAllFreeIninitialDirections();

        return move.steps.Min();
    }
}
