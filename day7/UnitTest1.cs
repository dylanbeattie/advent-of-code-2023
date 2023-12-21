using Shouldly;

namespace day7; 

public class HandTests {
	[Theory]
	[InlineData("AAAAA 0", HandType.FiveOfAKind)]
	[InlineData("A2222 0", HandType.FourOfAKind)]
	[InlineData("22A22 0", HandType.FourOfAKind)]
	[InlineData("9KKKK 0", HandType.FourOfAKind)]
	[InlineData("AA222 0", HandType.FullHouse)]
	[InlineData("97979 0", HandType.FullHouse)]
	[InlineData("32T3K 0", HandType.OnePair)]
	[InlineData("KK677 0", HandType.TwoPair)]
	[InlineData("KTJJT 0", HandType.TwoPair)]
	[InlineData("T55J5 0", HandType.ThreeOfAKind)]
	[InlineData("QQQJA 0", HandType.ThreeOfAKind)]
	[InlineData("A2345 0", HandType.HighCard)]
	public void HandType_Parses_Properly(string line, HandType type) {
		var hand = new Hand(line);
		Assert.Equal(hand.Type, type);
	}

	[Theory]
	[InlineData("AAAAA", "AAAA2")]
	[InlineData("AAAAA", "A2345")]
	[InlineData("KK677", "KTJJT")]
	[InlineData("QQQJA", "T55J5")]
	public void Ranks(string h1, string h2) {
		var hand1 = new Hand(h1);
		var hand2 = new Hand(h2);
		hand1.ShouldBeGreaterThan(hand2);
	}
}
