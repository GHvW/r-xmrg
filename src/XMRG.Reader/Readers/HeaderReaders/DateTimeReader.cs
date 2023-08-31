using Honeycomb.Core;
using Honeycomb.Core.PrimitiveParsers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMRG.Reader.Readers.HeaderReaders;

public class DateTimeReader : IParser<DateTime> {

    public (DateTime, ReadOnlyMemory<byte>)? Parse(
        ReadOnlyMemory<byte> input 
    ) => 
        new NBytes(20)
            .Select(bytes => DateTime.Parse(Encoding.Default.GetString(bytes.Span)))
            .Parse(input);
}
