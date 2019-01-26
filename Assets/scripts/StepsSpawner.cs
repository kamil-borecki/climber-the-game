﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StepsSpawner : MonoBehaviour
{
    public State state;

    public int stepsCount = 10;
    public float stepDistance = 0.07f;
    public float stepDistanceRandomFactor = 0.05f;
    public float routeWidth = 0.3f;
    public float routeWidthRandomFactor = 0.1f;

    private List<GameObject> steps = new List<GameObject>();
    private GameObject finishLine;
    private float currentHeight = 0;
    private string currentLevel;

    // TODO get rid of this
    private bool stepsMoved = false;

    void Start()
    {
        currentHeight = gameObject.GetComponent<MeshFilter>().mesh.bounds.min[1];
        currentLevel = state.currentLevel.ToString();

        CreateSteps();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO ten if nie powinien sprawdzać co klatkę
        if (!stepsMoved)
        {
            UpdateStepsPositions();
        }
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
            i++;
            if (steps[i])
            {
                steps[i].transform.localPosition = new Vector2(routeWidth + Random.Range(0f, routeWidthRandomFactor), currentHeight + Random.Range(0f, stepDistanceRandomFactor));
            }
        }

        finishLine.transform.localPosition = new Vector2(-routeWidth, currentHeight);

        stepsMoved = !stepsMoved;
    }

    private GameObject CreateStep()
    {
        string url = "lvl" + currentLevel + "/prefabs/" + Random.Range(1, 4).ToString();
        var tempObj = Instantiate(Resources.Load(url, typeof(GameObject)) as GameObject);
        tempObj.transform.SetParent(gameObject.transform);

        return tempObj;
    }

    private void CreateFinalStepColider()
    {
        finishLine = Instantiate(Resources.Load("common/finisher", typeof(GameObject)) as GameObject);
        finishLine.transform.SetParent(gameObject.transform);
    }
}
