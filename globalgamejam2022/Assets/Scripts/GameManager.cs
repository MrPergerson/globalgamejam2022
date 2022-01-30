using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Start()
    {
        if(Instance != null && Instance == this)
        {
            Destroy(this);
        }else
        {
            Instance = this;
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene("art test");
    }

    public void EndGame()
    {
        print("load");
        SceneManager.LoadScene("GameEnd");
    }

    public void CloseApplication()
    {

    }

    public void GoToMainMenu()
    {

    }
}
