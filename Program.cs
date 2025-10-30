class Program
{
    static void Main()
    {
        ReservationSystem system = new ReservationSystem();
        bool isProgramActive = true;
        while (isProgramActive)
        {
            Console.WriteLine("Выберите команду");
            Console.WriteLine("1.Создать n столов (n > 0)");
            Console.WriteLine("2.Создать n бронирований (n > 0)");
            Console.WriteLine("3.Редактировать стол по его ID (стол не участвует в бронировании)");
            Console.WriteLine("4.Вывести информацию о столе по его ID");
            Console.WriteLine("5.Вывести перечень всех доступных для брони столов соответствующих фильтру");
            Console.WriteLine("6.Вывести перечень всех бронирований");
            Console.WriteLine("7.Вывести информацию о бронировании по последним 4 цифрам номера телефона и имени клиента");
            Console.WriteLine("8.Завершение работы программы");

            string? x = Convert.ToString(Console.ReadLine());
            switch (x)
            {
                case "1":
                    break;
                
                case "2":
                    break;

                case "3":
                    break;

                case "4":
                    break;

                case "5":
                    break;

                case "6":
                    system.PrintAllReservations();
                    break;

                case "7":
                    break;

                case "8":
                    isProgramActive = false;
                    break;

                default:
                    break;
            };
        }


        Reservation reservation = new Reservation("1", "1", "кто-то", "+79998885342", "13:00", "15:00");

        Dictionary<string, Reservation> timeSlots = new Dictionary<string, Reservation>
        {
            {"09:00-10:00", Reservation.DefaultReservation()},
            {"10:00-11:00", Reservation.DefaultReservation()},
            {"11:00-12:00", Reservation.DefaultReservation()},
            {"12:00-13:00", Reservation.DefaultReservation()},
            {"13:00-14:00", reservation},
            {"14:00-15:00", reservation},
            {"15:00-16:00", reservation},
            {"16:00-17:00", Reservation.DefaultReservation()},
            {"17:00-18:00", Reservation.DefaultReservation()}
        };
        Table myTable = new Table(timeSlots, "12345", "у окна", 3);
        myTable.PrintTable();
    }
}