using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageWebResizer.Helpers
{
    public class Util
    {
        public static double getKB(long? length)
        {
            return length == null ? 0 : Math.Round((long)length / 1024f);
        }
    }
}
