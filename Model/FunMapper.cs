using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FunMapper
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        public FunNameEnum FunName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public DataTypeEnum DataTypeEnum { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public OperationTypeEnum OperationTypeEnum { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Variable { get; set; }
        /// <summary>
        /// 第一次单击或者按下鼠标发送的值
        /// </summary>
        public object FirstClickOrDownValue { get; set; }
        /// <summary>
        /// 第二次单击或者松开鼠标发送的值
        /// </summary>
        public object SecondClickOrUpValue { get; set; }
        /// <summary>
        /// 上一次发送给Plc的值
        /// </summary>
        public object LastSendToPlcValue { get; set; }
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool ReadOnly { get; set; } = false;
    }
}
