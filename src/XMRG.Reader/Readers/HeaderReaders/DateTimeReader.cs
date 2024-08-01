using Honeycomb.Core;
using Honeycomb.Core.PrimitiveParsers;

using System;
using System.Linq;
using System.Text;

namespace XMRG.Reader.Readers.HeaderReaders;

public class DateTimeReader : IParser<DateTime> {

    public ParseResult<DateTime>? Parse(
        int currentIndex,
        ReadOnlySpan<byte> input 
    ) => 
        new NBytes(20)
            .Select(bytes => DateTime.Parse(Encoding.Default.GetString(bytes.Span)))
            .Parse(currentIndex, input);
}
