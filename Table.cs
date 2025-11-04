#pragma warning disable CS8625

public class Table
{
    public int ID { get; set; }
    public string Location { get; set; }
    public int SpotCount { get; set; }
    public Dictionary<string, Reservation> Time;

    public Table(int id, string location, int spotCount, Dictionary<string, Reservation> time = null)
    {
        ID = id;
        Location = location;
        SpotCount = spotCount;
        Time = time ?? GetDefaultTimeSlots();
    }

    public void EditTable(Dictionary<string, Reservation> NewTime, string NewLocation, int NewSpotCount)
    {
        Location = NewLocation;
        SpotCount = NewSpotCount;
        Time = NewTime;
    }

    public Dictionary<string, Reservation> GetDefaultTimeSlots()
    {
        return new Dictionary<string, Reservation>
        {
            {"09:00-10:00", null},
            {"10:00-11:00", null},
            {"11:00-12:00", null},
            {"12:00-13:00", null},
            {"13:00-14:00", null},
            {"14:00-15:00", null},
            {"15:00-16:00", null},
            {"16:00-17:00", null},
            {"17:00-18:00", null}
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

            if (reservation != null && reservation.GetStartReservation() != "")
            {
                reservationInfo = $"ID {reservation.ID}, {reservation.Name}, {reservation.PhoneNumber}";
            }

            int dashCount = 50 - time.Length;
            if (dashCount < 1) dashCount = 1;

            string output = $"{time} {new string('-', dashCount)}{reservationInfo}";
            Console.WriteLine(output);
        }
    }

    public bool IsTimeFree(string startTime, string endTime, int ignoreReservation = -1)
    {
        int s = Convert.ToInt32(startTime.Split(":")[0]);
        int e = Convert.ToInt32(endTime.Split(":")[0]);

        foreach (var kv in Time)
        {
            string timeSlot = kv.Key;
            Reservation existing = kv.Value;

            string[] t = timeSlot.Split("-");
            int start = Convert.ToInt32(t[0].Split(":")[0]);
            int end = Convert.ToInt32(t[1].Split(":")[0]);

            if (s <= start && e >= end)
            {
                if (existing != null && existing.ID != ignoreReservation) { return false; }
            }
        }

        return true;
    }

    public void ApplyReservation(Reservation reservation)
    {
        if (reservation == null) { return; }

        int s = Convert.ToInt32(reservation.GetStartReservation().Split(":")[0]);
        int e = Convert.ToInt32(reservation.GetEndReservation().Split(":")[0]);

        foreach (var timeSlot in Time.Keys.ToList())
        {
            string[] t = timeSlot.Split("-");
            int start = Convert.ToInt32(t[0].Split(":")[0]);
            int end = Convert.ToInt32(t[1].Split(":")[0]);

            if (s <= start && e >= end)
            {
                Time[timeSlot] = reservation;
            }
        }
    }

    public void RemoveReservation(int reservationId)
    {
        foreach (var key in Time.Keys.ToList())
        {
            var r = Time[key];
            if (r != null && r.ID == reservationId)
                Time[key] = null;
        }
    }

}

