using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manakov.Nsudotnet.Calendar
{
    class Program
    {
        static void Main(string[] args)
        {
            IOslave slave = new IOslave();
            DateTime date;
            try
            {
                date = slave.getDate();
            }
            catch (DateParseFailureException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                return;
            }
            Calendar calendar = new Calendar(date.Date);
            calendar.print();

            Console.ReadLine();
        }
    }

    class IOslave
    {
        public DateTime getDate()
        {

            DateTime date;
            if (DateTime.TryParse(Console.ReadLine(), out date))
            {
                return date;
            }
            else
            {
                throw new DateParseFailureException("failed to parse");
            }

        }
    }

    class Calendar
    {

        private DateTime currentDate;
        private Boolean switcher1 = true;
        private Boolean switcher2 = false;
        private Boolean switcher3 = false;
        private int counter = 0;

        public Calendar(DateTime date)
        {
            this.currentDate = date;
        }

        public DateTime getFirstDayofWeek(DateTime inDate)
        {
            switch (inDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return inDate.AddDays(0);
                case DayOfWeek.Tuesday:
                    return inDate.AddDays(-1);
                case DayOfWeek.Wednesday:
                    return inDate.AddDays(-2);
                case DayOfWeek.Thursday:
                    return inDate.AddDays(-3);
                case DayOfWeek.Friday:
                    return inDate.AddDays(-4);
                case DayOfWeek.Saturday:
                    return inDate.AddDays(-5);
            }
            return inDate.AddDays(-6);
        }

        public void print()
        {
            DateTime firstDate = currentDate.AddDays(-currentDate.Day + 1);
            DateTime workingDate = this.getFirstDayofWeek(firstDate);
            DateTime preDate;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("| Mon | Tue | Wen | Thu | Fri ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("| Sat | Sun |");

            while (switcher1)
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
                    if (workingDate.DayOfYear == currentDate.DayOfYear) Console.BackgroundColor = ConsoleColor.Blue;
                    if (workingDate == DateTime.Today) Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("{0, 4} ", workingDate.Day);
                    workingDate = workingDate.AddDays(1);

                    preDate = workingDate.AddDays(-1);
                    if ((Math.Abs(workingDate.Day - preDate.Day) > 25) && (switcher2))
                    {
                        switcher1 = false;
                        switcher3 = false;
                    }
                }
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("|");
                switcher2 = true;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine(counter + " Working days total");

        }

    }

    class DateParseFailureException : Exception
    {
        public DateParseFailureException() { }

        public DateParseFailureException(String message)
            : base(message)
        {
        }

        public DateParseFailureException(String message, Exception inner)
            : base(message)
        {
        }
    }
}
