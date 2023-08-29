using Honeycomb.Core;
using Honeycomb.Core.Parsers;

using System;
using System.Linq;

using XMRG.Reader.Headers;
using XMRG.Reader.Readers.HeaderReaders;

namespace XMRG.Reader.Readers;

public class MetadataReader : IParser<Metadata> {

    private readonly DateTimeReader dateTimeReader = new();
    private readonly IParser<int> intReader;
    private readonly IParser<float> floatReader;
    private readonly MetadataBuild build;

    public MetadataReader(
        MetadataBuild build,
        IParser<int> intReader,
        IParser<float> floatReader
    ) {
        this.build = build;
        this.intReader = intReader;
        this.floatReader = floatReader;
    }


    public (Metadata, ArraySegment<byte>)? Parse(
        ArraySegment<byte> input
    ) =>
        (this.build switch {
            MetadataBuild.Post1997 =>
                (from userData in new UserDataReader()
                 from savedTime in this.dateTimeReader
                 from processFlag in new ProcessFlagReader()
                 from validTime in this.dateTimeReader
                 from maxVal in this.intReader
                 from versionNum in this.floatReader
                 select new Metadata(userData, savedTime, processFlag, new Post1997Additions(validTime, maxVal, versionNum))),
            MetadataBuild.Build1997 =>
                (from userData in new UserDataReader()
                 from savedTime in this.dateTimeReader
                 from processFlag in new ProcessFlagReader()
                 select new Metadata(userData, savedTime, processFlag, null)),
            MetadataBuild.Pre1997 => new Fail<Metadata>(),
            _ => new Fail<Metadata>()
        })
        .Parse(input);
}
