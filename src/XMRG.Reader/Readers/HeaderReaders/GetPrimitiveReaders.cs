using Honeycomb.Core;
using Honeycomb.Core.Parsers;
using Honeycomb.Core.PrimitiveParsers;

using System;
using System.Linq;

namespace XMRG.Reader.Readers.HeaderReaders;

public class GetPrimitiveReaders : IParser<(IParser<int>, IParser<float>, IParser<short>)> {

    private readonly BigInt bigIntReader = new();
    private readonly LittleInt littleIntReader = new();

    private IParser<(IParser<int>, IParser<float>, IParser<short>)> Bigs(
        int n
    ) =>
        n == 16 
            ? new Succeed<(IParser<int>, IParser<float>, IParser<short>)>((this.bigIntReader, new BigFloat(), new BigShort()))
            : new Fail<(IParser<int>, IParser<float>, IParser<short>)>();

    private IParser<(IParser<int>, IParser<float>, IParser<short>)> Smalls(
        int n
    ) =>
        n == 16 
            ? new Succeed<(IParser<int>, IParser<float>, IParser<short>)>((this.littleIntReader, new LittleFloat(), new LittleShort()))
            : new Fail<(IParser<int>, IParser<float>, IParser<short>)>();

    // header is 4 int's long, if count returned by int reader is 16, endianness is correct
    public ((IParser<int>, IParser<float>, IParser<short>), ReadOnlyMemory<byte>)? Parse(
        ReadOnlyMemory<byte> input
    ) =>
         this.bigIntReader
            .SelectMany(Bigs)
            .Or(this.littleIntReader.SelectMany(Smalls))
            .Parse(input);
}
