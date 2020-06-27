using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BtPasswordGenerator.Model
{
    public class PasswordModel
    {

        public string Guid { get; set; }
        public int UserId { get; set; }
        public long CreationDateTicks { get; set; }
    }
}
