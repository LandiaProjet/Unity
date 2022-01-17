using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem {

    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private static readonly int[] experiencePerLevel = new[] { 100, 120, 140, 160, 180, 200, 220, 250, 300, 400, 500 };

    public LevelSystem() {}

    public void AddExperience(int amount) {
        if (!IsMaxLevel()) {
            PlayerData.getData().experience += amount;
            while (!IsMaxLevel() && PlayerData.getData().experience >= GetExperienceToNextLevel(PlayerData.getData().level)) {
                PlayerData.getData().experience -= GetExperienceToNextLevel(PlayerData.getData().level);
                PlayerData.getData().level++;
                if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
            }
            if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
        }
    }

    public int GetLevelNumber() {
        return PlayerData.getData().level;
    }

    public float GetExperienceNormalized() {
        if (IsMaxLevel()) {
            return 1f;
        } else {
            return (float)PlayerData.getData().experience / GetExperienceToNextLevel(PlayerData.getData().level);
        }
    }

    public int GetExperience() {
        return PlayerData.getData().experience;
    }

    public int GetExperienceToNextLevel(int level) {
        if (level < experiencePerLevel.Length) {
            return experiencePerLevel[level];
        } else {
            Debug.LogError("Level invalid: " + level);
            return 100;
        }
    }

    public bool IsMaxLevel() {
        return IsMaxLevel(PlayerData.getData().level);
    }

    public bool IsMaxLevel(int level) {
        return level == experiencePerLevel.Length - 1;
    }

}
