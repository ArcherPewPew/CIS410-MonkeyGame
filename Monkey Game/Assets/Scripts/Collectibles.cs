using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    void onTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("fruit"))
        {
            Debug.Log("Player has triggered the fruit");
            other.gameObject.SetActive(false);
        }
    }
}
