using static GraphicsEngine.Primitives.Utils.FloatUtils;


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


        public Point2D Evaluate(float t)
        {
            return GetEvaluator()(t);
        }

        protected virtual Func<float, Point2D> GetEvaluator()
        {
            return (float t) => P0 + Clamp01(t) * (P1 - P0); ;
        }
    }
}
