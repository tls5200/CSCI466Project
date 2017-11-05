﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public static float edgeBufferSize = 10;
    public static float zoomSpeed = 0.02f;

    public Camera gameCamera;
    public Rect defaultScreenSize = new Rect(Vector2.zero, new Vector2(40, 30));
    private float preferedSize;

    // Use this for initialization
    void Start()
    {
        gameCamera = GetComponent<Camera>();
        preferedSize = gameCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float aspectRatio = gameCamera.aspect;

        Vector3 position = transform.position;
        Rect gameBounds;
        float xUpperLimit = position.x;
        float xLowerLimit = position.x;
        float yUpperLimit = position.y;
        float yLowerLimit = position.y;
        float size;

        if (Level.currentLevel != null)
        {
            gameBounds = Level.currentLevel.gameBounds;

            foreach (Player item in Level.currentLevel.players)
            {
                if (item != null && item.active)
                {
                    if (item.position.x > xUpperLimit)
                        xUpperLimit = item.position.x;
                    if (item.position.x < xLowerLimit)
                        xLowerLimit = item.position.x;
                    if (item.position.y > yUpperLimit)
                        yUpperLimit = item.position.y;
                    if (item.position.y < yLowerLimit)
                        yLowerLimit = item.position.y;
                }
            }

            foreach (PlayerControls item in Controls.get().players)
            {
                if (item.zoomOut)
                    preferedSize *= 1 + zoomSpeed;
                if (item.zoomIn)
                    preferedSize /= 1 + zoomSpeed;
            }
        }
        else
        {
            gameBounds = defaultScreenSize;
        }

        if (2f * preferedSize > gameBounds.height)
            preferedSize = gameBounds.height / 2f;
        else if (aspectRatio * 2f * preferedSize > gameBounds.width)
            preferedSize = gameBounds.width / (aspectRatio * 2f);
        else if (preferedSize < edgeBufferSize)
            preferedSize = edgeBufferSize;

        size = preferedSize;

        xUpperLimit += edgeBufferSize;
        xLowerLimit -= edgeBufferSize;
        yUpperLimit += edgeBufferSize;
        yLowerLimit -= edgeBufferSize;

        if (xUpperLimit > gameBounds.xMax)
            xUpperLimit = gameBounds.xMax;
        if (xLowerLimit < gameBounds.xMin)
            xLowerLimit = gameBounds.xMin;
        if (yUpperLimit > gameBounds.yMax)
            yUpperLimit = gameBounds.yMax;
        if (yLowerLimit < gameBounds.yMin)
            yLowerLimit = gameBounds.yMin;

        position.x = (xUpperLimit - xLowerLimit) / 2.0f + xLowerLimit;
        position.y = (yUpperLimit - yLowerLimit) / 2.0f + yLowerLimit;


        if (xUpperLimit - xLowerLimit > size * 2f * aspectRatio)
            size = (xUpperLimit - xLowerLimit) / (2f * aspectRatio);
        if (yUpperLimit - yLowerLimit > size * 2f)
            size = (yUpperLimit - yLowerLimit) / 2f;

        if (position.x + size * aspectRatio > gameBounds.xMax)
            position.x -= position.x + size * aspectRatio - gameBounds.xMax;
        if (position.x - size * aspectRatio < gameBounds.xMin)
            position.x += gameBounds.xMin - position.x + size * aspectRatio;
        if (position.y + size > gameBounds.yMax)
            position.y -= position.y + size - gameBounds.yMax;
        if (position.y - size < gameBounds.yMin)
            position.y += gameBounds.yMin - position.y + size;


        if (Level.currentLevel != null)
            Level.currentLevel.backgroundPosition = position;

        gameCamera.orthographicSize = size;
        transform.position = position;
        
    }
}
