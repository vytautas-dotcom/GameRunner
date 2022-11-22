using GameRunner;

IDataReader reader = new FileReader();
IGame game = new Game(reader);

var result = game.Run(@"TestData\map1.txt");

Console.WriteLine(result);