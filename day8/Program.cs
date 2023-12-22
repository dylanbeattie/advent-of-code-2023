var lines = File.ReadAllLines("input.txt");
var directions = lines[0];
var left = lines.Skip(2).ToDictionary(line => line[..3], line => line[7..10]);
var right = lines.Skip(2).ToDictionary(line => line[..3], line => line[12..15]);

var part1 = CountSteps("AAA", s => s == "ZZZ");
Console.WriteLine($"Part 1: {part1}");

var moduli = left.Keys.Where(k => k.EndsWith('A')).Select(n => CountSteps(n, s => s.EndsWith('Z'))).ToArray();
var part2 = LeastCommonMultiple(moduli);
Console.WriteLine($"Part 2: {part2}");

long CountSteps(string node, Func<string, bool> halt) {
	var i = 0;
	while (! halt(node)) node = (directions[i++ % directions.Length] == 'L' ? left : right)[node];
	return i;
}

long LeastCommonMultiple(long[] numbers) => numbers.Aggregate((a, b) => Math.Abs(a * b) / GreatestCommonDivisor(a, b));
long GreatestCommonDivisor(long a, long b) => b == 0 ? a : GreatestCommonDivisor(b, a % b);
