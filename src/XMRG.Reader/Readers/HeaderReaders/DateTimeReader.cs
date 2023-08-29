using Honeycomb.Core;
using Honeycomb.Core.PrimitiveParsers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMRG.Reader.Readers.HeaderReaders;

public class DateTimeReader : IParser<DateTime> {

    public (DateTime, ArraySegment<byte>)? Parse(
        ArraySegment<byte> input
    ) => 
        new NBytes(20)
            .Select(bytes => DateTime.Parse(Encoding.Default.GetString(bytes)))
            .Parse(input);
}
