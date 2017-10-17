using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy.Tools
{
    public class TreeGridModel
    {
        public int level { get; set; }

        public string parent { get; set; }

        public bool isLeaf { get; set; }

        public bool expanded { get; set; }

        public bool loaded { get; set; }
    }
}
