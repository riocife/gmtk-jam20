using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPoint : MonoBehaviour
{
    public float gizmoRadius = 1f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<PlayerMovement>())
        {
            Debug.Log("Player wins!");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
    }
}
