using Honeycomb.Core;

using System;
using System.Linq;

using XMRG.Reader.Headers;

namespace XMRG.Reader.Readers;
public class BoundsReader : IParser<MapBounds> {

    private readonly IParser<int> intParser;

    public BoundsReader(IParser<int> intParser) {
        this.intParser = intParser;
    }

    public ParseResult<MapBounds>? Parse(
        int currentIndex,
        ReadOnlySpan<byte> input
    ) =>
        (from xor in this.intParser
         from yor in this.intParser
         from cols in this.intParser
         from rows in this.intParser
         select new MapBounds(xor, yor, cols, rows))
        .Parse(currentIndex, input);
}
