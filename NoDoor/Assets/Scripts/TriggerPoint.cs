using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    public GameObject tagGameObj;
    public GameObject getGameObj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagGameObj.tag)
        {
            getGameObj.GetComponent<MovePlatForm>().onTrigger = true;
        }
    }
}
