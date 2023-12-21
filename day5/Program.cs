var input = File.ReadAllText("input.txt");
var chunks = input.Split("\n\n");
var seeds = chunks[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse).ToList();
var maps = chunks.Skip(1).Select(chunk => new Map(chunk.Split("\n").Skip(1)));

List<long> locations = new();
foreach (var seed in seeds) {
	var v = seed;
	foreach (var map in maps) v = map.Lookup(v);
	locations.Add(v);
}
Console.WriteLine($"Part 1: {locations.Min()}");

var ranges = seeds.Chunk(2);
var spam = maps.Reverse();
for(var i = 0L; i < Int64.MaxValue; i++) {
	if (i % 10000 == 0) Console.WriteLine(i);
	var v = i;
	foreach(var map in spam) v = map.ReverseLookup(v);
	foreach(var range in ranges) {
		if (v > range.First() && v <= range.First() + range.Last()) {
			Console.WriteLine($"Part 2: {i}");
			Environment.Exit(0);
		}
	}
}

public class Map {
	public Map() { }
	List<(long Target, long Source, long Length)> maps = new();
	public Map(IEnumerable<string> lines) {
		foreach (var line in lines) {
			//Console.WriteLine($"Mapping {line}");
			var tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
			if (tokens.Length != 3) continue;
			var target = tokens[0];
			var source = tokens[1];
			var length = tokens[2];
			maps.Add((target, source, length));
		}
	}
	public long Lookup(long source) {
		var map = maps.FirstOrDefault(m => source >= m.Source && source < m.Source + m.Length);
		if (map == default) return source;
		var offset = source - map.Source;
		return map.Target + offset;
	}

	public long ReverseLookup(long target) {
		var map = maps.FirstOrDefault(m => target >= m.Target && target < m.Target + m.Length);
		if (map == default) return target;
		var offset = target - map.Target;
		return map.Source + offset;
	}
}
