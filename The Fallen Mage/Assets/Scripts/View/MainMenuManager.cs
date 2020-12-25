using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void NewGameBtn()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void ExitBtn()
    {
        Application.Quit();
    }
}
