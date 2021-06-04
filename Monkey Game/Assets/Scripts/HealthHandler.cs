using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ThirdPersonUserControl.ChangeHealth;

public class HealthHandler : MonoBehaviour
{
    public Image[] hp;      //Loading the hearts as an array called hp.
    public Sprite Health_5;

    void Update()
    {
        // The basic logic is when the user got hurt, we disabled one heart from the image array from the right to the left.
        // You can also cut the code and move it to ThirdPersonUserControl.cs
        for (int i=0; i < hp.Length; i++){
            if (ChangeHealth(-1)){
                hp[-1].enabled = false;
            }
        }
    }
}
