using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryZoneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        int levelNumber = SceneManager.GetActiveScene().buildIndex;
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            SceneManager.LoadScene(levelNumber + 1);
    }
}
