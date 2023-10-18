using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicScoreCalculator : IScoreCalculator
{
    public int CalculateScore(float distance)
    {
        return (int)distance;
    }
}
