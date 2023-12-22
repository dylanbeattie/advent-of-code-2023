var lines = File.ReadAllLines("input.txt");
var durations = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(Int32.Parse).ToArray();
var records = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(Int32.Parse).ToArray();

List<Race> races = new();
for (var i = 0; i < durations.Count(); i++)
{
	races.Add(new Race(durations[i], records[i]));
}

foreach (var race in races)
{
	Console.WriteLine(race);
	Console.WriteLine(race.WinCount);
}

Console.WriteLine($"Part 1: {races.Aggregate(1L, (product, race) => product * race.WinCount)}");

lines = File.ReadAllLines("input.txt").Select(s => s.Replace(" ", "")).ToArray();
var duration = Int64.Parse(lines[0].Split(':')[1]);
var record = Int64.Parse(lines[1].Split(':')[1]);

var bigRace = new Race(duration, record);

foreach (var hold in new[] { 10814162, 10814163, 46163630, 46163631 }) {
	Console.WriteLine($"{hold}: {bigRace.Wins(hold)}");
}
Console.WriteLine($"Part 2: {bigRace.SolveQuadratically()}");

public class Race
{
	public override string ToString() => $"Duration: {duration} Record: {record}";
	private long duration;
	private long record;
	public Race(long duration, long record)
	{
		this.duration = duration;
		this.record = record;
	}

	public long WinCount
	{
		get
		{
			long wins = 0;
			for (var hold = 0; hold < duration; hold++)
			{
				var speed = hold;
				var result = (duration - hold) * speed;
				Console.WriteLine($"{hold} => {result}");
				if (result > record) wins++;
			}
			return wins;
		}
	}

	public long SolveQuadratically()
	{
		var a = -1;
		var b = duration;
		var c = -record;
		Console.WriteLine(a);
		Console.WriteLine(b);
		Console.WriteLine(c);
		var s1 = (-b + (Math.Sqrt(b * b - (4 * a * c)))) / (2 * a);
		var s2 = (-b - (Math.Sqrt(b * b - (4 * a * c)))) / (2 * a);
		var lower = Math.Ceiling(Math.Min(s1, s2));
		var upper = Math.Ceiling(Math.Max(s1, s2));
		return (long)(upper - lower);
	}
	public long Distance(long hold) => (duration - hold) * hold;

	public bool Wins(long hold) => Distance(hold) > record;
}

