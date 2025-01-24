using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{

    public GameObject Player;
    public Vector2 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Collider2D>().IsTouching(Player.GetComponent<Collider2D>())){
            Player.transform.position = spawnPoint;
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }    
    }
}
