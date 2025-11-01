using Microsoft.VisualBasic;

public class Reservation
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    private string StartReservation { get; set; }
    private string EndReservation { get; set; }
    public string Comment { get; set; }
    private int TableID;
    public string GetStartReservation() { return StartReservation; }
    public string GetEndReservation() { return EndReservation; }

    public Reservation(Table table, int id, string name, string phonenumber, string startreservation, string endreservation, string comment = "")
    {
        ID = id;
        Name = name;
        PhoneNumber = phonenumber;
        StartReservation = startreservation;
        EndReservation = endreservation;
        TableID = table.ID;
        Comment = comment;
    }

    private void DeleteReservation(ref Table table)
    {
        table.RemoveReservation(ID);
    }

    void EditReservation(ref List<Table> tables, string startreservation, string endreservation, int tableid, string name = "", string phonenumber = "", string comment = "")
    {
        if (name != "") Name = name;
        if (phonenumber != "") PhoneNumber = phonenumber;

        Table ourTable = tables.Find(t => t.ID == this.TableID);
        Table newTable = tables.Find(t => t.ID == tableid);

        if (newTable.IsTimeFree(StartReservation, EndReservation, tableid))
        {
            DeleteReservation(ref ourTable);
            StartReservation = startreservation;
            EndReservation = endreservation;
            TableID = tableid;
            newTable.ApplyReservation(this);
        }

        if (comment != "") Comment = comment;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"ID клиента: {new string('-', 70)}{ID}.");
        Console.WriteLine($"Имя клиента: {new string('-', 55)}«{Name}».");
        Console.WriteLine($"Телефонный номер: {new string('-', 50)}{PhoneNumber}");
        Console.WriteLine($"Начало брони: {new string('-', 70)}{StartReservation}.");
        Console.WriteLine($"Конец брони: {new string('-', 55)}{EndReservation}.");
        Console.WriteLine($"Комментарий: {new string('-', 50)}{Comment}");
        Console.WriteLine($"ID стола: {new string('-', 55)}{TableID}");
    }
}