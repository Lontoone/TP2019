using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{

    public void OnLoginButtonClick()
    {

        SceneManager.LoadScene("Level 1");
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("finish");
            Application.Quit();
        }
    }
}
