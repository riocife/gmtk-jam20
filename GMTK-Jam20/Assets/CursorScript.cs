using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public Vector2 crosshairOfset = new Vector2();

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 movePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movePosition += crosshairOfset;
        this.transform.position = movePosition;
    }
}
