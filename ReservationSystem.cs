#pragma warning disable CS8604
#pragma warning disable CS8600

public class ReservationSystem
{
    private List<Reservation> reservations = new List<Reservation>();
    private List<Table> tables = new List<Table>();

    public void AddReservation(Reservation reservation)
    {
        reservations.Add(reservation);
    }

    public bool RemoveReservation(int reservationId)
    {
        var reservation = reservations.Find(r => r.ID == reservationId);
        if (reservation == null)
        {
            return false;
        }

        var table = tables.Find(t => t.ID == reservation.TableID);
        if (table != null)
        {
            reservation.DeleteReservation(ref table);
        }

        reservations.Remove(reservation);
        return true;
    }

    public void PrintAllTablesID()
    {
        Console.Write("ID столов: [ ");
        foreach (Table t in tables)
        {
            Console.Write(t.ID + "; ");
        }
        Console.Write("]");
        Console.WriteLine();

    }

    public void CreateTables(int n)
    {
        int ID = tables.Count;
        string Location;
        int SpotCount;
        for (int i = 0; i < n; i++)
        {
            Console.Clear();
            Console.Write("Введите локацию стола: ");
            Location = Console.ReadLine();
            Console.Write("Введите кол-во мест за столом: ");
            SpotCount = Convert.ToInt32(Console.ReadLine());
            Table t = new Table(ID, Location, SpotCount);
            tables.Add(t);
            ID++;

            Console.WriteLine("Стол успешно добавлен! Нажмите Enter.");
            Console.ReadLine();
        }
    }

    public void CreateReservations(int n)
    {
        int ID = reservations.Count;
        string Name, PhoneNumber, StartReservation, EndReservation, comment;
        int TableID;
        for (int i = 0; i < n; i++)
        {
            Console.Clear();

            Console.Write("Введите имя клиента: ");
            Name = Console.ReadLine();

            Console.Write("Введите телефонный номер (формат: +X (XXX) XXX-XX-XX): ");
            PhoneNumber = Console.ReadLine();

            Console.Write("Введите время начала брони (формат XX:XX): ");
            StartReservation = Console.ReadLine();

            Console.Write("Введите время окончания брони (формат XX:XX): ");
            EndReservation = Console.ReadLine();

            Console.Write("Введите комментарий (необязательно): ");
            comment = Console.ReadLine();

            PrintAllTablesID();
            Console.Write("Введите ID стола для брони: ");
            TableID = Convert.ToInt32(Console.ReadLine());

            Table table = tables.FirstOrDefault(t => t.ID == TableID);
            if (table == null)
            {
                Console.WriteLine("Ошибка: стол не найден. Нажмите Enter.");
                Console.ReadLine();
                i--; continue;
            }

            if (!table.IsTimeFree(StartReservation, EndReservation))
            {
                Console.WriteLine("Выбранное время уже занято или некорректно. Нажмите Enter.");
                Console.ReadLine();
                i--; continue;
            }

            Reservation reservation = new Reservation(table, ID, Name, PhoneNumber, StartReservation, EndReservation, comment);
            table.ApplyReservation(reservation);
            AddReservation(reservation);
            ID++;

            Console.WriteLine("Бронирование успешно добавлено! Нажмите Enter.");
            Console.ReadLine();
        }
    }

    public void EditTable(int TableID)
    {
        Console.Clear();
        var table = tables.Find(t => t.ID == TableID);
        if (table == null)
        {
            Console.WriteLine("Стол с таким ID не найден. Нажмите Enter.");
            Console.ReadLine();
            return;
        }

        foreach (Reservation r in table.Time.Values.ToList())
        {
            if (r != null)
            {
                Console.WriteLine("Стол уже участвует в активном бронировании. Нажмите Enter.");
                Console.ReadLine();
                return;
            }
        }

        Dictionary<string, Reservation> dict = table.GetDefaultTimeSlots();

        Console.Write("Введите новое кол-во мест (пустой ввод = нет изменений): ");
        string SpotsInput = Console.ReadLine();
        int NewSpotCount = string.IsNullOrWhiteSpace(SpotsInput) ? table.SpotCount : Convert.ToInt32(SpotsInput);

        Console.Write("Введите новую локацию (пустой ввод = нет изменений): ");
        string NewLocation = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(NewLocation)) NewLocation = table.Location;

        table.EditTable(dict, NewLocation, NewSpotCount);

        Console.WriteLine("Стол успешно отредактирован! Нажмите Enter.");
        Console.ReadLine();
    }

    public void PrintTable(int TableID)
    {
        var table = tables.Find(t => t.ID == TableID);
        if (table == null)
        {
            Console.WriteLine("Указанного стола не существует");
            return;
        }
        table.PrintTable();
    }

    public void PrintTable(Table table)
    {
        if (table == null)
        {
            Console.WriteLine("Указанного стола не существует");
            return;
        }
        table.PrintTable();
    }

    public void PrintTablesEnableToReservate(string StartReservation, string EndReservation)
    {
        Console.Clear();
        Console.WriteLine($"Столы которые можно забронировать на промежутке от {StartReservation} до {EndReservation}\n");

        foreach (Table table in tables)
        {
            if (table.IsTimeFree(StartReservation, EndReservation))
            {
                Console.WriteLine();
                PrintTable(table);
            }
        }

        Console.WriteLine("Выведены все подходящие столы! Нажмите Enter.");
        Console.ReadLine();
    }

    public void PrintAllReservations()
    {
        Console.Clear();
        Console.WriteLine("=== ВСЕ БРОНИРОВАНИЯ ===\n");
        foreach (var res in reservations)
        {
            res.DisplayInfo();
            Console.WriteLine();
        }

        Console.WriteLine("Выведены все бронирования! Нажмите Enter.");
        Console.ReadLine();
    }

    public void PrintInfoAboutClientReservations(string Name, string PhoneNumberLast_4)
    {
        Console.Clear();
        Console.WriteLine($"Все бронирования {Name} с номером заканчивающимся на {PhoneNumberLast_4}");
        foreach (Reservation reservation in reservations)
        {
            if (reservation.Name == Name)
            {
                string[] SplittedNumber = reservation.PhoneNumber.Split("-");
                string[] SplittedNumberLast_4 = PhoneNumberLast_4.Split("-");

                if (SplittedNumber[1] == SplittedNumberLast_4[0] && SplittedNumber[2] == SplittedNumberLast_4[1])
                {
                    Console.WriteLine();
                    reservation.DisplayInfo();
                }
            }
        }
        Console.WriteLine();
        Console.WriteLine("Вся информация о бронированиях клиента выведена! Нажмите Enter.");
        Console.ReadLine();
    }
}