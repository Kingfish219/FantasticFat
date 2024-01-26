using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopChecker : MonoBehaviour
{
    private AiMover mover;
    [SerializeField] SpriteRenderer sp;
    private void Start()
    {
        mover = GetComponent<AiMover>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("poop"))
        {
            mover.friction *= 50;
            Destroy(collision.gameObject);
            StartCoroutine(RemoveTimer());
            sp.color = Color.green;
        }
    }

    IEnumerator RemoveTimer()
    {
        yield return new WaitForSeconds(1.0f);
        mover.friction /= 50;
        sp.color = Color.white;
    }
}
