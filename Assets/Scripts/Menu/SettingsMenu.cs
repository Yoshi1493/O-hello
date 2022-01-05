﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : Menu
{
    [SerializeField] UserSettings userSettings;

    [Space]

    [SerializeField] Toggle soundToggle;
    [SerializeField] Toggle hintsToggle;
    [SerializeField] Toggle animationsToggle;

    [Space]

    [SerializeField] ToggleGroup difficultySettings;
    Toggle[] difficultyToggles;

    [Space]

    [SerializeField] ToggleGroup backgroundImages;
    Toggle[] backgroundToggles;
    public event System.Action BackgroundChangeAction;

    [SerializeField] ToggleGroup boardColours;
    Toggle[] colourToggles;

    protected override void Awake()
    {
        base.Awake();
        InitOptions();
    }

    //init. options based on UserSettings values
    void InitOptions()
    {
        hintsToggle.isOn = userSettings.hintsOn;
        soundToggle.isOn = userSettings.soundOn;

        difficultyToggles = difficultySettings.GetComponentsInChildren<Toggle>();
        backgroundToggles = backgroundImages.GetComponentsInChildren<Toggle>();
        colourToggles = boardColours.GetComponentsInChildren<Toggle>();

        for (int i = 0; i < difficultyToggles.Length; i++)
        {
            difficultyToggles[i].SetIsOnWithoutNotify(i == (int)userSettings.cpuDifficulty);
        }

        for (int i = 0; i < backgroundToggles.Length; i++)
        {
            backgroundToggles[i].SetIsOnWithoutNotify(i == (int)userSettings.backgroundImage);
        }

        for (int i = 0; i < colourToggles.Length; i++)
        {
            colourToggles[i].SetIsOnWithoutNotify(i == (int)userSettings.boardColour);
        }
    }

    public void OnToggleHints()
    {
        userSettings.hintsOn = hintsToggle.isOn;
    }

    public void OnToggleSound()
    {
        userSettings.soundOn = soundToggle.isOn;
    }

    public void OnToggleAnimations()
    {
        userSettings.animationsOn = animationsToggle.isOn;
    }

    public void OnChangeCPUDifficulty(int value)
    {
        userSettings.cpuDifficulty = (UserSettings.CPUDifficulty)value;
    }

    public void OnChangeBackgroundImage(int value)
    {
        if (backgroundToggles[value].isOn)
        {
            userSettings.backgroundImage = (UserSettings.BackgroundImage)value;
            BackgroundChangeAction?.Invoke();
        }
    }

    public void OnChangeBoardColour(int value)
    {
        if (colourToggles[value].isOn)
        {
            userSettings.boardColour = (UserSettings.BoardColour)value;
        }
    }

    public void SaveSettings()
    {
        userSettings.Save();
    }
}