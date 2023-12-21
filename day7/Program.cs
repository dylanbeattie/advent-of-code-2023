using day7;

var lines = File.ReadAllLines("input.txt");

var part1Hands = lines.Select(line => new Hand(line)).OrderBy(hand => hand);
var part1 = part1Hands.Select((hand, i) => hand.Bid * (i + 1)).Sum();
Console.WriteLine($"Part 1: {part1}");

var part2Hands = lines.Select(line => new Hand(line, useJokers: true)).OrderBy(hand => hand);
var part2 = part2Hands.Select((hand, i) => hand.Bid * (i + 1)).Sum();
Console.WriteLine($"Part 2: {part2}");
