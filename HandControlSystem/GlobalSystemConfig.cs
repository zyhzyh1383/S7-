using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandControlSystem
{
    public class GlobalSystemConfig
    {
        /// <summary>
        ///plcip地址
        /// </summary>
        public string Ipaddress { get; set; }
        /// <summary>
        ///plc机台号
        /// </summary>
        public string MachineTypeNo { get; set; }
        /// <summary>
        /// plc端口号
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// plc插槽号
        /// </summary>
        public string SlotNo { get; set; }
        /// <summary>
        /// plc型号
        /// </summary>
        public string ModelType { get; set; }
        /// <summary>
        /// plc定位置
        /// </summary>
        public double PostionValue { get; set; }
        /// <summary>
        /// 重复次数
        /// </summary>
        public int RepeatTimes { get; set; }
    }
}
