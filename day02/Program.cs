var lines = File.ReadAllLines("input.txt");

var usScore = 0;
var themScore = 0;

foreach(var line in lines)
{
    var round = line.Split(" ");

    var usShape = ToShape(round[1]);
    var themShape = ToShape(round[0]);

    var us = ShapeValue(usShape);
    var them = ShapeValue(themShape);

    var usRoundScore = OutcomeValue(usShape, themShape);
    var themRoundScore = OutcomeValue(themShape, usShape);

    usScore += (us + usRoundScore);
    themScore += (them + themRoundScore);
    
    // Console.WriteLine($"({usShape} v {themShape}) -> ({us} v {them}) -> ({usRoundScore} v {themRoundScore}) -> ({usScore} v {themScore})");
}

Console.WriteLine($"Final Score - Us: {usScore}, Them: {themScore}");

Shape ToShape(string input) 
{
    if (input == "A" || input == "X") return Shape.Rock;
    if (input == "B" || input == "Y") return Shape.Paper;
    if (input == "C" || input == "Z") return Shape.Scissors;

    throw new ArgumentException(nameof(input), $"({input}) is invalid");
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