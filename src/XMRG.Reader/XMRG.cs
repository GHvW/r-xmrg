using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMRG.Reader.Headers;

namespace XMRG.Reader;

public record XMRG(
    MapBounds Bounds,
    Metadata? Metadata,
    IReadOnlyCollection<IReadOnlyCollection<short>> Data);


public static class XMRGOps {

    public static IEnumerable<Feature> Features(this XMRG xmrg) {
        foreach (var (row, rowNumber) in xmrg.Data.Zip(Enumerable.Range(0, xmrg.Data.Count))) {
            foreach (var (item, columnNumber) in row.Zip(Enumerable.Range(0, row.Count))) {
                yield return new Feature(
                    new HRAP(columnNumber, rowNumber).ToLatLong(), 
                    item);
            }
        }      
    }

    public static IEnumerable<int> HeightValues(this XMRG xmrg) {
        foreach (var (row, rowNumber) in xmrg.Data.Zip(Enumerable.Range(0, xmrg.Data.Count))) {
            foreach (var (item, _) in row.Zip(Enumerable.Range(0, row.Count))) {
                yield return item;           }
        }      
    }
}

