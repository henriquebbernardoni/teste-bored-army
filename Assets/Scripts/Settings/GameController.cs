using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject settingsPrefab;
    private SettingsController settingsController;
    private EnemyController enemyController;

    [SerializeField] private TMP_Text timer;
    private int matchTime;
    private int timeLeft;

    [SerializeField] private TMP_Text scoreText;
    private int score;

    [SerializeField] private GameObject matchEndGameObject;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        if (!FindObjectOfType<SettingsController>())
        {
            Instantiate(settingsPrefab);
        }
        settingsController = FindObjectOfType<SettingsController>();
        enemyController = FindObjectOfType<EnemyController>();
    }

    private void Start()
    {
        matchTime = settingsController.MatchTime;
        timeLeft = matchTime;
        timer.text = "Tempo: " + timeLeft.ToString();
        scoreText.text = "Pontos: " + score.ToString();
        StartCoroutine(MatchProgression());

        playButton.onClick.AddListener(PlayButton);
        mainMenuButton.onClick.AddListener(MenuButton);
    }

    public void MatchEnd()
    {
        timer.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        matchEndGameObject.SetActive(true);

        enemyController.StopAllCoroutines();
        EnemyBase[] enemies = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].ShipDeath();
        }

        finalScoreText.text = score.ToString();
    }

    private IEnumerator MatchProgression()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;
            timer.text = "Tempo: " + timeLeft.ToString();
            if (timeLeft <= 0)
            {
                MatchEnd();
                yield break;
            }
        }
    }

    public void AddScore(int addedScore)
    {
        score += addedScore;
        if (score < 0)
        {
            score = 0;
        }
        scoreText.text = "Pontos: " + score.ToString();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}