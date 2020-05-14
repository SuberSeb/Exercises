using JsonImporter.Models;
using System;
using System.Text;

namespace JsonImporter.Tools
{
    internal class RandomGenerator
    {
        private int lastNumber;

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();

            var value = random.Next(min, max);

            return value;
        }

        public int IncrementalNumber()
        {
            return lastNumber++;
        }

        public YesOrNoEnum RandomEnum(int probability)
        {
            Random random = new Random();

            if (random.Next(100) < probability)
            {
                return YesOrNoEnum.Yes;
            }
            else
            {
                return YesOrNoEnum.No;
            }
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;

            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            if (lowerCase)
                return builder.ToString().ToLower();

            return builder.ToString();
        }
    }
}