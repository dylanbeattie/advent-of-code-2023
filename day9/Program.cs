var lists = File.ReadAllLines("input.txt")
	.Select(line => line.Split(' ').Select(Int32.Parse).ToList())
	.ToList();

var part1 = lists.Select(Extrapolate).Sum();
Console.WriteLine($"Part 1: {part1}");

var part2 = lists.Select(Etalopartxe).Sum();
Console.WriteLine($"Part 2: {part2}");

int Extrapolate(List<int> list)
	=> list.All(i => i == 0) ? 0 : list.Last() + Extrapolate(Crunch(list));

int Etalopartxe(List<int> list)
	=> list.All(i => i == 0) ? 0 : list.First() - Etalopartxe(Crunch(list));

List<int> Crunch(List<int> list)
	=> list.Skip(1).Select((value,index) => value - list[index]).ToList();
		
