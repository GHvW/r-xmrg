using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMRG.Reader.Headers;

public record Metadata(
    UserData UserData,
    DateTime SavedDateTime,
    ProcessFlag ProcessFlag,
    Post1997Additions? Additions);

public record UserData(OperatingSystemType OSType, string UserId);

public record Post1997Additions(
    DateTime Valid, // ccyy-mm-dd hh:mm:ss
    int MaxValue, // in MM
    float VersionNumber // AWIPS Build Number
);

