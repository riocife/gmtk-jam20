using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Persona : MonoBehaviour
{
    // Player can only collect this persona back after this time passes.
    public float uncollectableTime = 3f;

    [HideInInspector] public PlayerSkills skill;

    SpriteRenderer sr;
    Collider2D col;
    Animator anim;

    AIPath path;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        col.enabled = false;

        path = GetComponent<AIPath>();
    }

    void Start()
    {
        StartCoroutine(ActivateCollider());
    }

    void Update()
    {
        anim.SetBool("ReachedDestination", path.reachedDestination);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.transform.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.RegainSkill(skill);
            Destroy(gameObject);
        }
    }

    IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(uncollectableTime);

        col.enabled = true;
    }
}
