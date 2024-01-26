using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopHandler : MonoBehaviour
{
    [SerializeField] GameObject PoopPrefab;
    Controller controller;
    private void Start()
    {
        controller = GetComponent<Controller>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (controller.GetDonutsCount() > 2)
            {
                Instantiate(PoopPrefab, transform.position, Quaternion.identity);
                GetComponent<Controller>().ResetFat();
            }
        }
    }
}
