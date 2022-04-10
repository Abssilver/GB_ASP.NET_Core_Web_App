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
            var regexRemoveExtraSpaces = new Regex("[ ]{2,}", options);
            var regexPlaceDotsBeforeCapitalLetters = new Regex(@"(?=\s+[A-ZА-Я]\w*)\s+", options);
            var regexPointInTheEnd = new Regex(@"[^\w.!?]$", options);
            var resultNoExtraSpaces = regexRemoveExtraSpaces.Replace(str, " ");
            var resultEndStringWithPoint = regexPointInTheEnd.Replace(resultNoExtraSpaces, ".").Trim();
            var resultAddDots = regexPlaceDotsBeforeCapitalLetters.Replace(resultEndStringWithPoint, ". ");
            Console.WriteLine($"Origin: \"{str}\"");
            Console.WriteLine($"Remove extra spaces: \"{resultNoExtraSpaces}\"");
            Console.WriteLine($"Place dot to the end: \"{resultEndStringWithPoint}\"");
            Console.WriteLine($"Add dots into string body: \"{resultAddDots}\"");
            Console.ReadKey();
        }
    }
}