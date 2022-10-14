using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum MouseEnum
    {
        [Description("鼠标按下")]
        MouseDownValue,
        [Description("鼠标松开")]
        MouseUpValue
    }
}
