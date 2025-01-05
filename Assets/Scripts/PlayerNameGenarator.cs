using System;
using System.Text;

namespace DefaultNamespace
{
    public static class PlayerNameGenarator
    {
        private const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string GetPlayerName(string currentName)
        {
            if (string.IsNullOrEmpty(currentName))
            {
                return GenerateRandomName();
            }

            return currentName;
        }

        private static string GenerateRandomName()
        {
            return "jogador#" + GenerateRandomString(5);
        }

        private static string GenerateRandomString(int length)
        {
            StringBuilder randomString = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                randomString.Append(validChars[random.Next(validChars.Length)]);
            }

            return randomString.ToString();
        }
    }
}