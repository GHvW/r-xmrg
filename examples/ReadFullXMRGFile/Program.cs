﻿
using Honeycomb.Core;

using XMRG.Reader;
using XMRG.Reader.Readers;
using XMRG.Reader.Readers.HeaderReaders;

Console.WriteLine("Let's read XMRG");

var bytes = File.ReadAllBytes(@"\xmrg0506199516z.gz");
//var bytes = File.ReadAllBytes(@"\xmrg1107202218z.gz");
//var bytes = File.ReadAllBytes(@"\xmrg6_2022110818f012.gz");
//var bytes = File.ReadAllBytes(@"\xmrg0822202319z\xmrg0822202319z");

var readersResult = new GetPrimitiveReaders().Parse(bytes);

if (readersResult.HasValue) {

    var readers = readersResult.Value.Item1;
    var result = new XMRGReader(readers.Item1, readers.Item2, readers.Item3).Parse(bytes);
    
    if (result != null) {
        var data = result.Value.Item1;
        var avg = data.HeightValues().Where(n => n >= 0).Select(it => it / 100.0).Average();
        var max = data.HeightValues().Where(n => n > 0).Select(it => it / 100.0).Max();

        Console.WriteLine($"Y origin: {data.Bounds.YOrigin}");
        Console.WriteLine($"avg: {avg}");
        Console.WriteLine($"max: {max}");
    } else {
        Console.WriteLine("result was null :(");
    }
}

