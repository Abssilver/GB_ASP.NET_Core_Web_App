using System;
using System.Text.RegularExpressions;

namespace RegexTask
{
    class Program
    {
        static void Main(string[] args)
        {
            //Доработать строку до нормального состояния (удалить лишние пробелы, проставить точки)
            string str = " Предложение один Теперь предложение два Предложение три ";

            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            string result = regex.Replace(str, " ").Trim();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}