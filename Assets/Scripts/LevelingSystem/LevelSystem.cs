using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem {

    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;
    public LevelSystem() {}

    public void AddExperience(int amount) {
        PlayerData.getData().experience += amount;
        while (PlayerData.getData().experience >= GetExperienceToNextLevel(PlayerData.getData().level)) {
            PlayerData.getData().experience -= GetExperienceToNextLevel(PlayerData.getData().level);
            PlayerData.getData().level++;
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    public int GetLevelNumber() {
        return PlayerData.getData().level;
    }

    public float GetExperienceNormalized() {
        return (float)PlayerData.getData().experience / GetExperienceToNextLevel(PlayerData.getData().level);
    }

    public int GetExperience() {
        return PlayerData.getData().experience;
    }

    public int GetExperienceToNextLevel(int level) {
        return (level * 100);
    }

}
