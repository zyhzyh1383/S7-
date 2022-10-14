using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum OperationTypeEnum
    {
        [Description("切换")]
        Switch,
        [Description("按住")]
        Hold
    }
}
