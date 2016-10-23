using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private Button restartGameButton;
    [SerializeField]
    private Text scoreText, pauseText;

    private int score;
	// Use this for initialization
	void Start () {
        scoreText.text = score.ToString() + "M";
        StartCoroutine(CountScore());
    }

    IEnumerator CountScore()
    {
        yield return new WaitForSeconds(0.6f);
        score++;
        scoreText.text = score.ToString() + "M";
        StartCoroutine(CountScore());
    }

    void OnEnable()
    {
        PlayerDied.endGame += PlayerDiedEndTheGame;
    }

    void OnDisable()
    {
        PlayerDied.endGame -= PlayerDiedEndTheGame;
    }

    void PlayerDiedEndTheGame()
    {
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }else
        {
            int highscore = PlayerPrefs.GetInt("Score");
            if (score > highscore)
            {
                PlayerPrefs.SetInt("Score", score);
            }
        }
        pauseText.text = "Game Over";
        pausePanel.SetActive(true);
        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => RestartGame());
        Time.timeScale = 0f;

    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => ResumeGame());
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("MainMenu");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("Gameplay");
    }
}
