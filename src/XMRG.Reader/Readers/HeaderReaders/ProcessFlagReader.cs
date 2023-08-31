using Honeycomb.Core;
using Honeycomb.Core.PrimitiveParsers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMRG.Reader.Headers;

namespace XMRG.Reader.Readers.HeaderReaders;

public class ProcessFlagReader : IParser<ProcessFlag> {

    public (ProcessFlag, ReadOnlyMemory<byte>)? Parse(
        ReadOnlyMemory<byte> input
    ) =>
        new NBytes(8)
            .Select(data => new ProcessFlag(Encoding.Default.GetString(data.Span)))
            .Parse(input);       
}
