public class Reservation
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string PhoneNumber{ get; set; }
    private string StartReservation { get; set; }
    private string EndReservation { get; set; }
    public string Comment{ get; set; }
    private string TableID;
    public string GetStartReservation() { return StartReservation; }
    public string GetEndReservation() { return EndReservation; }

    public Reservation(List<Table> tables, string tableid, string id, string startreservation, string endreservation, string name = "Undefined", string phonenumber = "Undefined", string comment = "")
    {
        ID = id;
        Name = name;
        PhoneNumber = phonenumber;
        StartReservation = startreservation;
        EndReservation = endreservation;
        TableID = tableid;
        Table table = FindTable(ref tables, tableid);
        SetTimeSlots(ref table, startreservation, endreservation);
        Comment = comment;
    }

    public static Reservation DefaultReservation()
    {
        return new Reservation(new List<Table>(), "Undefined", "Undefined", "Undefined", "Undefined");
    }

    private bool CheckTimeSlots(Table table, string startreservation, string endreservation, string id)
    {
        int startreservationTime = Convert.ToInt32(startreservation.Split(":")[0]);
        int endreservationTime = Convert.ToInt32(endreservation.Split(":")[0]);

        foreach (var timeSlot in table.Time.Keys.ToList())
        {
            string[] t = timeSlot.Split("-");
            int startTime = Convert.ToInt32(t[0].Split(":")[0]);
            int endTime = Convert.ToInt32(t[0].Split(":")[2]);

            if (startTime <= startreservationTime && endTime >= endreservationTime)
            {
                if (table.Time[timeSlot] == null || table.Time[timeSlot].ID == id) { continue; }
                else { return false; }
            }
        }

        return true;
    }

    private void SetTimeSlots(ref Table table, string startreservation, string endreservation)
    {
        int startreservationTime = Convert.ToInt32(startreservation.Split(":")[0]);
        int endreservationTime = Convert.ToInt32(endreservation.Split(":")[0]);

        foreach (var timeSlot in table.Time.Keys.ToList())
        {
            string[] t = timeSlot.Split("-");
            int startTime = Convert.ToInt32(t[0].Split(":")[0]);
            int endTime = Convert.ToInt32(t[0].Split(":")[2]);

            if (startTime <= startreservationTime && endTime >= endreservationTime)
            {
                table.Time[timeSlot] = this;
            }
        }
    }

    private void DeleteReservation(ref Table table, string startreservation, string endreservation)
    {
        int startreservationTime = Convert.ToInt32(startreservation.Split(":")[0]);
        int endreservationTime = Convert.ToInt32(endreservation.Split(":")[0]);

        foreach (var timeSlot in table.Time.Keys.ToList())
        {
            string[] t = timeSlot.Split("-");
            int startTime = Convert.ToInt32(t[0].Split(":")[0]);
            int endTime = Convert.ToInt32(t[0].Split(":")[2]);

            if (startTime <= startreservationTime && endTime >= endreservationTime)
            {
                table.Time[timeSlot] = null;
            }
        }
    }

    private Table FindTable(ref List<Table> tables, string tableid)
    {
        for (int i = 0; i < tables.Count; i++)
        {
            Table table = tables[i];
            if (table.ID == tableid)
            {
                return table;
            }
        }

        return null;
    }
    void EditReservation(ref List<Table> tables, string startreservation, string endreservation, string tableid, string id = "Undefined", string name = "Undefined", string phonenumber = "Undefined", string comment = "")
    {

        if (id != "undefined") ID = id;
        if (name != "undefined") Name = name;
        if (phonenumber != "undefined") PhoneNumber = phonenumber;

        Table ourTable = FindTable(ref tables, this.ID);
        Table newTable = FindTable(ref tables, tableid);

        if (CheckTimeSlots(newTable, startreservation, endreservation, tableid))
        {
            DeleteReservation(ref ourTable, this.StartReservation, this.EndReservation);
            StartReservation = startreservation;
            EndReservation = endreservation;
            TableID = tableid;
            SetTimeSlots(ref newTable, startreservation, endreservation);
        }

        if (comment != "undefined") Comment = comment;
    }
}