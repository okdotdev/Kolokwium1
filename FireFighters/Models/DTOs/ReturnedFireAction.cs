namespace FireFighters.Models.DTOs;

public class ReturnedFireAction
{
    public int IdFireAction  { get; set; }
    public DateTime StartTime  { get; set; }
    public DateTime EndTime  { get; set; }
    public bool NeedSpecialEquipm { get; set; }
    public IEnumerable<Firefighter> Firefighters { get; set; }

}