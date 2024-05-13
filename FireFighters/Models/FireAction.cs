namespace FireFighters.Models;

public class FireAction
{
    public int IdFireAction  { get; set; }
    public DateTime StartTime  { get; set; }
    public  DateTime EndTime  { get; set; }
    public  bool NeedSpecialEquipm  { get; set; }
}