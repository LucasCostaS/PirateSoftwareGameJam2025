using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 initialPosition;
    private float quadrantSizeHorizontal = 54f;
    private float quadrantSizeVertical = 30f;


    // Update is called once per frame
    void Update()
    {
        int quadrantX, quadrantY;
        quadrantX = (int)MathF.Floor(MathF.Abs((player.transform.position.x + (quadrantSizeHorizontal / 2 - initialPosition.x)) / quadrantSizeHorizontal));
        quadrantY = (int)MathF.Floor(MathF.Abs((player.transform.position.y + (quadrantSizeVertical/2 - initialPosition.y)) / quadrantSizeVertical));
        gameObject.transform.position = new Vector3(initialPosition.x + (quadrantSizeHorizontal * quadrantX), initialPosition.y + (quadrantSizeVertical * quadrantY), gameObject.transform.position.z);
        
    }

    // Start is called before the first frame updates
    void Start()
    {
        initialPosition = gameObject.transform.position;    
    }
}
