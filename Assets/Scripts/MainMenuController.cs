using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            if (gameObject.transform.GetChild(2).gameObject.activeSelf)
            {
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
                gameObject.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if (gameObject.transform.GetChild(3).gameObject.activeSelf)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void ButtonClick(){
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }
}
