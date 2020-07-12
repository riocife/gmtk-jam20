using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerSkills { Dash, Weapon };

[System.Serializable]
public struct PersonaPrefab
{
    public string name;
    public PlayerSkills skill;
    public GameObject prefab;
}


public class PlayerHealth : MonoBehaviour
{
    public static Action<PlayerSkills> onPlayerLoseSkill;
    public static Action<PlayerSkills> onPlayerGainSkill;
    public static Action onPlayerDied;

    Animator animator;
    public List<PersonaPrefab> personaPrefabs = new List<PersonaPrefab>();

    public AnimatorOverrideController animationsOverride;

    List<PlayerSkills> activeSkills = new List<PlayerSkills>();

    Dash dash;
    Transform weapon;

    bool invincible = false;
    public float invincibilityTime = 1f;

    void Awake()
    {
        dash = GetComponent<Dash>();
        weapon = transform.GetChild(0);
        animator = GetComponentInChildren<Animator>();

        foreach (PersonaPrefab persona in personaPrefabs)
        {
            activeSkills.Add(persona.skill);
        }
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
        
        animator.SetTrigger("Hitted");

        // If there are no active skills, game over.
        if (activeSkills.Count <= 0)
        {
            Die();
        }
        else
        {
            int randomSkillIndex = UnityEngine.Random.Range(0, activeSkills.Count);
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

        onPlayerLoseSkill.Invoke(skill);
    }

    IEnumerator InvincibilityTimer()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

    void InstantiatePersona(PlayerSkills skill)
    {
        GameObject personaPrefab = personaPrefabs.Find(prefab => prefab.skill == skill).prefab;
        Persona persona = Instantiate(personaPrefab, transform.position, Quaternion.identity).GetComponent<Persona>();
        if (persona != null)
        {
            persona.skill = skill;
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

        onPlayerGainSkill.Invoke(skill);
    }

    void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Shooting>().enabled = false;
        GetComponent<Dash>().enabled = false;

        onPlayerDied.Invoke();

        animator.SetTrigger("Died");

        StartCoroutine(AfterDeath());
    }

    IEnumerator AfterDeath()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
