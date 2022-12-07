var lines = File.ReadAllLines("input.txt");

var usScore = 0;
var themScore = 0;

foreach(var line in lines)
{
    var round = line.Split(" ");

    var themShape = ThemShape(round[0]);
    var usShape = UsShape(themShape, round[1]);

    var us = ShapeValue(usShape);
    var them = ShapeValue(themShape);

    var usRoundScore = OutcomeValue(usShape, themShape);
    var themRoundScore = OutcomeValue(themShape, usShape);

    usScore += (us + usRoundScore);
    themScore += (them + themRoundScore);
    
    // Console.WriteLine($"({usShape} v {themShape}) -> ({us} v {them}) -> ({usRoundScore} v {themRoundScore}) -> ({usScore} v {themScore})");
}

Console.WriteLine($"Final Score - Us: {usScore}, Them: {themScore}");

Shape ThemShape(string input) 
{
    if (input == "A") return Shape.Rock;
    if (input == "B") return Shape.Paper;
    if (input == "C") return Shape.Scissors;

    throw new ArgumentException(nameof(input), $"({input}) is invalid");
}

Shape UsShape(Shape otherShape, string input)
{
    // Draw
    if (input == "Y") return otherShape;

    // Lose
    if (input == "X")
    {
        return otherShape switch 
        {
            Shape.Paper => Shape.Rock,
            Shape.Rock => Shape.Scissors,
            Shape.Scissors => Shape.Paper,
            _ => throw new Exception()
        };
    }

    // Win
    if (input == "Z")
    {
        return otherShape switch 
        {
            Shape.Paper => Shape.Scissors,
            Shape.Rock => Shape.Paper,
            Shape.Scissors => Shape.Rock,
            _ => throw new Exception()
        };
    }

    throw new ArgumentException(nameof(input));
}

int ShapeValue(Shape shape) => shape switch
{
    Shape.Rock => 1,
    Shape.Paper => 2,
    Shape.Scissors => 3,
    _ => throw new ArgumentException(nameof(shape))
};

int OutcomeValue(Shape a, Shape b)
{
    if (a == b) return 3;

    // Win Conditions
    if (a == Shape.Rock && b == Shape.Scissors) return 6;
    if (a == Shape.Paper && b == Shape.Rock) return 6;
    if (a == Shape.Scissors && b == Shape.Paper) return 6;

    return 0;
}

enum Shape
{
    Rock,
    Paper,
    Scissors
}