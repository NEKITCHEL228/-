public class ReservationSystem
{
    private List<Reservation> reservations = new List<Reservation>();
    private List<Table> tables = new List<Table>();
    
    public void AddReservation(Reservation reservation)
    {
        reservations.Add(reservation);
    }
    
    public bool RemoveReservation(string reservationId)
    {
        var reservation = reservations.Find(r => r.GetID() == reservationId);
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
    
    public void CreateReservations(int n)
    {
        for (int i = 0; i < n; i++)
        {
            
        }
    }
}