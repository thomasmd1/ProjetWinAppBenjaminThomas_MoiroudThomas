using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WinAppBJ.Utils
{
    class ConvertToMD5
    {

        static public string EncodeTo64(string input)
        {
           
            //encode en MD5
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {
                sb.Append(hash[i].ToString("x2"));
            }

            

            //Encode le password en 64
            var valueB64 = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return System.Convert.ToBase64String(valueB64);

            


        }

    }
}
