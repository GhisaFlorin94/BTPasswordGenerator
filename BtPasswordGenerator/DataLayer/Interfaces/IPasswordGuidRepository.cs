using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BtPasswordGenerator.DataLayer.Interfaces
{
    public interface IPasswordGuidRepository
    {
        bool StoreGuid(string guid);
        public IEnumerable<string> ReadActiveGuid();
    }
}
