using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Code Ref: Unity Project -- Ruby's Adventure
    public float speed;
    public bool vertical;
    // Here we give it a random movement
    int changeTime = Random.Range(1, 4);
    private Rigidbody rb;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        Vector3 position = rb.position;
        
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;;
        }
        rb.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //This line of code should be replaced by the correct fileName
        //Example ==> // RubyController player = other.gameObject.GetComponent<RubyController >();
        // Not Working ==> //ThirdPersonUserControl player = other.gameObject.GetComponent<ThirdPersonUserControl>();

        if (player != null)
        {
            // This is a property inside playerController (will be added soon)
            // player.ChangeHealth(-1);
            player.ChangeHealth(-1);
        }
    }
}
