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
        if (reservation != null)
        {
            reservations.Remove(reservation);
            return true;
        }
        return false;
    }

    public void PrintAllReservations()
    {
        Console.WriteLine("=== ВСЕ БРОНИРОВАНИЯ ===");
        foreach (var res in reservations)
        {
            res.DisplayInfo();
        }
    }

    public void PrintAllTablesID()
    {
        foreach (Table t in tables)
        {
            Console.Write(t.ID + " ");
        }
        Console.WriteLine();
        
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
            Console.Write("Введите телефонный номер: ");
            PhoneNumber = Console.ReadLine();
            Console.Write("Введите время начала брони (формат XX:XX): ");
            StartReservation = Console.ReadLine();
            Console.Write("Введите время окончания брони (формат XX:XX): ");
            EndReservation = Console.ReadLine();
            Console.Write("Введите комментарий (необязательно): ");
            comment = Console.ReadLine();
            Console.Write("Введите ID стола для брони: ");
            TableID = Convert.ToInt32(Console.ReadLine());
            Reservation reservation = new Reservation(tables, ID, TableID, Name, PhoneNumber, StartReservation, EndReservation, comment);
            reservations.Add(reservation);
            ID++;
        }
    }

    public void Createtables(int n)
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
        }


    }
}