using System.Security.Cryptography;
using System.Text;

namespace BMNews.Helpers
{
    public class CriptografiaHelper
    {
        public static byte[] ConverterEmBytes(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        public static byte[] CriptografarSenha(string senha)
        {
            return Criptografar(senha, "1B%1A&R6R0(A!<%<M+A>!N12%S!>A(}");
        }

        public static byte[] Criptografar(string texto, string salt)
        {
            while (salt.Length < 6)
            {
                salt += salt + "Z";
            }
            using (var sha = SHA512.Create())
            {
                salt = Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(salt.Substring(salt.Length - 4))));
                return sha.ComputeHash(Encoding.UTF8.GetBytes(texto + salt));
            }
        }
    }
}
