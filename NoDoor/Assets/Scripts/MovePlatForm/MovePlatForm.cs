using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePlatForm : MonoBehaviour
{
    public Vector2 direction;
    public float moveTime;
    bool enbleMove = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && enabled)
        {
            transform.position = Vector2.MoveTowards(transform.position, direction, moveTime);
            enabled = false; 
        }
    }
}
