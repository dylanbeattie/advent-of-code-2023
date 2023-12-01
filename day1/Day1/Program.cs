using System.ComponentModel;
using System.Globalization;

var lines = File.ReadAllLines("input.txt");
var part1 = 0;
foreach (var line in lines)
{
    var digits = line.Where(c => c >= '0' && c <= '9');
    var value = $"{digits.First()}{digits.Last()}";
    part1 += Int32.Parse(value);
}
Console.WriteLine(part1);

var map = new Dictionary<string, string> {
    { "one", "1" },
    { "two", "2" },
    { "three", "3" },
    { "four", "4" },
    { "five", "5" },
    { "six", "6"},
    {"seven", "7"},
    {"eight", "8"},
    {"nine", "9" }
};
var part2 = 0;
foreach (var line in lines)
{
    string digits = "";
    for(var i = 0; i < line.Length; i++) {
        foreach(var(token,number) in map) {
            if (line[i..].StartsWith(token)) {
                digits += number;
                continue;
            }
        }
        if (line[i] >= '0' && line[i] <= '9') digits += line[i];
    }
    var value = $"{digits.First()}{digits.Last()}";
    
    Console.WriteLine(line + " - " + digits + " - " + value);
    part2 += Int32.Parse(value);
}
Console.WriteLine(part2);
