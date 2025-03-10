using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void OnClickPlay()
    {
        //SceneManager.LoadScene(1);
        LoadingScreenManager.Instance.SwitchToScene(1);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
