using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject settingsPrefab;
    private SettingsController settingsController;

    [SerializeField] private Button playButton;
    [SerializeField] private Button openSettingsButton;
    [SerializeField] private Button closeSettingsButton;
    [SerializeField] private Button openInstructionsButton;
    [SerializeField] private Button closeInstructionsButton;

    [SerializeField] private Scrollbar matchTimeScrollbar;
    [SerializeField] private Scrollbar spawnTimeScrollbar;
    [SerializeField] private TMP_Dropdown playerColorDropdown;

    [SerializeField] private TMP_Text matchTimeDisplay;
    [SerializeField] private TMP_Text spawnTimeDisplay;

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject instructionsPanel;

    private void Awake()
    {
        if (!FindObjectOfType<SettingsController>())
        {
            Instantiate(settingsPrefab);
        }
        settingsController = FindObjectOfType<SettingsController>();
    }

    private void Start()
    {
        playButton.onClick.AddListener(PlayButton);

        openSettingsButton.onClick.AddListener(() => { SettingsPanelActive(true); });
        closeSettingsButton.onClick.AddListener(() => { SettingsPanelActive(false); });

        openInstructionsButton.onClick.AddListener(() => { InstructionsPanelActive(true); });
        closeInstructionsButton.onClick.AddListener(() => { InstructionsPanelActive(false); });

        matchTimeScrollbar.numberOfSteps = (180 - 60)/15 + 1;
        matchTimeScrollbar.size = 1 / matchTimeScrollbar.numberOfSteps;
        matchTimeScrollbar.onValueChanged.AddListener(MatchTimeSet);

        spawnTimeScrollbar.numberOfSteps = 10 - 4 + 1;
        spawnTimeScrollbar.size = 1 / spawnTimeScrollbar.numberOfSteps;
        spawnTimeScrollbar.onValueChanged.AddListener(SpawnTimeSet);

        playerColorDropdown.onValueChanged.AddListener(PlayerColorSet);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SettingsPanelActive(bool state)
    {
        settingsPanel.SetActive(state);
        MatchTimeSet(matchTimeScrollbar.value);

        if (!state) return;

        matchTimeScrollbar.value = (float)(settingsController.MatchTime - 60) / (float)(180 - 60);
        MatchTimeSet(matchTimeScrollbar.value);

        spawnTimeScrollbar.value = (float)(settingsController.EnemySpawnTime - 6) / (float)(10 - 6);
        SpawnTimeSet(spawnTimeScrollbar.value);

        playerColorDropdown.value = (int)(settingsController.PlayerColor) - 2;
    }

    private void InstructionsPanelActive(bool state)
    {
        instructionsPanel.SetActive(state);
    }

    private void MatchTimeSet(float value)
    {
        int matchTime = (int)(value * (matchTimeScrollbar.numberOfSteps-1)) * 15 + 60;
        settingsController.ChangeMatchTime(matchTime);
        matchTimeDisplay.text = matchTime.ToString();
    }

    private void SpawnTimeSet(float value)
    {
        int spawnTime = (int)(value * (spawnTimeScrollbar.numberOfSteps - 1)) + 6;
        settingsController.ChangeEnemySpawnTime(spawnTime);
        spawnTimeDisplay.text = spawnTime.ToString();
    }

    private void PlayerColorSet(int value)
    {
        settingsController.ChangePlayerColor(value + 2);
    }
}