using System.Globalization;

public class Reservation
{
    private string ID = "Undefined";
    private string Name = "Undefined";
    private string PhoneNumber = "Undefined";
    private string StartReservation = "Undefined";
    private string EndReservation = "Undefined";
    private string Comment = "";
    private string TableID = "Undefined";
    public string GetStartReservation() { return StartReservation; }
    public string GetEndReservation() { return EndReservation; }

    public Reservation(string tableid, string id, string name = "Undefined", string phonenumber = "Undefined", string startreservation = "Undefined", string endreservation = "Undefined", string comment = "")
    {
        ID = id;
        Name = name;
        PhoneNumber = phonenumber;
        StartReservation = startreservation;
        EndReservation = endreservation;
        TableID = tableid;
        Comment = comment;
    }

    public static Reservation DefaultReservation()
    {

        return new Reservation("Undefined", "Undefined");
    }

    private bool CheckTimeSlots(Table table, string startreservation, string endreservation, string id)
    {
        int startreservationTime = Convert.ToInt32(this.StartReservation.Split(":")[0]);
        int endreservationTime = Convert.ToInt32(this.EndReservation.Split(":")[0]);

        if (table.ID == this.TableID)
        {
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
        }


        return true;
    }

    private void DeleteReservation(ref Table table)
    {
        int startreservationTime = Convert.ToInt32(this.StartReservation.Split(":")[0]);
        int endreservationTime = Convert.ToInt32(this.EndReservation.Split(":")[0]);

        if (table.ID == this.TableID)
        {
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

    }

    void EditReservation(ref List<Table> tables, string startreservation, string endreservation, string tableid, string id = "Undefined", string name = "Undefined", string phonenumber = "Undefined", string comment = "")
    {

        if (id != "undefined") ID = id;
        if (name != "undefined") Name = name;
        if (phonenumber != "undefined") PhoneNumber = phonenumber;

        for (int i = 0; i < tables.Capacity; i++)
        {
            Table t = tables[i];
            if (t.ID == tableid)
            {
                if (CheckTimeSlots(t, startreservation, endreservation, tableid))
                {
                    StartReservation = startreservation;
                    EndReservation = endreservation;
                    TableID = tableid;
                    DeleteReservation(ref t);
                    // + еще поменять в столе расписание
                }
            }
        }

        if (comment != "undefined") Comment = comment;
    }





}