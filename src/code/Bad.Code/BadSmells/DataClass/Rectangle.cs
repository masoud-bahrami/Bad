namespace Bad.Code.BadSmells.DataClass;

public class Rectangler
{
    public int CalculateArea(Rectangle rectangle)
    {
        var rectangleWeight = rectangle.Weight;
        var rectangleHeight = rectangle.Height;
        
        return rectangleHeight * rectangleWeight;
    }
}
public class Rectangle
{
    public Rectangle(int height, int weight)
    {
        Height = height;
        Weight = weight;
    }

    public int Height { get;private set; }
    public int Weight { get;private set; }
}