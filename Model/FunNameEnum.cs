using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum FunNameEnum
    {
        [Description("上升")]
        Up,
        [Description("下降")]
        Down,
        [Description("定位")]
        Position,
        [Description("原点")]
        Origin,
        [Description("复位")]
        Reset,
        [Description("开始测试")]
        StartTest,
        [Description("测试复位")]
        TestReset,
        [Description("测试气抓")]
        TestGrap,
        [Description("气抓准备OK")]
        GrapPrepareOk,
        [Description("清除数据")]
        ClearData
    }
}
