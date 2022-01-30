using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject pauseMenu;

    private PlayerControls playerControls;
    public bool isPaused { get; private set; }

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Pause.performed += PauseGame;
    }

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

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("art test");
        Time.timeScale = 1;
    }

    public void EndGame()
    {
        print("load");
        SceneManager.LoadScene("GameEnd");
        Time.timeScale = 1;
    }

    public void CloseApplication()
    {

    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        if(pauseMenu)
        {

            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            print("there is no pause menu!");
        }
    }

    public void UnPauseGame()
    {
        if (pauseMenu)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            print("there is no pause menu!");
        }
    }
}
