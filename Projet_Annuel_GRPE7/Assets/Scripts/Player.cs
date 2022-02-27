using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int points;
    private int position;
    public int tour = 0;
    private int placementStart;
    public bool finished = false;
    public Camera curentCamera;
    private int currentCheckpoint;

    public void setPlacement()
    {
        placementStart = position;
    }
    
    public int FinishPassed()
    {
        tour++;
        currentCheckpoint = 0;
        return tour;
    }
    
    public bool CheckpointPassed(int checkpointNb)
    {
        if (checkpointNb == currentCheckpoint+1)
        {
            currentCheckpoint++;
            return true;
        }
        return false;
    }
}
