using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayersOnWeb.Factory
{
    public abstract class Creator
    {
        public abstract void createWriter(string method);
    }
}
