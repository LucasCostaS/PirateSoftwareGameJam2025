using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// The class definition for a projectile
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Public fields
    /// </summary>
    public float speed = 10f;   // this is the projectile's speed
    public float lifespan = 1.25f; // this is the projectile's lifespan (in seconds)

    /// <summary>
    /// Private fields
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// Message that is called when the script instance is being loaded
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Message that is called before the first frame update
    /// </summary>
    void Start()
    {
        rb.AddForce(rb.transform.right * speed);
        Destroy(gameObject, lifespan);
    }

    void OnCollisionEnter2D(){
        Destroy(gameObject);
    }
}