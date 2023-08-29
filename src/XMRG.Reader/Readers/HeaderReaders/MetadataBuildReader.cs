using Honeycomb.Core;
using Honeycomb.Core.Parsers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMRG.Reader.Headers;

namespace XMRG.Reader.Readers.HeaderReaders;

public class MetadataBuildReader : IParser<MetadataBuild> {

    private readonly IParser<int> intReader;
    private readonly int columnCount;

    public MetadataBuildReader(int columnCount, IParser<int> intReader) {
        this.intReader = intReader;
        this.columnCount = columnCount;
    }

    public (MetadataBuild, ArraySegment<byte>)? Parse(
        ArraySegment<byte> input
    ) =>
        this.intReader
            .SelectMany<int, MetadataBuild>(it => {
                var build = MetadataOps.Parse(it, this.columnCount);
                return build.HasValue
                    ? new Succeed<MetadataBuild>(build.Value)
                    : new Fail<MetadataBuild>();
            })
            .Parse(input);
}
