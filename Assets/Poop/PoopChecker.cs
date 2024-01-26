using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopChecker : MonoBehaviour
{
    private AiMover mover;
    private void Start()
    {
        mover = GetComponent<AiMover>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("poop"))
        {
            mover.friction *= 50;
            StartCoroutine(RemoveTimer(collision.gameObject));
        }
    }

    IEnumerator RemoveTimer(GameObject g)
    {
        yield return new WaitForSeconds(1.0f);
        mover.friction /= 50;
        Destroy(g);
    }
}
