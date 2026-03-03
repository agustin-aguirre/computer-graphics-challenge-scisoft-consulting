using static GraphicsEngine.Primitives.Utils.FloatUtils;


namespace GraphicsEngine.Primitives
{
    public class QuadraticBezierSegment : LinearBezierSegment
    {
        public Point2D P2 { get; set; }

        public QuadraticBezierSegment(Point2D p0, Point2D p1, Point2D p2) : base(p0, p1)
        {
            P2 = p2;
        }


        protected override Func<float, Point2D> GetEvaluator()
        {
            return (float t) =>
            {
                t = Clamp01(t);
                return Square(1f-t)*P0 + 2f*(1f-t)*t*P1 + Square(t)*P2;
            };
        }
    }
}
