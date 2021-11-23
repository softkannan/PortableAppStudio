using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Utility
{
    public class ClipBoardExInfo
    {
        private static ClipBoardExInfo _inst = new ClipBoardExInfo();

        public static ClipBoardExInfo Inst { get => _inst; }
        public ClipBoardExInfo()
        {
        }

        public object Tag { get; set; }
    }

    public class PairValue
    {
        public PairValue(string leftSideVal, string rightSideVale)
        {
            Left = leftSideVal;
            Right = rightSideVale;
        }

        public string Left { get; private set; }
        public string Right { get; private set; }
    }

}
