using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("���������");
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("gameOver", true);
        }
    }
}
