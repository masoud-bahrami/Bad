namespace Bad.Code.BadSmells._17MiddleMan;

public class Order
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
    
    public int GetId()
    {
        return Id;
    }

    public OrderStatus GetStatus()
    {
        return Status;
    }
}

public enum OrderStatus
{
    InProgress
}