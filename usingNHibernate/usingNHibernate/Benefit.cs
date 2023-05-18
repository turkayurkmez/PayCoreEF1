namespace usingNHibernate;


public abstract class EntityBase
{
    public virtual int Id { get; set; }
}
public class Benefit : EntityBase
{
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual Employee Employee { get; set; }
}

public class Leave : Benefit
{

    public virtual LeaveType Type { get; set; }
    public virtual int Available { get; set; }
    public virtual int Remaining { get; }

}

public enum LeaveType
{
    Casual, Sick, UnPaid
}

public class FoodTicket : Benefit
{

    public virtual DateTime StartDate { get; set; }
    public virtual DateTime EndDate { get; set; }

    public virtual decimal Amount { get; set; }
}