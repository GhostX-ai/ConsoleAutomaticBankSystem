using System;
using System.Collections.Generic;
using CABS.Models;
namespace CABS
{
    class Program
    {
        UAccaount ua = new UAccaount();
        static void Main()
        {

        }
        static void Welcoming()
        {
            Console.Write("1-для регистрации\n2-для входа\n3-для выхода");
            int chs = 0;
            try
            {
                chs = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Welcoming();
            }
            Console.WriteLine();
            switch (chs)
            {
                case 1:
                    {
                        SingUp();
                    }
                    break;
                case 2:
                    {
                        SingIn();
                    }
                    break;
            }
        }
    }
}
