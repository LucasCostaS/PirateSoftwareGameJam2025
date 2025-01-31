using UnityEngine;

public class Level_3_Controller : MonoBehaviour
{
    public GameObject player;
    public LayerMask water;
    public LayerMask spike;
    private Vector3 activeSpawnPoint;
    private float results;
    private Rigidbody2D rb;
    public Vector3[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        activeSpawnPoint = spawnPoints[0];
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawnPoint();
        WaterCollision();
        SpikeCollision();
    }

    private void UpdateSpawnPoint()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (player.transform.position.x >= spawnPoints[i].x - 4f && player.transform.position.x <= spawnPoints[i].x + 4f
                && player.transform.position.y >= spawnPoints[i].y - 10f && player.transform.position.y <= spawnPoints[i].y + 10f)
            {
                activeSpawnPoint = spawnPoints[i];
                break;
            }
        }
    }

    void WaterCollision()
    {
        if (rb.IsTouchingLayers(water))
        {
            player.transform.position = activeSpawnPoint;
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    void SpikeCollision()
    {
        if (rb.IsTouchingLayers(spike))
        {
            ContactPoint2D[] contacts = new ContactPoint2D[10];
            player.GetComponent<Collider2D>().GetContacts(contacts);
            // Loop through all contact points
            foreach (ContactPoint2D contact in contacts)
            {
                if (contact.collider == null)
                    continue;
                    
                if (contact.collider.gameObject.layer == LayerMask.NameToLayer("Spike") && contact.normal.y > 0.5f)
                {
                    player.transform.position = activeSpawnPoint;
                    rb.velocity = new Vector2(0f, 0f);
                }
            }
        }
    }
}


