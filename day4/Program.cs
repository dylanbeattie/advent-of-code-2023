var lines = File.ReadAllLines("input.txt");

var cards = lines.Select(line => new Card(line)).ToList();
foreach (var card in cards) Console.WriteLine(card);

var part1 = cards.Sum(card => card.ScorePart1);
Console.WriteLine(part1);

var part2 = cards.Sum(card => card.ScorePart2);
Console.WriteLine(part2);

class Card {
	private int number;
	private static List<Card> cards = new();
	private int[] winners;
	private int[] numbers;
	public Card(string line) {
		var lists = line.Split(new[] { ':', '|' }).Skip(1).ToArray();
		winners = lists[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)
			.Select(Int32.Parse).ToArray();
		numbers = lists[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
			.Select(Int32.Parse).ToArray();
		cards.Add(this);
		number = cards.Count;
	}

	public int[] Matches => winners.Intersect(numbers).ToArray();
	public int ScorePart1 => (int) Math.Pow(2, Matches.Length - 1);
	private static Dictionary<int,int?> scoresPart2 = new();
	public int? ScorePart2
		=> scoresPart2.GetValueOrDefault(number) ??
		(scoresPart2[number] = 1 + cards.Skip(number).Take(Matches.Length).Sum(c => c.ScorePart2));

	public override string ToString()
		=> $"{number} : {String.Join(',', winners)} U {String.Join(',', numbers)} = {String.Join(',', Matches)} ({ScorePart1})";
}

