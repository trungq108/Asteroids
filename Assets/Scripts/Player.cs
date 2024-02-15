using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float thrustSpeed;
    public float rotationSpeed = 1f;
    private Rigidbody2D rb;
    private float turnDirection;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            turnDirection = 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            turnDirection= -1.0f;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W));
        {
            rb.AddForce(transform.up * thrustSpeed);
        }
        if(turnDirection != 0.0f)
        {
            rb.AddTorque(rotationSpeed * turnDirection);
        }
    }
    void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab,transform.position, Quaternion.identity);

        bullet.Shoot(transform.up);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {   if(collision.gameObject.CompareTag("Asteroid"))
        {
        this.gameObject.SetActive(false);
        FindAnyObjectByType<GameManager>().OnPlayerDead();
        }
    }
    public void TurnoffCollision()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Collider");
    }
        public void TurnOnCollision()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

}
