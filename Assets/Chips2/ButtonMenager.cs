using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenager1 : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);

    }
    public void TeleportClick()
    {
        PlayerController.Player_Singltone.death = true;

    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
