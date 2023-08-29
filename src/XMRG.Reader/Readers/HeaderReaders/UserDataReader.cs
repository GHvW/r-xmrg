using Honeycomb.Core;
using Honeycomb.Core.Parsers;
using Honeycomb.Core.PrimitiveParsers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMRG.Reader.Headers;

namespace XMRG.Reader.Readers.HeaderReaders;

public class UserDataReader : IParser<UserData> {

    public (UserData, ArraySegment<byte>)? Parse(
        ArraySegment<byte> input
    ) =>
        new NBytes(2)
            .SelectMany(it => {
                var str = Encoding.Default.GetString(it);
                return str switch {
                    "HP" =>
                        new NBytes(8)
                            .Select(userId =>
                                new UserData(
                                    OperatingSystemType.HP,
                                    Encoding.Default.GetString(userId))),
                    "LX" =>
                        new NBytes(8)
                            .Select(userId =>
                                new UserData(
                                    OperatingSystemType.LX,
                                    Encoding.Default.GetString(userId))),
                    _ => new Fail<UserData>()
                };
            })
            .Or(new NBytes(10)
                .Select(userId =>
                    new UserData(
                        OperatingSystemType.Unknown,
                        Encoding.Default.GetString(userId))))
            .Parse(input);
}
