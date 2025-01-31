using System;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    public bool isGrounded;
    private KeyCode fireWeapon = KeyCode.Space;
    public float recoilStrength = 100f;
    public float jumpForce = 100f;
    public float rotateSpeed = 5f;
    private Rigidbody2D rb;
    public GameObject shotPoint;
    public LayerMask ground;
    public LayerMask snow;
    public LayerMask ice;
    private Animator thisAnim;
    private Animator thisAnim2;
    private AudioSource audioShot;
    private int magazineSize = 5;
    public int shotsLeft;
    private float jumpPoint = -100f;
    public bool controlEnabled;
    public GameObject magazineSlots; 
    public GameObject projectile;    // this is a reference to your projectile prefab
    public Transform spawnPoint; // this is a reference to the transform where the prefab will spawn
    private GameObject projectileClone;

    private void Shoot()
    {
        if (Input.GetKeyDown(fireWeapon) && shotsLeft > 0)
        {
            shotsLeft--;
            projectileClone = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            projectileClone.GetComponent<Rigidbody2D>().AddForce(spawnPoint.transform.right * recoilStrength, ForceMode2D.Impulse);
            thisAnim.SetTrigger("Shoot");
            audioShot.Play();
            thisAnim2.SetTrigger("Shoot");

            rb.AddForce(-shotPoint.transform.right * recoilStrength, ForceMode2D.Impulse);
            isGrounded = rb.IsTouchingLayers(ground) || rb.IsTouchingLayers(snow);
            magazineSlots.transform.GetChild(shotsLeft).GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            jumpPoint = gameObject.transform.position.y;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void ChangeDirection()
    {
        if (isGrounded)
        {
            float shotPointX = shotPoint.transform.right.x;
            if (Input.GetKeyDown(KeyCode.A) && shotPointX > 0f)
                gameObject.transform.Rotate(new Vector3(0f, 180f, 0f), Space.World);

            if (Input.GetKeyDown(KeyCode.D) && shotPointX < 0f)
                gameObject.transform.Rotate(new Vector3(0f, -180f, 0f), Space.World);
        }
    }

    private void Spin()
    {
        if (!isGrounded)
        {
            Vector3 mouse_pos = Input.mousePosition;
            mouse_pos.z = 0f;
            Vector3 object_pos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            float angle = Mathf.Atan2(mouse_pos.y - object_pos.y, mouse_pos.x - object_pos.x) * Mathf.Rad2Deg - 5f;
            ContactPoint2D[] contacts = new ContactPoint2D[10];
            rb.GetContacts(contacts);
            Quaternion lastRotation = transform.rotation;
            if (contacts.Length == 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotateSpeed * Time.deltaTime);
                if (rb.GetContacts(contacts) > 0 && !rb.IsTouchingLayers(ground))
                    transform.rotation = lastRotation;
            }
            else if (contacts.Length > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotateSpeed * Time.deltaTime);
                if (rb.GetContacts(contacts) > 0 && !rb.IsTouchingLayers(ground))
                    transform.rotation = lastRotation;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        thisAnim = transform.GetChild(0).GetComponent<Animator>();
        audioShot = transform.GetChild(0).GetComponent<AudioSource>();
        thisAnim2 = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        shotsLeft = magazineSize;
        controlEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        magazineSlots.transform.position = new Vector3(gameObject.transform.position.x - 1.3f, gameObject.transform.position.y - 0.55f, 0f);
        if (controlEnabled)
        {
            Shoot();
            Jump();
            if (jumpPoint != -100f && Math.Abs(gameObject.transform.position.y - jumpPoint) > 0.5f)
            {
                isGrounded = false;
                jumpPoint = -100f;
            }
            ChangeDirection();
            Spin();
        }

        ChangeDrag();
    }

    private void ChangeDrag()
    {
        if (rb.IsTouchingLayers(ground) && !rb.IsTouchingLayers(snow))
            rb.drag = 0.5f;
        if (!rb.IsTouchingLayers(ground) && rb.IsTouchingLayers(snow))
            rb.drag = 0.25f;
        if (rb.IsTouchingLayers(ice))
            rb.drag = 0.1f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Loop through all contact points
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Check the normal of each contact point
            if ((contact.normal.y > 0) && (contact.collider.gameObject.name == "GroundLayer" || contact.collider.gameObject.name == "Snow"))
            {
                isGrounded = true;
                shotsLeft = magazineSize;
                for (int i = 0; i < shotsLeft; i++)
                {
                    magazineSlots.transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
    }
}
