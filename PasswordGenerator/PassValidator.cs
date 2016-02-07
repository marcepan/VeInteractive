using PasswordGenerator.Generators;

namespace PasswordGenerator
{
    public class PassValidator
    {
        public PassGenerator Generator;

        public PassValidator(PassGenerator generator)
        {
            this.Generator = generator;
        }

        public bool ValidatePassword(string userId, string password)
        {
            var pass = Generator.Generate(userId);

            return pass == password;
        }
    }
}
