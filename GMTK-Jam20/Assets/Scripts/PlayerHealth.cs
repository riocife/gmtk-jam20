using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    enum PlayerSkills {  Dash, Weapon };

    List<PlayerSkills> activeSkills = new List<PlayerSkills>();

    Dash dash;
    Transform weapon;

    bool invincible = false;
    public float invincibilityTime = 1f;

    void Awake()
    {
        dash = GetComponent<Dash>();
        weapon = transform.GetChild(0);

        activeSkills.Add(PlayerSkills.Dash);
        activeSkills.Add(PlayerSkills.Weapon);
    }

    public void OnHit()
    {
        if (invincible) return;

        // If there are no active skills, game over.
        if (activeSkills.Count <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            int randomSkillIndex = Random.Range(0, activeSkills.Count);
            PlayerSkills skill = activeSkills[randomSkillIndex];
            activeSkills.RemoveAt(randomSkillIndex);

            LoseSkill(skill);

            StartCoroutine(InvincibilityTimer());
        }
    }

    void LoseSkill(PlayerSkills skill)
    {
        switch (skill)
        {
            case PlayerSkills.Dash:
                dash.enabled = false;
                break;
            case PlayerSkills.Weapon:
                weapon.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    IEnumerator InvincibilityTimer()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

}
