using Honeycomb.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMRG.Reader.Readers;

public class RowReader : IParser<IReadOnlyCollection<short>> {

    private readonly IParser<short> shortReader;
    private readonly int count;

    public RowReader(IParser<short> shortReader, int count) {
        this.shortReader = shortReader;
        this.count = count;
    }

    public (IReadOnlyCollection<short>, ReadOnlyMemory<byte>)? Parse(
        ReadOnlyMemory<byte> input
    ) =>
        this.shortReader
            .Repeat(this.count)
            .Parse(input);
}
