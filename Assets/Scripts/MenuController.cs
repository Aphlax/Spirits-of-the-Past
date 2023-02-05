using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string startSceneId;

    public void OnStartButtonClick() {
        SceneManager.LoadScene(startSceneId, LoadSceneMode.Single);
    }

    public void OnExitButtonClick() {
        Application.Quit();
    }
}
