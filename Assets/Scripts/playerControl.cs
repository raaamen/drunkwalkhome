using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float Speed;
    float speedX, speedY;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Speed = Random.Range(2, 6);
        speedX = Input.GetAxisRaw("Horizontal") * Speed;
        speedY = Input.GetAxisRaw("Vertical") * Speed;
        rb.velocity = new Vector2(speedX, speedY);
    }
}
