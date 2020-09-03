using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoChangeScene : MonoBehaviour
{
    public string sceneName;
    public float time;
    // Start is called before the first frame update
    public bool autoStart = true;
    void Start()
    {
        if (autoStart)
            Invoke("ChangeScene", time);
    }

    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
