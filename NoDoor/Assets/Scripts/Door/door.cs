using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    public void LevelSuccessful()
    {
        GameManager.Instance.GameOver();
    }
}
