namespace Bad.Code.BadSmells.DataClass;

public class RecruitmentType
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class InsuranceConfiguration
{
    public  string Id { get; set; }
    public int MinimumRate { get; set; }
    public string Formula { get; set; }
}