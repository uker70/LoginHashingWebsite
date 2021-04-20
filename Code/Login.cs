using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginHashing
{
    class Login
    {
        public static int attempts = 1;
        public static bool loggedIn { get; set; }

        //login method that gets username and password
        public static bool VerifyLogin(string username, string password)
        {
            string[] output = null;
            //tries to call the database and a try/catch to get any errors
            try
            {
                output = Database.Login(username);
            }
            catch
            {
                return false;
            }

            //converts inputted password and the salt from database to byte arrays
            try
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] salt = Convert.FromBase64String(output[1]);

                //hashes the inputted password with the salt
                string hashedPassword = Convert.ToBase64String(Hash.HashLoop(passwordBytes, salt));

                //checks if the hashed password matches the password from the database
                if (output[0] == hashedPassword)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //create user method that gets a username and password
        public static void CreateUser(string username, string password)
        {
            //generates salt with 32 length
            string salt = Convert.ToBase64String(NumberGenerator.Generate(32));

            //hashes the password with the generated salt
            byte[] hashedPassword = Hash.HashLoop(Encoding.UTF8.GetBytes(password), Convert.FromBase64String(salt));

            //creates the new user in the database
            Database.Create(username, Convert.ToBase64String(hashedPassword), salt);
        }
    }
}
