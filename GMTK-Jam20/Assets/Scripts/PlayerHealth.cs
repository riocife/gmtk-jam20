using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSkills { Dash, Weapon };

[System.Serializable]
public struct PersonaPrefab
{
    string name;
    PlayerSkills skill;
    GameObject prefab;
}

public class PlayerHealth : MonoBehaviour
{
    public GameObject personaPrefab;

    public PersonaPrefab[] personaPrefabs;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnHit();
        }
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
                dash.StopDash();
                dash.enabled = false;
                break;
            case PlayerSkills.Weapon:
                weapon.gameObject.SetActive(false);
                break;
            default:
                break;
        }

        InstantiatePersona(skill);
    }

    IEnumerator InvincibilityTimer()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

    void InstantiatePersona(PlayerSkills skill)
    {
        Persona persona = Instantiate(personaPrefab, transform.position, Quaternion.identity).GetComponent<Persona>();
        if (persona != null)
        {
            persona.Skill = skill;
            persona.GetComponent<AIDestinationSetter>().target = GetComponent<PlayerMovement>().playerStart;
        }
    }

    public void RegainSkill(PlayerSkills skill)
    {
        if (activeSkills.Contains(skill)) return;
        activeSkills.Add(skill);

        switch (skill)
        {
            case PlayerSkills.Dash:
                dash.enabled = true;
                break;
            case PlayerSkills.Weapon:
                weapon.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
