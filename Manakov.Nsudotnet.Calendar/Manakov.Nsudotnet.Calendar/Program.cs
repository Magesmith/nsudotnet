using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Manakov.Nsudotnet.Calendar
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean switcher = true;
            Boolean switcher2 = false;
            Boolean switcher3 = false;
            int counter = 0;

            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                DateTime firstDate = date.AddDays(-date.Day + 1);
                DateTime workingDate = firstDate.AddDays(((int)firstDate.DayOfWeek == 0) ? (-6) : (1 - (int)firstDate.DayOfWeek));
                DateTime previousDate;

                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                for (DateTime addDate = workingDate; addDate.DayOfYear < workingDate.DayOfYear + 5; addDate = addDate.AddDays(1))
                {
                    Console.Write("|{0,4} ", addDate.ToString("ddd", CultureInfo.CurrentCulture));
                }
                Console.BackgroundColor = ConsoleColor.Red;
                for (DateTime addDate = workingDate.AddDays(5); addDate.DayOfYear < workingDate.DayOfYear + 7; addDate = addDate.AddDays(1))
                {
                    Console.Write("|{0,4} ", addDate.ToString("ddd", CultureInfo.CurrentCulture));
                }
                Console.WriteLine("|");


                while (switcher)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        if (workingDate == firstDate) switcher3 = true;

                        if (i >= 5) Console.BackgroundColor = ConsoleColor.Red;
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            if (switcher3) counter++;
                        }
                        Console.Write("|");
                        if (workingDate.DayOfYear == date.DayOfYear) Console.BackgroundColor = ConsoleColor.Blue;
                        if (workingDate == DateTime.Today) Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write("{0, 4} ", workingDate.Day);
                        workingDate = workingDate.AddDays(1);

                        previousDate = workingDate.AddDays(-1);
                        if ((Math.Abs(workingDate.Day - previousDate.Day) > 25) && (switcher2))
                        {
                            switcher = false;
                            switcher3 = false;
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("|");
                    switcher2 = true;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;

                Console.WriteLine(counter);
                Console.ReadLine();
                return;
            }
            else
            {
                Console.WriteLine("failed to parse");
                Console.ReadLine();
                return;
            }

        }
    }
}
