using System;
using System.IO;
using System.Linq;

namespace PasswordCheck2
{
    public delegate void Message(string message);
    public class Password
    {
        public event Message Error;
        public event Message Success;
        private readonly int lenght;
        private readonly string symbols1;
        private readonly string symbols2;
        private readonly string symbols3;
        private readonly string symbols4;

        private readonly string symbols;
        public Password()
        {
            lenght = 8;
            symbols1 = "QWERTYUIOPASDFGHJKLZXCVBNM";
            symbols2 = "qwertyuiopasdfghjklzxcvbnm";
            symbols3 = "!@#$%^&*()=+;,.";
            symbols4 = "123456789";

            symbols = symbols1 + symbols2 + symbols3 + symbols4;
        }

        public bool MinLenght(string password)
        {
            if(password.Length < lenght)
            {
                Error?.Invoke($"пароль содержит меньше {lenght} символов");
                return false;
            }
            else
            {
                Success?.Invoke("Пароль имеет достаточное количество символов");
                return true;
            }
        }
        public bool CheckSymbols(string password)
        {
            int pass_check1 = password.IndexOfAny(symbols1.ToCharArray());
            int pass_check2 = password.IndexOfAny(symbols2.ToCharArray());
            int pass_check3 = password.IndexOfAny(symbols3.ToCharArray());
            int pass_check4 = password.IndexOfAny(symbols4.ToCharArray());
            if(pass_check1==-1 || pass_check2==-1||pass_check3==-1||pass_check4==-1)
            {
                Error?.Invoke("Пароль не соответствует минимальным требованиям");
                return false;
            }
            else
            {
                Success?.Invoke("Пароль соответствует минимальным требованиям ");
                return true;
            }
        }
        public bool CheckAphabet(string password)
        {
            foreach(var symbol in password)
            {
                if(!symbols.Contains(symbol))
                {
                    Error?.Invoke("Пароль содержит запрещенные символы");
                    return false;
                }
            }
            Success?.Invoke("Пароль не содержит запрещнные символы");
            return true;
        }

    }
}
