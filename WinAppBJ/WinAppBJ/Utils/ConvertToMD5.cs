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

        static public string EncodeToMD5AndB64(string password)
        {
           
            //Conversion du mot de passe en MD5 
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);

            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            //Ici on parcourt chaque caractère du mot de passe
            for (int i = 0; i < hash.Length; i++)

            {
                sb.Append(hash[i].ToString("x2"));
            }

            

            //Conversion du mot de passe en Base 64
            var valueB64 = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return System.Convert.ToBase64String(valueB64);

            


        }

    }
}
