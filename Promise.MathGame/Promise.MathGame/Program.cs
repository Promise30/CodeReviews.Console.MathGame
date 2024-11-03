using Promise.MathGame;

while (true) { 
    Console.WriteLine($"""
        Welcome to the Math Game!
        (1) Start a new game
        (2) View history
        (3) Exit
        """);
    Console.Write("Enter your option: ");
    var option = int.TryParse(Console.ReadLine(), out var result) ? result : 0;

    if (option == 1)
    {
        Console.WriteLine("Starting a new game...");
        MathGame.StartGame();
    }
    else if (option == 2)
    {
        var history = MathGame.GetHistory();
        Console.WriteLine($"""
            Game History:
            {history}
        """);
    }
    else if (option == 3)
    {
        Console.WriteLine("Exiting...");
        break;
    }
        continue; 
}

