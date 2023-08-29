using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMRG.Reader.Headers;

public enum MetadataBuild {
    Pre1997,
    Build1997,
    Post1997,
}

public static class MetadataOps {

    public static MetadataBuild? Parse(int byteCount, int columnCount) =>
        byteCount switch {
            66 => MetadataBuild.Post1997,
            38 => MetadataBuild.Build1997,
            var c when c == columnCount * 2 => MetadataBuild.Pre1997,
            _ => null
        };
}
