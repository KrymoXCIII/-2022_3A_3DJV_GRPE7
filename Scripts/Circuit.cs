using System;
using System.Collections;
using System.Collections.Generic;
using Scenes.Scripts;
using UnityEditor.SearchService;
using UnityEngine;

public enum gameState
{
    qualification = 0,
    placementObjet = 1,
    course = 2
};

public class Circuit : MonoBehaviour
{
    private gameState gs;
    private int numberFinished;
    public int numberPlayer;
    public Camera topDowView;
    
    public Checkpoint[] checkpoints;

    private GameLoop gl;
    
    public Transform spawn1;
    public Transform spawn2;

    public Player p1;
    public Player p2;
    
    private void OnEnable()
    {
        gs = gameState.qualification;
        Display.displays[0].Activate();
        p1.transform.position = spawn1.position;
        p2.transform.position = spawn2.position;
        p1.transform.rotation = spawn1.rotation;
        p2.transform.rotation = spawn2.rotation;
        p1.GetComponent<Rigidbody>().velocity = new Vector3();
        p2.GetComponent<Rigidbody>().velocity = new Vector3();
        p1.curentCamera.enabled = true;
        p1.curentCamera.targetDisplay = 0;    
        p2.curentCamera.enabled = true;
        p2.curentCamera.targetDisplay = 0;
        topDowView.enabled = false;
        topDowView.targetDisplay = 1;
    }

    private void Update()
    {
        if (gs == gameState.placementObjet)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                courseStart();
            }
        }
    }

    public void courseStart()
    {
        gs = gameState.course;
        p1.transform.position = spawn1.position;
        p2.transform.position = spawn2.position;
        p1.curentCamera.enabled = true;
        p1.curentCamera.targetDisplay = 0;    
        p2.curentCamera.enabled = true;
        p2.curentCamera.targetDisplay = 0;
        topDowView.enabled = false;
        topDowView.targetDisplay = 1;
    }

    public void placementObjetStart()
    {
        Debug.Log("objet la");
        gs = gameState.placementObjet;
        topDowView.enabled = true;
        topDowView.targetDisplay = 0;
        p1.curentCamera.enabled = false;
        p1.curentCamera.targetDisplay = 1;    
        p2.curentCamera.enabled = false;
        p2.curentCamera.targetDisplay = 1;
        p1.tour = 0;
        p2.tour = 0;
    }
    
    public void checkpointPassed(Player p, int checkpointN)
    {
        int tour = 0;
        var newCheckpoint = p.CheckpointPassed(checkpointN);
        if (checkpointN == checkpoints.Length-1 && newCheckpoint)
        {
           tour = p.FinishPassed();
        }

        if (tour == 0)
            return;
        if(gs == gameState.qualification){
            if (tour == 1)
            {
                numberFinished++;
                p.setPlacement();
                if (numberFinished == numberPlayer)
                {
                    placementObjetStart();
                    numberFinished = 0;
                }
            }
        } else if (gs == gameState.course)
        {
            if (tour == 1)
            {
                numberFinished++;
                if (numberFinished == numberPlayer)
                {
                   gl.courseFinished();
                }
            }
        }
    }
    
    public void setGl(GameLoop gameLoop)
    {
        gl = gameLoop;
    }
}
