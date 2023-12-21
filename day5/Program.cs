Console.WriteLine("Hello!");
var lines = new[] {
			"50 98 2",
			"52 50 48"
		};
var map = new Map(lines);




public class Map {
	public Map() { }
	Dictionary<Int64, Int64?> map = new();
	public Map(string[] lines) {
		foreach (var line in lines) {
			Console.WriteLine($"Mapping {line}");
			var tokens = line.Split(' ').Select(Int64.Parse).ToArray();
			var target = tokens[0];
			var source = tokens[1];
			var length = tokens[2];
			for (var i = 0; i < length; i++) {
				Console.WriteLine($"Mapping {source} to {target}");
				map.Add(source++, target++);
			}
		}
	}
	public Int64 Lookup(Int64 source) => map.GetValueOrDefault(source) ?? source;
}
