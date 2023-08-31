using Honeycomb.Core;
using Honeycomb.Core.Parsers;
using Honeycomb.Core.PrimitiveParsers;

using System;
using System.Linq;
using System.Text;

using XMRG.Reader.Headers;

namespace XMRG.Reader.Readers.HeaderReaders;

public class UserDataReader : IParser<UserData> {

    public (UserData, ReadOnlyMemory<byte>)? Parse(
        ReadOnlyMemory<byte> input
    ) =>
        new NBytes(2)
            .SelectMany(it => {
                var str = Encoding.Default.GetString(it.Span);
                return str switch {
                    "HP" =>
                        new NBytes(8)
                            .Select(userId =>
                                new UserData(
                                    OperatingSystemType.HP,
                                    Encoding.Default.GetString(userId.Span))),
                    "LX" =>
                        new NBytes(8)
                            .Select(userId =>
                                new UserData(
                                    OperatingSystemType.LX,
                                    Encoding.Default.GetString(userId.Span))),
                    _ => new Fail<UserData>()
                };
            })
            .Or(new NBytes(10)
                .Select(userId =>
                    new UserData(
                        OperatingSystemType.Unknown,
                        Encoding.Default.GetString(userId.Span))))
            .Parse(input);
}
