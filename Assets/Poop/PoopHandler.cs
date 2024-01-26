using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopHandler : MonoBehaviour
{
    [SerializeField] GameObject PoopPrefab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (true)//check if has enugh cake
            {
                Instantiate(PoopPrefab, transform.position, Quaternion.identity);
                GetComponent<Controller>().ResetFat();
            }
        }
    }
}
