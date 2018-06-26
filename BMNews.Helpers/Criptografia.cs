using System;
using System.Security.Cryptography;
using System.Text;

namespace BMNews.Helpers
{
    public static class Criptografia
    {
        public static string Codificador(string senha)
        {
           StringBuilder SenhaCriptografada = new StringBuilder();

            MD5 md5 = MD5.Create();
            byte[] entrada = Encoding.ASCII.GetBytes(senha);
            byte[] Hash = md5.ComputeHash(entrada);

            for (int i = 0; i < Hash.Length; i++)
            {
                SenhaCriptografada.Append(Hash[i].ToString("X2"));
            }


            return Convert.ToString(SenhaCriptografada);
        }

    }
}