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

    public TextMeshProUGUI scoreText;
    private int score;

    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    private float spawnRate = 1.5f;

    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        soundGameOver = GetComponent<AudioSource>();
        
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void PlaySound ()
    {

    }
}
