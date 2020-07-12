using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillHUD : MonoBehaviour
{
    public PlayerSkills skill;
    public Sprite inactiveSprite;
    Sprite activeSprite;

    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        activeSprite = image.sprite;

        PlayerHealth.onPlayerLoseSkill += OnPlayerLoseSkill;
        PlayerHealth.onPlayerGainSkill += OnPlayerGainSkill;
        PlayerHealth.onPlayerDied += OnPlayerDied;
    }

    void OnPlayerLoseSkill(PlayerSkills skillLost)
    {
        if (skill != skillLost) return;
        image.sprite = inactiveSprite;
    }

    void OnPlayerGainSkill(PlayerSkills skillGained)
    {
        if (skill != skillGained) return;
        image.sprite = activeSprite;
    }

    void OnPlayerDied()
    {
        PlayerHealth.onPlayerLoseSkill -= OnPlayerLoseSkill;
        PlayerHealth.onPlayerGainSkill -= OnPlayerGainSkill;
        PlayerHealth.onPlayerDied -= OnPlayerDied;
    }
}
