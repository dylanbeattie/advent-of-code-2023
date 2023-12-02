var lines = File.ReadAllLines("input.txt");
var games = lines.Select(line => new Game(line));
var part1 = games.Where(g => g.Possible).Sum(g => g.Id);
Console.WriteLine(part1);

var part2 = games.Sum(g => g.Power);
Console.WriteLine(part2);

public class Game {
    public int Id { get; init; }
    public List<Glimpse> Glimpses {get; init;}
    public Game(string line) {
        Id = Int32.Parse(line.Split(new[] { ' ', ':' })[1]);
        Glimpses = line.Split(':')[1].Split(';').Select(token => new Glimpse(token)).ToList();
    }

    public int Power => 
        Glimpses.Max(g => g.Red) 
        *
        Glimpses.Max(g => g.Green)
        *
        Glimpses.Max(g => g.Blue);

    public bool Possible => this.Glimpses.All(g => g.Possible);

    public class Glimpse{
        public bool Possible => Red <= 12 && Green <= 13 && Blue <= 14;
        public int Red {get;init;}
        public int Green{get;init;}
        public int Blue { get;init;}
        public Glimpse(string input) {
            foreach(var token in input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)) {
                var bits = token.Split(' ');
                if (bits.Length != 2) continue;
                var count = Int32.Parse(bits[0]);
                switch(bits[1]) {
                    case "red": this.Red = count; break;
                    case "blue":this.Blue = count; break;
                    case "green":this.Green = count;break;
                }
            }
        }
    }
}