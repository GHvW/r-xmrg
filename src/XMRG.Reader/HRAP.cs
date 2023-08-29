
using System;

namespace XMRG.Reader;

public record HRAP(double X, double Y) : ICoordinate { }

public static class HRAPOps {

    public static LatLong ToLatLong(this HRAP hrap) {
        var earthr = 6371.2;
        var stlon = 105.0;
        var raddeg = 180.0 / Math.PI;
        var xmesh = 4.7625;
        var tlat = 60.0 / raddeg;

        var _x = hrap.X - 401.0; // >
        var _y = hrap.Y - 1601.0; // >

        var rr = (_x * _x) + (_y * _y);

        var gi = (earthr * (1.0 + Math.Sin(tlat))) / xmesh;
        var _gi = gi * gi;

        var rlat = Math.Asin((_gi - rr) / (_gi + rr)) * raddeg;

        var ang = Math.Atan2(_y, _x) * raddeg;

        ang += ang < 0.0 ? 360.0 : 0.0;

        var rlon = 270.0 + stlon - ang;

        rlon += rlon < 0.0 ? 360.0 : 0.0;

        rlon -= rlon > 360.0 ? 360.0 : 0.0;

        return new LatLong(rlon, rlat);
    }
}