namespace day7;
public class Hand : IComparable<Hand> {
	public override string ToString() => deal;
	private const string PART1_VALUES = "23456789TJQKA";
	private const string PART2_VALUES = "J23456789TQKA";
	private int[] Cards => deal.Select(d => (useJokers ? PART2_VALUES : PART1_VALUES).IndexOf(d)).ToArray();

	private readonly string deal;
	private readonly bool useJokers;
	public int Bid { get; init; }

	public Hand(string line, bool useJokers) : this(line) {
		this.useJokers = useJokers;
	}

	public Hand(string line) {
		var tokens = line.Split(' ');
		if (tokens.Length > 1) Bid = Int32.Parse(tokens[1]);
		deal = tokens[0];
	}

	public HandType Type => DetermineType(deal);

	private static IEnumerable<string> Shuffle(string input) {
		var index = input.IndexOf('J');
		if (index < 0) {
			yield return input;
		} else {
			foreach (var replacement in PART2_VALUES.Skip(1)) {
				var card = new StringBuilder(input) { [index] = replacement };
				foreach (var c2 in Shuffle(card.ToString())) yield return c2;
			}
		}
	}

	private HandType? bestType = null;
	private HandType BestType => bestType ??= DetermineBestType(deal);

	private static HandType DetermineBestType(string cards) => Shuffle(cards).Select(DetermineType).Max();
	private static HandType DetermineType(string cards) {
		var groups = cards.GroupBy(c => c).ToList();
		return (groups.Count, groups.Max(g => g.Count())) switch {
			(1, _) => HandType.FiveOfAKind,
			(2, 4) => HandType.FourOfAKind,
			(2, 3) => HandType.FullHouse,
			(3, 3) => HandType.ThreeOfAKind,
			(3, 2) => HandType.TwoPair,
			(4, _) => HandType.OnePair,
			(5, _) => HandType.HighCard,
			_ => HandType.Unknown
		};
	}

	public int CompareTo(Hand? that) {
		if (that == default) return 1;
		if (useJokers) {
			if (this.BestType != that.BestType) return (this.BestType - that.BestType);
		} else {
			if (this.Type != that.Type) return (this.Type - that.Type);
		}
		for (var i = 0; i < this.Cards.Length; i++) {
			if (this.Cards[i] != that.Cards[i]) return this.Cards[i] - that.Cards[i];
		}
		return (this.Type - that.Type);
	}
}
