using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID19
{
    public class Comboxitem
    {
        public string Ma { get; internal set; }
        public string Ten { get; internal set; }
        public class ComboBoxItem
        {
            public string Ma { get; set; }
            public string Ten { get; set; }
            public string CombinedValue => $"{Ma} - {Ten}";
        }
    }
}
