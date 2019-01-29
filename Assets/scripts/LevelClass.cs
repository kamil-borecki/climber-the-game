using UnityEngine;
using System.Collections;

public class Level
{
    public string sceneName;
    public int stepsCount;
    public int stepsVariations;
    public float stepDistance;
    public float stepDistanceRandomFactor;
    public float routeWidth;
    public float routeWidthRandomFactor;

    public Level(LevelSettings levelSettings)
    {
        sceneName = levelSettings.sceneName;
        stepsCount = levelSettings.stepsCount;
        stepDistance = levelSettings.spaceBeetwenSteps;
        stepDistanceRandomFactor = levelSettings.stepsDistanceFactor;
        routeWidth = levelSettings.routeWidth;
        routeWidthRandomFactor = levelSettings.routeWidthFactor;
    }
}
