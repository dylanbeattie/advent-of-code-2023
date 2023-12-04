using System.Security.Cryptography;
using System.Text.RegularExpressions;

var grid = File.ReadAllLines("input.txt");
Number? number = null;
List<Number> numbers = new();
for (var row = 0; row < grid.Length; row++) {
    for (var col = 0; col < grid[0].Length; col++) {
        var c = grid[row][col];
        if (c.IsDigit()) {
            number ??= new Number(row, col, grid);
            number.AddDigit(c);
        } else if (number != null) {
            numbers.Add(number);
            number = null;
        }
    }
}

foreach (var num in numbers) Console.WriteLine(num);
Console.WriteLine();

var part1 = numbers
    .Where(n => n.IsAdjacentToSymbol())
    .Sum(n => n.Value);

Console.WriteLine($"Part 1: {part1}");

var part2 = numbers
    .SelectMany(n => n.AdjacentSymbols)
    .GroupBy(pair => pair.Item1)
    .Where(group => group.Key.Character == '*')
    //.Select(group => group.ToArray());
    .Where(group => group.Count() == 2)
    .Sum(group => group.Aggregate(1, (acc, tuple) => acc * tuple.Item2.Value));
Console.WriteLine(part2);



    //.Where(g => g.Key.Item3 == '*');
    ;

struct Symbol(int row, int col, char symbol) {
    public char Character => symbol;
	public override string ToString() {
		return $"[{row},{col}]: {symbol}";
	}
}

class Number(int row, int col, string[] grid) {

    public override string ToString() {
        return $"[{row},{col} ({(IsAdjacentToSymbol() ? 'Y' : 'N')})] : {Digits} {Value}";
    }

    public void AddDigit(char digit) {
        this.Digits += digit;
    }
    public int Value => Int32.Parse(Digits);
    string Digits { get; set; } = String.Empty;

    int rMin => Math.Max(row - 1, 0);
    int rMax => Math.Min(row + 2, grid.Length);
    int cMin => Math.Max(col - 1, 0);
    int cMax => Math.Min(col + Digits.Length + 1, grid[0].Length);

    public IEnumerable<(Symbol, Number)> AdjacentSymbols {
        get {
            for (var r = rMin; r < rMax; r++) {
                for (var c = cMin; c < cMax; c++) {
                    if (grid[r][c].IsSymbol()) {
                        yield return (new Symbol(r, c, grid[r][c]), this);
                    }
                }
            }
        }
    }

    public bool IsAdjacentToSymbol() {
        for (var r = rMin; r < rMax; r++) {
            for (var c = cMin; c < cMax; c++) {
                if (grid[r][c].IsSymbol()) return true;
            }
        }
        return false;
    }
}

public static class Extensions {
    public static bool IsDigit(this char c) => c >= '0' && c <= '9';
    public static bool IsSymbol(this char c)
        => c != '.' && !c.IsDigit();
}