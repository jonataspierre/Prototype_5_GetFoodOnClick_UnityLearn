using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private AudioSource soundGameOver;

    public GameObject titleScreen;
    public GameObject cameraSound;

    public GameObject pauseScreen;
    private bool paused;

    public TextMeshProUGUI scoreText;
    private int score;

    public TextMeshProUGUI livesText;
    private int live;

    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    private float spawnRate = 1.0f;

    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        soundGameOver = GetComponent<AudioSource>();

        UpdateLiveScore(3);
    }

    void ChangePause()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePause();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int indexTarget = Random.Range(0, targets.Count);

            Instantiate(targets[indexTarget]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        
        scoreText.text = "Score \n" + score;
    }

    public void UpdateLiveScore(int scoreLive)
    {
        live += scoreLive;

        livesText.text = "Lives \n" + live;

        if (live == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        soundGameOver.Play();
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {        
        SceneManager.LoadScene(0);
    }

    public void StartGame (int difficulty)
    {
        isGameActive = true;
        score = 0;

        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());

        titleScreen.gameObject.SetActive(false);
    }
}
