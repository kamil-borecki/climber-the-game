using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;

[System.Serializable]
public class LevelSettings
{
    public string sceneName;
    public int stepsCount;
    public float spaceBeetwenSteps, stepsDistanceFactor, routeWidth, routeWidthFactor;

}

[CreateAssetMenu()]
public class State : ScriptableObject
{
    public List<Level> levels = new List<Level>();
    public int currentLevel = 0;

    public List<LevelSettings> levelSettings;
   

    private void OnEnable()
    {

        foreach(LevelSettings levelSetting in levelSettings)
        {
            levels.Add(new Level(levelSetting));
        }
       
    }
}
