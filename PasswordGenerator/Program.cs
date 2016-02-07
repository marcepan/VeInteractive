using System;
using PasswordGenerator.Generators;

namespace PasswordGenerator
{
    class Program
    {
        private static void Main()
        {
            var generator = new PassGenerator();
            var validator = new PassValidator(generator);

            Console.WriteLine("{0}", "Login:");
            var login = Console.ReadLine();

            Console.WriteLine("{0} {1}", "Your password is:", generator.Generate(login));
            Console.WriteLine("{0}", "Login:");
            var newLogin = Console.ReadLine();

            while (newLogin != null && !newLogin.Equals(login))
            {
                Console.WriteLine("{0} {1}", "Your password is:", generator.Generate(newLogin));
                Console.WriteLine("{0}", "Login:");
                newLogin = Console.ReadLine();

            }

            Console.WriteLine("{0}", "Password:");
            var password = Console.ReadLine();
            if (validator.ValidatePassword(login, password))
            {
                Console.WriteLine("{0}", "Valid password!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("{0}", "Invalid password!");
                Console.ReadLine();
            }
        }
    }
}
