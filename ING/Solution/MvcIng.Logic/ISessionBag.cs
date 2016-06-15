using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcIng.Logic
{
    public interface ISessionBag<in TKeyEnum>
    {
        T Get<T>(TKeyEnum key);
        void Set<T>(TKeyEnum key, T value);
    }
}
