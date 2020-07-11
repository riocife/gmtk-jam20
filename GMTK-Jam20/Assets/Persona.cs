﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persona : MonoBehaviour
{
    // Initial force to add to this persona.
    public float initialImpulse = 10f;

    // Player can only collect this persona back after this time passes.
    public float uncollectableTime = 3f;

    [System.Serializable]
    public struct SkillColor
    {
        public PlayerSkills skill;
        public Color color;
    }

    // Each Skill can be associated with it's own color.
    public List<SkillColor> colors = new List<SkillColor>();

    [Header("Setup")]
    public Transform playerTriggerDetection;

    PlayerSkills skill;
    public PlayerSkills Skill
    {
        get { return skill; }
        set
        {
            skill = value;
            sr.color = colors.Find(s => s.skill == value).color;
        }
    }

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        StartCoroutine(InitialState());

        // Adds impulse at random angle (in radians)
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rb.AddForce(dir * initialImpulse, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.transform.GetComponent<PlayerHealth>();
        if (player != null && playerTriggerDetection.gameObject.activeInHierarchy)
        {
            Debug.Log("Hello");
            Destroy(gameObject);
        }
    }

    IEnumerator InitialState()
    {
        playerTriggerDetection.gameObject.SetActive(false);

        yield return new WaitForSeconds(3.0f);

        playerTriggerDetection.gameObject.SetActive(true);
    }
}
