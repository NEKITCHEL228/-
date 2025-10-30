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

    void EditReservation(string id = "Undefined", string name = "Undefined", string phonenumber = "Undefined", string startreservation = "Undefined", string endreservation = "Undefined", string tableid = "undefined", string comment = "")
    {
        if (id != "undefined") ID = id;
        if (name != "undefined") Name = name;
        if (phonenumber != "undefined") PhoneNumber = phonenumber;
        if (startreservation != "undefined") StartReservation = startreservation;
        if (endreservation != "undefined") EndReservation = endreservation;
        if (tableid != "undefined") TableID = tableid;
        if (comment != "undefined") Comment = comment;
    }

    void ReturnReservation(ref List<Table> tables)
    {
        for (int i = 0; i < tables.Capacity; i++)
        {
            Table table = tables[i];
            if (table.ID == this.TableID)
            {
                foreach (var timeSlot in table.Time.Keys.ToList())
                {
                    string[] t = timeSlot.Split("-");
                    int startTime = Convert.ToInt32(t[0].Split(":")[0]);
                    int endTime = Convert.ToInt32(t[0].Split(":")[2]);

                }
            }
        }
    }



}