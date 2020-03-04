using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model
{
    public interface INameProvider
    {
        string GetNewName(string typeName);
    }
}
