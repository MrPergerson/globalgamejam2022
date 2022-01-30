using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    private PlayerControls playerControls;
    public bool isPaused { get; private set; }

    [SerializeField] private int generatorsActive = 0;
    [SerializeField] private int generatorsNeededToBeActive = 0;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Pause.performed += PauseGame;
    }

    private void Start()
    {
        FindAllGeneratorsInScene();

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
        SceneManager.LoadScene("Level 01");
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1;
    }

    public void EndGame()
    {
        print("load");
        SceneManager.LoadScene("GameEnd");
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        if (gameOverMenu)
        {

            isPaused = true;
            gameOverMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            print("Game over: there is no game over menu!");
        }
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
            print("Paused game: there is no pause menu!");
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

    public void FindAllGeneratorsInScene()
    {
        
        var generators = FindObjectsOfType<Generator>();
        generatorsNeededToBeActive = generators.Length;
        foreach(var generator in generators)
        {
            generator.OnGeneratorRepaired += IncrementGeneratorsActiveCount;
        }
        
    }

    public void IncrementGeneratorsActiveCount()
    {
        generatorsActive++;

        if(generatorsActive >= generatorsNeededToBeActive)
        {
            // will get next scene in the build list...assuming there is one
            var currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene + 1);
        }
    }
}
