using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillHUD : MonoBehaviour
{
    public PlayerSkills skill;
    public Color inactiveColor;

    Color originalColor;

    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;

        PlayerHealth.onPlayerLoseSkill += OnPlayerLoseSkill;
        PlayerHealth.onPlayerGainSkill += OnPlayerGainSkill;
        PlayerHealth.onPlayerDied += OnPlayerDied;
    }

    void OnPlayerLoseSkill(PlayerSkills skillLost)
    {
        if (skill != skillLost) return;
        image.color = inactiveColor;
    }

    void OnPlayerGainSkill(PlayerSkills skillGained)
    {
        if (skill != skillGained) return;
        image.color = originalColor;
    }

    void OnPlayerDied()
    {
        PlayerHealth.onPlayerLoseSkill -= OnPlayerLoseSkill;
        PlayerHealth.onPlayerGainSkill -= OnPlayerGainSkill;
        PlayerHealth.onPlayerDied -= OnPlayerDied;
    }
}
