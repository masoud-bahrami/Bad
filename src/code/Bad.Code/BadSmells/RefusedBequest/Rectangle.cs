namespace Bad.Code.BadSmells.RefusedBequest
{
    public class Rectangle
    {        
        public int Height { get; private set; }
        public int Weight { get; private set; }

        public Rectangle(int height , int weight)
        {
            Height = height;
            Weight = weight;
        }

        public virtual void SetHeight(int newHeight)
        {
            this.Height = newHeight;
        }
        public virtual void SetWeight(int newWeight)
        {
            this.Weight= newWeight;
        }
    }

    public class Square:Rectangle
    {
        public Square(int height, int weight) : base(height , weight)
        {
        }

        public override void SetHeight(int newHeight)
        {
            base.SetHeight(newHeight);
            base.SetHeight(newHeight);
        }

        public override void SetWeight(int newWeight)
        {
            base.SetHeight(newWeight);
            base.SetHeight(newWeight);
        }
    }
}
