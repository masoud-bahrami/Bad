using System;
using System.Collections.Generic;
using System.Linq;

namespace Bad.Code.BadSmells._17MiddleMan;


public class ManagerManager
{
    public bool IsManagerFree(string managerId , Duration duration)
    {
        Manager manager = FetchManager(managerId);
        return manager.IsFree(duration);
    }

    private Manager FetchManager(string managerId)
    {
        throw new NotImplementedException();
    }
}

public class Secretary: Human
{
    public List<Meeting> Meeting { get; set; }

    public bool IsFree(Manager manager, Duration duration)
    {
        //TODO implementation
        return true;
    }
}

public class Meeting
{
    public Human Human { get; set; }
    public Duration Duration { get; set; }
}


public class Manager: Human
{
    public Secretary Secretary { get; set; }
    
    public bool IsFree(Duration duration)
    {
        return Secretary.IsFree(this, duration);
    }
}

public class Human{public string Id { get; }}


public class Duration
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}