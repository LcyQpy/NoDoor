using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePlatForm : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool isLoop = true;
    [SerializeField] private bool isTriggerMove = false;
    public bool onTrigger = false;

    private int pointNum = 1;
    private void Update()
    {
        UpdatePoint();
        Move(isTriggerMove, onTrigger);
    }
    private void Move(bool isTriggerMove, bool trigger)
    {
        if (isTriggerMove)
        {
            if (trigger) {
                transform.position = Vector2.MoveTowards(transform.position, points[pointNum].transform.position, Time.deltaTime * speed);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, points[pointNum].transform.position, Time.deltaTime * speed);
        }
        
    }

    private void UpdatePoint()
    {
        if (Vector2.Distance(transform.position, points[pointNum].transform.position) < 0.1f && isLoop)
        {
            pointNum = 1 - pointNum;
        }
    }
}

