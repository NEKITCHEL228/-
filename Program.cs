#pragma warning disable CS8604
#pragma warning disable CS8600

using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;

        ReservationSystem system = new ReservationSystem();
        bool isProgramActive = true;
        int n;
        string Start, End, Name, PhoneNumberLast_4;
        while (isProgramActive)
        {
            Console.Clear();
            Console.WriteLine("Выберите команду");
            Console.WriteLine("1.Создать n столов (n > 0)");
            Console.WriteLine("2.Создать n бронирований (n > 0)");
            Console.WriteLine("3.Редактировать стол по его ID (стол не участвует в бронировании)");
            Console.WriteLine("4.Вывести информацию о столе по его ID");
            Console.WriteLine("5.Вывести перечень всех доступных для брони столов по времени бронирования");
            Console.WriteLine("6.Вывести перечень всех бронирований");
            Console.WriteLine("7.Вывести информацию о бронированиях по последним 4 цифрам номера телефона и имени клиента");
            Console.WriteLine("8.Удаление бронирования");
            Console.WriteLine("9.Завершение работы программы");
            Console.Write("Команда: ");

            string? x = Convert.ToString(Console.ReadLine());
            switch (x)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Введите кол-во столов: ");
                    n = Convert.ToInt32(Console.ReadLine());
                    system.CreateTables(n);
                    break;

                case "2":
                    Console.Clear();
                    Console.Write("Введите кол-во бронирований: ");
                    n = Convert.ToInt32(Console.ReadLine());
                    system.CreateReservations(n);
                    break;

                case "3":
                    Console.Clear();
                    system.PrintAllTablesID();
                    Console.Write("Введите ID стола: ");
                    n = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    system.EditTable(n);
                    break;

                case "4":
                    Console.Clear();
                    system.PrintAllTablesID();
                    Console.Write("Введите ID стола: ");
                    n = Convert.ToInt32(Console.ReadLine());
                    system.PrintTable(n);
                    Console.WriteLine($"Информация о столе с ID {n}");
                    Console.ReadLine();
                    break;

                case "5":
                    Console.Clear();
                    Console.Write("Введите время начала бронирования: ");
                    Start = Console.ReadLine();
                    Console.Write("Введите время конца бронирования: ");
                    End = Console.ReadLine();
                    system.PrintTablesEnableToReservate(Start, End);
                    break;

                case "6":
                    Console.Clear();
                    system.PrintAllReservations();
                    break;

                case "7":
                    Console.Clear();
                    Console.Write("Введите имя клиента: ");
                    Name = Console.ReadLine();
                    Console.Write("Введите последние 4 цифры телефона клиента (формат XX-XX): ");
                    PhoneNumberLast_4 = Console.ReadLine();
                    system.PrintInfoAboutClientReservations(Name, PhoneNumberLast_4);
                    break;

                case "8":
                    Console.Clear();
                    Console.Write("Введите ID брони: ");
                    n = Convert.ToInt32(Console.ReadLine());
                    system.RemoveReservation(n);
                    break;

                case "9":
                    isProgramActive = false;
                    break;

                default:
                    Console.Clear();
                    break;
            }
            ;
        }
    }
}