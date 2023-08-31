using Honeycomb.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMRG.Reader.Headers;

namespace XMRG.Reader.Readers;
public class BoundsReader : IParser<MapBounds> {

    private readonly IParser<int> intParser;

    public BoundsReader(IParser<int> intParser) {
        this.intParser = intParser;
    }

    public (MapBounds, ReadOnlyMemory<byte>)? Parse(
        ReadOnlyMemory<byte> input
    ) =>
        (from xor in this.intParser
         from yor in this.intParser
         from cols in this.intParser
         from rows in this.intParser
         select new MapBounds(xor, yor, cols, rows))
        .Parse(input);
}
