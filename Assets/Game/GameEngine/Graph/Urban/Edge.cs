
public class Edge 
{
    public UrbanVertex From { get; set; }

    public UrbanVertex To { get; set; }

    public float Weight { get; set; }

    public Edge(UrbanVertex from, UrbanVertex to, float weight = 1)
    {
        From = from;
        To = to;
        Weight = weight;
    }
}
