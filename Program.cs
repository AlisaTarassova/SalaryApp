using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Sisestage aastaarv: ");
        int year = int.Parse(Console.ReadLine());

        using (TextWriter file = new StreamWriter($"C:\\Users\\37254\\Csv files\\{year}.csv"))
        {
            file.WriteLine("Month, Payday, Reminder day");

            for (int month = 1; month <= 12; month++)
            {
                DateTime payDay = FindPayDay(year, month);

                DateTime reminderDay = FindReminderDay(payDay);

                file.WriteLine($"{new DateTime(year, month, 1).ToString("MMMM", CultureInfo.InvariantCulture)}," +
                    $"{payDay.ToString("dd.MM.yyyy")}," +
                    $"{reminderDay.ToString("dd.MM.yyyy")}");
            }
        }
        Console.WriteLine($"File {year}.csv was created.");
    }

    private static DateTime FindPayDay(int year, int month)
    {
        DateTime payDay = new DateTime(year, month, 10);
        if (payDay.DayOfWeek == DayOfWeek.Saturday || payDay.DayOfWeek == DayOfWeek.Sunday)
        {
            payDay = GetPreviousWeekday(payDay);
        }

        return payDay;
    }

    private static DateTime FindReminderDay(DateTime payDay)
    {
        DateTime reminderDay = payDay.AddDays(-3);
        if (reminderDay.DayOfWeek == DayOfWeek.Saturday || reminderDay.DayOfWeek == DayOfWeek.Sunday)
        {
            reminderDay = GetPreviousWeekday(reminderDay);
        }

        return reminderDay;
    }

    static DateTime GetPreviousWeekday(DateTime date)
    {
        do
        {
            date = date.AddDays(-1);
        } while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);

        return date;
    }
}