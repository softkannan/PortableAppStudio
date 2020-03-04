using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model
{
    public class InteliSense
    {
        private static InteliSense _inst = new InteliSense();

        public static InteliSense Inst
        {
            get
            {
                return _inst;
            }
        }

        InteliSense()
        {
            EnvVaraibles = new AutoCompleteStringCollection();

            string filename = PathManager.Init.GetResourcePath("EnvIntellisense.txt");
            if (File.Exists(filename))
            {
                EnvVaraibles.AddRange(File.ReadLines(filename).ToArray());
            }

        }

        public AutoCompleteStringCollection EnvVaraibles { get; set; }
    }
}
