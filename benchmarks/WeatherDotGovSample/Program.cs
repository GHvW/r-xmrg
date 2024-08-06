// See https://aka.ms/new-console-template for more information

// run with `dotnet run -c release`
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Honeycomb.Core;

using System;
using System.IO;

using XMRG.Reader.Readers;
using XMRG.Reader.Readers.HeaderReaders;

var summary = BenchmarkRunner.Run<ParseFile>();

[MemoryDiagnoser]
public class ParseFile {

    private IParser<int> readInt;
    private IParser<float> readFloat;
    private IParser<short> readShort;
    private byte[] bytes;

    public ParseFile() {
        //var bytes = File.ReadAllBytes(@"\xmrg1107202218z.gz");
        //var bytes = File.ReadAllBytes(@"\xmrg6_2022110818f012.gz");
        //var bytes = File.ReadAllBytes(@"\xmrg0822202319z\xmrg0822202319z");
        var filePath = Environment.GetEnvironmentVariable("XMRG_TEST_FILE_DIR") + "\\xmrg0506199516z.gz";
        var bytes = 
            File.ReadAllBytes(filePath)
                ?? throw new Exception("Couldn't read bytes");

        this.bytes = bytes;

        var (readers, _) = new GetPrimitiveReaders().Parse(0, bytes) ?? throw new Exception("Didn't work");

        this.readInt = readers.Item1;
        this.readFloat = readers.Item2;
        this.readShort = readers.Item3;
    }


    [Benchmark]
    public XMRG.Reader.XMRG ReadIt() {
        var (result, _) = 
            new XMRGReader(
                this.readInt, 
                this.readFloat, 
                this.readShort)
            .Parse(0, bytes) 
            ?? throw new Exception("Bad read");

        return result;
    }
}
