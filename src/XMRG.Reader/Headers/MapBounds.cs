using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMRG.Reader.Headers {

    // First Record of the Header
    public record MapBounds(
        int XOrigin, 
        int YOrigin, 
        int Columns, 
        int Rows);

}
