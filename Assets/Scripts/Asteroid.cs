using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public float size = 2f;
    public float minSize = 1f;
    public float maxSize = 3f;
    public float lifeTime = 30f;
    public float flySpeed = 100f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0, 0, Random.value * 360f);
        transform.localScale = Vector3.one * size;
        rb.mass = size;
        Destroy(gameObject, lifeTime);
    }
    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * flySpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (size * 0.5 >= minSize)
            {
                Split();
                Split();
            }
            Destroy(gameObject);
            FindAnyObjectByType<GameManager>().AsteroidDestroy(this);
        }
        void Split()
        {
            Vector2 position = transform.position;
            position += Random.insideUnitCircle;
            Asteroid half = Instantiate(this, position, transform.rotation);
            half.size = size * 0.5f;

            half.SetTrajectory(Random.insideUnitCircle.normalized);
        }
    }
}
