using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy.Tools
{
    public class TreeModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public bool Checked { get; set; }
        public string attributes { get; set; }
        public List<TreeModel> children { get; set; }
    }
}
