using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleBackground : MonoBehaviour
{
    private float y = 0F;
    private float x = 0F;
    // Update is called once per frame
    public void Start()
    {
        y = 0F;
        x = 0F;
    }
    void Update()
    {
        y -= +Input.GetAxis("Mouse Y");
        x -= +Input.GetAxis("Mouse X");
        if (x > 700)
            x = 700;
        if (x < -700)
            x = -700;
        if (y > 700)
            y = 700;
        if (y < -700)
            y = -700;
        transform.Rotate(new Vector3(+Input.GetAxis("Mouse X")/10, +Input.GetAxis("Mouse Y")/10), 0.01f);
        transform.position = new Vector3(x, y, 565);
    }
}
