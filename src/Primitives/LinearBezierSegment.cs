namespace GraphicsEngine.Primitives
{
    public class LinearBezierSegment
    {
        public Point2D P0 { get; set; }
        public Point2D P1 { get; set; }

        public LinearBezierSegment(Point2D p0, Point2D p1)
        {
            P0 = p0;
            P1 = p1;
        }


        public virtual Point2D Evaluate(float t)
        {
            // Clamping t between 0 and 1
            t = MathF.Min(MathF.Max(t, 0f), 1f);
            
            return P0 + t * (P1 - P0);
        }
    }
}
