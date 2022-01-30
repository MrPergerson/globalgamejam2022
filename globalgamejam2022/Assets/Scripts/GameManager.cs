using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    [SerializeField] private float sceneTransitionTime = 1;
    [SerializeField] private Animator sceneTransition;

    private PlayerControls playerControls;
    public bool isPaused { get; private set; }

    [Header("Generators")]
    [SerializeField] private int generatorsActive = 0;
    [SerializeField] private int generatorsNeededToBeActive = 0;

    

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Pause.performed += PauseGame;
        if(sceneTransition) 
            sceneTransition.gameObject.SetActive(true);
        else
        {
            Debug.LogWarning("Crossfade animator is not assigned to gamemanager");
        }
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
        StartCoroutine(LoadLevel("Level 01"));
    }

    public void RestartGame()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(currentScene));
    }

    public void EndGame()
    {
        StartCoroutine(LoadLevel("EndGame"));
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
        StartCoroutine(LoadLevel("MainMenu"));

    }

    public void LoadScene(string scene)
    {

        StartCoroutine(LoadLevel(scene));
        
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

    IEnumerator LoadLevel(int levelIndex)
    {
        sceneTransition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(sceneTransitionTime);

        Time.timeScale = 1;
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadLevel(string scene)
    {
        sceneTransition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(sceneTransitionTime);

        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }
}
