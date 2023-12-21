using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Shouldly;
using Xunit;

public class MapTests {
	[Theory]
	[InlineData(1, 1)]
	[InlineData(0, 0)]
	[InlineData(Int64.MaxValue, Int64.MaxValue)]
	public void Map_Returns_Input_If_No_Map(Int64 source, Int64 target) {
		var map = new Map();
		map.Lookup(source).ShouldBe(target);
	}

	[Theory]
	[InlineData(98, 50)]
	[InlineData(99, 51)]
	[InlineData(53, 55)]
	[InlineData(10, 10)]
	public void Map_Maps_Maps(long input, long output) {
		var lines = new[] {
			"50 98 2",
			"52 50 48"
		};
		var map = new Map(lines);
		map.Lookup(input).ShouldBe(output);
	}
}
