using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutEater : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Donut"))
        {
            EatDonut(collision.gameObject);
        }

    }

    private void EatDonut(GameObject g)
    {
        GetComponent<Controller>().AddDonut();
        Destroy(g);
    }
}
