using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniversalCalculator;

internal class Program
{
    static void Main(string[] args)
    {
            
        Console.WriteLine("Это консольное приложение является калькулятором для систем счисления от 1 до 50 включительно, а также служит калькулятором для перевода чисел в римскую СС.");
        Console.WriteLine("Выполнил Lipenkov A.D. Группа: ПрИ-101");

        CreateBorder();

        GetHelp();
        CreateBorder();
        GetInput();

    }

    private static void GetHelp()
    {
        Console.WriteLine("Операция: 1 - перевод числа из любой СС в любую другую СС.");
        Console.WriteLine("Операция: 2 - перевод числа в римскую СС.");
        Console.WriteLine("Операция: 3 - перевод из римской СС");
        Console.WriteLine("Операция: 4 - суммирование в любой СС. НЕ РАБОТАЕТ");
        Console.WriteLine("Операция: 5 - вычитание в любой СС. НЕ РАБОТАЕТ");
        Console.WriteLine("Операция: 6 - умножение в любой СС. НЕ РАБОТАЕТ");
        Console.WriteLine("Операция: 7 - вызвать список команд программы.");

    }

    private static int GetInput()
    {
        Console.WriteLine("Введите число, соответствующее операции, которую вы хотите выполнить:");
        while (true)
        {
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int command) || !(command >= 1 && command <= 7))
            {
                Console.WriteLine("Введите существующую команду!");
                GetInput();
            }
            switch (command)
            {
                case 1:
                    FirstFunction();
                    break;
                case 2:
                    SecondFunction();
                    break;
                case 3:
                    ThirdFunction();
                    break;
                case 4:
                    FourthFunction();
                    break;
                case 5:
                    FifthFunction();
                    break;
                case 6:
                    SixFunction();
                    break;
                case 7:
                    GetHelp();
                    break;
                default:
                    Console.WriteLine("К сожалению, что-то пошло не так.");
                    break;

            }
        }
    }

    private static void CreateBorder()
    {
        for (int i = 0; i < Console.WindowWidth; i++)
        {
            Console.Write("=");
        }
    }

    private static char ConvertNumberToSymbol(int modul)
    {
        if (modul >= 0 && modul <= 9) return (char)('0' + modul);
        if (modul >= 10 && modul <= 36) return (char)('A' + (modul - 10));
        if (modul >= 37 && modul <= 62) return (char)('a' + (modul - 36));

        throw new ArgumentException("Некорректный остаток от деления!");
    }

    private static int ConvertFromAnyToDec(string number, int numberBase)
    {
        if (numberBase > 50)
            throw new ArgumentException("Основание некорректо! Оно должно быть в пределе от 1 до 50 включительно!");
        int result = 0;
        int digitsCount = number.Length;
        int num;

        Console.WriteLine("Разбиваем число на отдельные символы.");
        var builder = new StringBuilder();
        builder.Append("Символы:");
        foreach (char c in number.ToCharArray())
        {
            builder.Append($" {c}");
        }
        Console.WriteLine(builder.ToString());

        Console.WriteLine("Теперь начинаем перевод в десятичную систему счисления.");
        Console.WriteLine("Изначально результат вычисления 0.");

        if (numberBase == 1)
        {
            Console.WriteLine("Чтобы число из 1-СС перевести в 10-СС нужно просто подсчитать, сколько 1 в этом числе. Полученное число и будет искомым число в 10-СС");
            int res = 0;
            for (int i = 0; i < number.Length; i++)
                res++;
            if (res != number.Length) throw new ArgumentException("Некорректное число! Попробуйте еще раз");
            return res;
        }
        for (int i = 0; i < digitsCount; i++)
        {
            char symbol = number[i];

            if (symbol >= '0' && symbol <= '9') num = symbol - '0';

            else if (symbol >= 'A' && symbol <= 'Z') num = symbol - 'A' + 10;
            else if (symbol >= 'a' && symbol <= 'z') num = symbol - 'a' + (('Z' - 'A') + 1) + 10;
            else throw new ArgumentException("Некорректное число!");

            if (num >= numberBase) throw new ArgumentException("Исходная строка имеет некорректные символы в обозначении чисел.");

            Console.WriteLine($"Умножаем результат на основание СС: {numberBase}, затем прибавляем число: {num}, соответствующее {i + 1} элементу числа.");

            result *= numberBase;
            result += num;
            Console.WriteLine($"({result / numberBase} * {numberBase}) + {num} = {result}");
            //Console.WriteLine($"({result} * {numberBase}) + {num}");
        }
        Console.WriteLine($"В ходе манипуляций получаем новое число: {result}");
        return result;
    }

    private static string ConvertFromDecToAny(int number, int numberBase)
    {

        if (numberBase > 50)
            throw new ArgumentException("Основание некорректо! Оно должно быть в пределе от 1 до 50 включительно!");
        StringBuilder builder = new StringBuilder();

        Console.WriteLine($"Теперь {number} переведем из 10-СС в {numberBase}");
        do
        {
            Console.WriteLine($"Делим с остатком {number} на {numberBase}. При этом остаток приписываем к числу-результату. ");
            int mod = number % numberBase;
            char symbol = ConvertNumberToSymbol(mod);
            Console.WriteLine($"{builder} + {mod}");
            builder.Append(symbol);
            number /= numberBase;

        } while (number >= numberBase);

        if (number != 0)
        {
            builder.Append(ConvertNumberToSymbol(number));
            Console.WriteLine($"Делим с остатком {number} на 10. При этом остаток приписываем к числу-результату. ");
        }

        Console.WriteLine($"Получаем число {builder.ToString()}. Но это еще не результат. Чтобы получить корректное нужно его записать наоборот: {string.Join("", builder.ToString().Reverse())}");
        string result = string.Join("", builder.ToString().Reverse());

        return result;
    }

    private static void FirstFunction()
    {
        Console.WriteLine("Введите число:");
        string originNumber = Console.ReadLine();
        Console.WriteLine("Введите систему счисления этого числа");
        int originNumberBase = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите систему счисления, в которую сконвертировать число");
        int toWhatBase = int.Parse(Console.ReadLine());

        int toDec = ConvertFromAnyToDec(originNumber, originNumberBase);
        Console.WriteLine($"Ваше новое число {ConvertFromDecToAny(toDec, toWhatBase)} в системе счисления {toWhatBase}");
    }

    private static void SecondFunction()
    {
        int[] rim = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        string[] arab = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

        Console.WriteLine("Введите число в диапазоне от 1 до 5000");
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int number) || !(number >= 1 && number <= 5000)) throw new ArgumentException("Некорректное число! Введите число от 1 до 5000");
        int i;
        i = 0;
        string output = "";
        var otigin = number;
        while (number > 0)
        {
            if (rim[i] <= number)
            {
                Console.WriteLine($"{number} - {rim[i]} = {number - rim[i]}");
                Console.WriteLine($"Число {arab[i]}, соответствующее {rim[i]} приписываем справа. И так до 0.");
                number = number - rim[i];
                output = output + arab[i];
            }
            else i++;

        }
        Console.WriteLine($"Получаем новое число {output} из исходного {otigin}");
    }

    private static void ThirdFunction()
    {
        Console.WriteLine("Введите число в римской СС");
        string input = Console.ReadLine();
        int[] rim = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        string[] arab = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

        for (int i = 0; i < input.Length; i++)
        {
            bool isCorrect = false;
            for (int j = 0; j < arab.Length; j++)
            {
                if (input[i].ToString() == arab[j])
                {
                    isCorrect = true;
                    break;
                }
            }

            if (!isCorrect) throw new ArgumentException("Некорректное число!");
        }

        Console.WriteLine($"Разбиваем число {input} на символы: {string.Join(" ", input.Split(""))}");

        int result = 0;
        var RomToArab = new Dictionary<char, int>
            {{ 'I', 1 },{ 'V', 5 },{ 'X', 10 },{ 'L', 50 },{ 'C', 100 },{ 'D', 500 },{ 'M', 1000 } };
        for (short i = 0; i < input.Length - 1; ++i)
        {
            if (RomToArab[input[i]] < RomToArab[input[i + 1]])
            {
                Console.WriteLine($"Число слева {RomToArab[input[i]]} меньше числа справа {RomToArab[input[i + 1]]} , поэтому вычитаем из результирующега числа левое {RomToArab[input[i]]}");
                result -= RomToArab[input[i]];
            }
            else if (RomToArab[input[i]] >= RomToArab[input[i + 1]])
            {
                Console.WriteLine($"Число слева {RomToArab[input[i]]} больше, чем число справа {RomToArab[input[i + 1]]}, то прибавляем к результирующему числу левое {RomToArab[input[i]]}");
                result += RomToArab[input[i]];
            }
            Console.WriteLine($"Получили текущее {result}");
        }
        result += RomToArab[input[^1]];
        Console.WriteLine($"Получили текущее {result}");
        Console.WriteLine($"Финальное число: {result}!");

    }

    private static void FourthFunction()
    {

    }

    private static void FifthFunction()
    {
        Console.WriteLine("Введите систему счисления для операции над числами:");
        string ss = Console.ReadLine();
        Console.WriteLine("Введите делимое:");
        string number = Console.ReadLine();
        Console.WriteLine("Введите делитель:");
        string del = Console.ReadLine();
        int based = Convert.ToInt32(ss);
        //Валидация своеобразная.
        int numberCorrected = ConvertFromAnyToDec(number, based);
        int delCorrected = ConvertFromAnyToDec(del, based);

        bool isNumberNegative = ConvertFromAnyToDec(number, based) < ConvertFromAnyToDec(del, based);

    }
        
    private static void SixFunction()
    {
        Console.WriteLine("Введите систему счисления для операции над числами:");
        string ss = Console.ReadLine();
        
    } 
}