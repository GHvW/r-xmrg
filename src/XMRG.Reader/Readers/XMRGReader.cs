using Honeycomb.Core;
using Honeycomb.Core.Parsers;

using System;
using System.Collections.Generic;
using System.Linq;

using XMRG.Reader.Headers;
using XMRG.Reader.Readers.HeaderReaders;

namespace XMRG.Reader.Readers;

public class XMRGReader : IParser<XMRG> {

    private readonly IParser<int> intReader;
    private readonly IParser<float> floatReader;
    private readonly IParser<short> shortReader;

    public XMRGReader(
        IParser<int> intReader,
        IParser<float> floatReader,
        IParser<short> shortReader
    ) {
        this.intReader = intReader;
        this.floatReader = floatReader;
        this.shortReader = shortReader;
    }

    public (XMRG, ReadOnlyMemory<byte>)? Parse(
        ReadOnlyMemory<byte> input
    ) =>
        (from bounds in new Padded<MapBounds>(new BoundsReader(this.intReader))
         from metadata in 
             new MetadataBuildReader(bounds.Columns, this.intReader)
                .SelectMany(build => new MetadataReader(build, this.intReader, this.floatReader))
                .Select(ToNullable)
                .Or(new Succeed<Metadata?>(null))
         from rows in this.Rows(this.shortReader, bounds.Columns, bounds.Rows)
         select new XMRG(bounds, metadata, rows))
        .Parse(input);

    private IParser<IReadOnlyCollection<IReadOnlyCollection<short>>> Rows(
        IParser<short> shortReader,
        int columnCount,
        int rowCount
    ) =>
        new Padded<IReadOnlyCollection<short>>(
            new RowReader(
                shortReader,
                columnCount))
        .Repeat(rowCount);

    private static A? ToNullable<A>(A it) => it;
}
