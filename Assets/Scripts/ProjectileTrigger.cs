using UnityEngine;

/// <summary>
/// The class definition for a projectile's trigger
/// </summary>
/// <remarks>
/// Attach this script as a component to any object capable of triggering projectiles
/// The spawn transform should represent the position where the projectile is to appear, i.e. gun barrel end
/// </remarks>
public class ProjectileTrigger : MonoBehaviour
{
    /// <summary>
    /// Public fields
    /// </summary>
    public GameObject projectile;    // this is a reference to your projectile prefab
    public Transform spawnPoint; // this is a reference to the transform where the prefab will spawn
    private GameObject projectileClone;

    /// <summary>
    /// Message that is called once per frame
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            projectileClone = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            projectileClone.GetComponent<Rigidbody2D>().AddForce(spawnPoint.transform.right * gameObject.GetComponent<PlayerController>().recoilStrength, ForceMode2D.Impulse);
            Debug.Log(projectileClone.GetComponent<Rigidbody2D>().velocity);
        }
    }
}