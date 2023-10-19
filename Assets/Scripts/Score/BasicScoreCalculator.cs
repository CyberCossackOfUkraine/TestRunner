public class BasicScoreCalculator : IScoreCalculator
{
    public int CalculateScore(float distance)
    {
        return (int)distance;
    }
}
