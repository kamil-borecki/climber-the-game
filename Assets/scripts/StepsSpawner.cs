using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StepsSpawner : MonoBehaviour
{
    public State state;

    private int stepsCount;
    private float stepDistance;
    private float stepDistanceRandomFactor;
    private float routeWidth;
    private float routeWidthRandomFactor;
    public List<GameObject> stepsVariants;

    private GameObject finishLine;

    private float currentHeight = 0;
    private List<GameObject> steps = new List<GameObject>();

    // TODO get rid of this
    private bool stepsMoved = false;

    void Awake()
    {
      SetupLevel(state.levels[state.currentLevel]);
      CreateSteps();
    }

    void Update()
    {
        // TODO dont check every frame
        if (!stepsMoved)
        {
            UpdateStepsPositions();
        }
    }

    private void SetupLevel(Level level)
    {
        stepsCount = level.stepsCount;
        stepDistance = level.stepDistance;
        stepDistanceRandomFactor = level.stepDistanceRandomFactor;
        routeWidth = level.routeWidth;
        routeWidthRandomFactor = level.routeWidthRandomFactor;
    }

    private void CreateSteps()
    {
        for(int i = 0; i < stepsCount; i++)
        {
            steps.Add(CreateStep());
        }
        CreateFinalStepColider();
    }

    private void UpdateStepsPositions()
    {
        for (int i = 0; i<steps.Count; i++)
        {
            currentHeight += stepDistance;

            steps[i].transform.localPosition = new Vector2(-routeWidth + Random.Range(0f, routeWidthRandomFactor), currentHeight + Random.Range(0f, stepDistanceRandomFactor));
            if (i+1 < steps.Count && steps[i+1])
            {
                i++;
                steps[i].transform.localPosition = new Vector2(routeWidth + Random.Range(0f, routeWidthRandomFactor), currentHeight + Random.Range(0f, stepDistanceRandomFactor));
            }
        }

        finishLine.transform.localPosition = new Vector2(-routeWidth, currentHeight);

        stepsMoved = !stepsMoved;
    }

    private GameObject CreateStep()
    {
        var tempObj = Instantiate(stepsVariants[Random.Range(0, stepsVariants.Count)]);
        tempObj.transform.SetParent(gameObject.transform);
        return tempObj;
    }

    private void CreateFinalStepColider()
    {
        finishLine = Instantiate(Resources.Load("common/finisher", typeof(GameObject)) as GameObject);
        finishLine.transform.SetParent(gameObject.transform);
    }
}
