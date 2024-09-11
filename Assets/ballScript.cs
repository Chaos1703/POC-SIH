using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    public Task2 task2;
    private bool dumped = false;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "SM_Sandbox_01 (1)" && !dumped)
        {
            dumped = true;
            task2.updateCount();
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}
