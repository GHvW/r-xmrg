﻿using Honeycomb.Core;
using Honeycomb.Core.PrimitiveParsers;

using System;
using System.Linq;

namespace XMRG.Reader.Readers;

public class Padded<A> : IParser<A> {

    private readonly IParser<A> reader;
    private readonly IParser<ReadOnlyMemory<byte>> padding = new IntBytes();

    public Padded(IParser<A> reader) {
        this.reader = reader;
    }

    public (A, ReadOnlyMemory<byte>)? Parse(
        ReadOnlyMemory<byte> input
    ) =>
        (from _prefixPad in this.padding
         from data in this.reader
         from _suffixPad in this.padding
         select data)
        .Parse(input);
}
