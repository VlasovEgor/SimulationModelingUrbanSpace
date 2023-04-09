
public class PedesterianEdge
{
    public PedestrianVertex From { get; set; }

    public PedestrianVertex To { get; set; }

    public float Weight { get; set; }

    public PedesterianEdge(PedestrianVertex from, PedestrianVertex to, float weight = 1)
    {
        From = from;
        To = to;
        Weight = weight;
    }
}
