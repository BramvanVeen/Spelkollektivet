using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;

    //Reference to the current Line renderer
    LineRenderer currentLineRenderer;

    //Vector2 containing the last position of the brush
    Vector2 lastPos;

    private void Update()
    {
        Draw();
    }
    
    //Draw function with three fases
    void Draw()
    {
        //For when the player first clicks
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        { 
            CreateBrush();
        }
        //For when the player is holding the mousebutton
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Check if mouseposition has last changed since last calling
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos != lastPos)
            {
                AddAPoint(mousePos);
                lastPos = mousePos;
            }
        }
        //For when the player releases the mouse  
        else
        {
            currentLineRenderer = null;
        }

        //A function to create a new instance of the brush for when a player first clicks
        void CreateBrush()
        {
            GameObject brushInstance = Instantiate(brush);
            currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

            //Set startpoints of the brush to where the player first clicks
            currentLineRenderer.SetPosition(0, mousePos);
            currentLineRenderer.SetPosition(1, mousePos);
        }

        //Increase the amount of points by one
        void AddAPoint(Vector2 pointPos)
        {
            currentLineRenderer.positionCount++;
            int positionIndex = currentLineRenderer.positionCount-1;
            currentLineRenderer.SetPosition(positionIndex, pointPos);
        }
    }  
}
