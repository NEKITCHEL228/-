public class Table
{
    public string ID { get; set; }
    public string Location { get; set; }
    public int SpotCount { get; set; }
    public Dictionary<string, Reservation> Time;


    void EditTable(Dictionary<string, Reservation> NewTime, string NewLocation, int NewSpotCount, string NewID)
    {
        ID = NewID;
        Location = NewLocation;
        SpotCount = NewSpotCount;
        Time = NewTime;
    }

    public Table(string id, string location, int spotCount, Dictionary<string, Reservation> time = null)
    {
        ID = id;
        Location = location;
        SpotCount = spotCount;
        Time = time ?? GetDefaultTimeSlots();
    }

    private static Dictionary<string, Reservation> GetDefaultTimeSlots()
    {
        return new Dictionary<string, Reservation>
        {
            {"09:00-10:00", Reservation.DefaultReservation()},
            {"10:00-11:00", Reservation.DefaultReservation()},
            {"11:00-12:00", Reservation.DefaultReservation()},
            {"12:00-13:00", Reservation.DefaultReservation()},
            {"13:00-14:00", Reservation.DefaultReservation()},
            {"14:00-15:00", Reservation.DefaultReservation()},
            {"15:00-16:00", Reservation.DefaultReservation()},
            {"16:00-17:00", Reservation.DefaultReservation()},
            {"17:00-18:00", Reservation.DefaultReservation()}
        };
    }

    public void PrintTable()
    {
        Console.WriteLine($"ID: {new string('-', 70)}{ID}.");
        Console.WriteLine($"Расположение: {new string('-', 55)}«{Location}».");
        Console.WriteLine($"Количество мест: {new string('-', 50)}{SpotCount}");
        Console.WriteLine("Расписание:");

        foreach (var timeSlot in Time)
        {
            string time = timeSlot.Key;
            Reservation reservation = timeSlot.Value;

            string reservationInfo = "";

            if (reservation != null && reservation.GetStartReservation() != "Undefined")
            {
                reservationInfo = $"ID {reservation.ID}, {reservation.Name}, {reservation.PhoneNumber}";
            }

            int dashCount = 50 - time.Length;
            if (dashCount < 1) dashCount = 1;

            string output = $"{time} {new string('-', dashCount)}{reservationInfo}";
            Console.WriteLine(output);
        }
    }
}

