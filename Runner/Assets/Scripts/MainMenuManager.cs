using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void BtnStartOnClick()
    {
        SceneManager.LoadScene(1);
    }

    public void BtnExitOnClick()
    {
        Application.Quit();
    }
}
