class Table
{
    public string ID = "Undefined";
    public string Location = "Undefined";
    public int SpotCount;
    public Dictionary<string, Reservation> Time;


    void EditTable(string NewID, string NewLocation, int NewSpotCount, Dictionary<string, Reservation> NewTime)
    {
        ID = NewID;
        Location = NewLocation;
        SpotCount = NewSpotCount;
        Time = NewTime;
    }

    public Table(Dictionary<string, Reservation> time = null, string id = "Undefined", string location = "Undefined", int spotCount = 0)
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
        Console.WriteLine($"ID: --------------------------------------------------------------------{ID}.");
        Console.WriteLine($"Расположение:-------------------------------------------------------«{Location}».");
        Console.WriteLine($"Количество мест: -----------------------------------------------------------{SpotCount}");
        Console.WriteLine("Расписание:");

        foreach (var timeSlot in Time)
        {
            string time = timeSlot.Key;
            bool isBooked = timeSlot.Value.GetStartReservation() == time;

            string baseLine = $"          {time} -------------------------------------------------";

            if (baseLine.Length < 60)
            {
                baseLine = baseLine.PadRight(60, '-');
            }
            else
            {
                baseLine = baseLine.Substring(0, 60);
            }

            string status = isBooked ? "Занято" : "Свободно";
            Console.WriteLine($"{baseLine} {status}");
        }
    }
}

