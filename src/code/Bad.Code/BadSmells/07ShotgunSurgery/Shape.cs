using System;

namespace Bad.Code.BadSmells._07ShotgunSurgery
{
    public abstract class Shape
    {
        // what will happen, if we decide to add the z axes?
        // all classes should be changes. One modification, multiple changes in many places
        public abstract void Draw(int x, int y);
    }
    public class Rectangle: Shape
    {
        public override void Draw(int x, int y)
        {
            // TODO
        }
    }

    public class Square : Shape
    {
        public override void Draw(int x, int y)
        {
            // TODO
        }
    }

    public class Triangle : Shape
    {
        public override void Draw(int x, int y)
        {
            // TODO
        }
    }
    
}