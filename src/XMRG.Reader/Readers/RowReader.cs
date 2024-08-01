using Honeycomb.Core;

using System;
using System.Collections.Generic;

namespace XMRG.Reader.Readers;

public class RowReader : IParser<IReadOnlyCollection<short>> {

    private readonly IParser<short> shortReader;
    private readonly int count;

    public RowReader(IParser<short> shortReader, int count) {
        this.shortReader = shortReader;
        this.count = count;
    }

    public ParseResult<IReadOnlyCollection<short>>? Parse(
        int currentIndex,
        ReadOnlySpan<byte> input
    ) =>
        this.shortReader
            .Repeat(this.count)
            .Parse(currentIndex, input);
}
