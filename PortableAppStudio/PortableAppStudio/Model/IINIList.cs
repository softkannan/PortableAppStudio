using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model
{
    public interface IINIList : IList
    {
        void UpdateIndex();
    }
}
