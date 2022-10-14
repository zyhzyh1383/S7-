
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CommonFun
    {
        public static List<KeyValuePair<string, string>> GetSource(string[] strs)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            foreach (var str in strs)
            {
                var item = new KeyValuePair<string, string>(str, str);
                list.Add(item);
            }
            return list;
        }
    }
}
