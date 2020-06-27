using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BtPasswordGenerator.BusinessLogic.Helpers
{
    public class EncodingHelper
    {

        public static string Base64Encode(byte[] plainTextBytes)
        {
            return System.Convert.ToBase64String(plainTextBytes);
        }
        
        public static byte[] Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return base64EncodedBytes;
        }
    }
}
