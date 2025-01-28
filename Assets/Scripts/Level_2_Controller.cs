using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2_Controller : MonoBehaviour
{
    public GameObject player;
    public LayerMask ice;
    public LayerMask ground;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.IsTouchingLayers(ice))
            player.GetComponent<PlayerController>().controlEnabled = false;
        if (rb.IsTouchingLayers(ground))
            player.GetComponent<PlayerController>().controlEnabled = true;

    }
}
