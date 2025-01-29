using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.AI;

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
    private int magazineSize = 5;
    public int shotsLeft;
    private Vector3 jumpPoint;
    public bool controlEnabled;

    private void Shoot()
    {
        if (Input.GetKeyDown(fireWeapon) && shotsLeft > 0)
        {
            thisAnim.SetTrigger("Shoot");
            thisAnim2.SetTrigger("Shoot");
            rb.AddForce(-shotPoint.transform.right * recoilStrength, ForceMode2D.Impulse);
            isGrounded = rb.IsTouchingLayers(ground) || rb.IsTouchingLayers(snow);
            // if (Math.Abs(rb.velocity.x) > 12f)
            //     rb.velocity = new Vector2(rb.velocity.x - (rb.velocity.x - 12f), rb.velocity.y);
            // if (Math.Abs(rb.velocity.y) > 12f)
            //     rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - (rb.velocity.y - 12f));
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            jumpPoint = gameObject.transform.position;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
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
        if (!isGrounded && gameObject.transform.position.y - jumpPoint.y > 0.5f)
        {
            Vector3 mouse_pos = Input.mousePosition;
            mouse_pos.z = 0f;
            Vector3 object_pos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            float angle = Mathf.Atan2(mouse_pos.y - object_pos.y, mouse_pos.x - object_pos.x) * Mathf.Rad2Deg - 5f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotateSpeed * Time.deltaTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        thisAnim = transform.GetChild(0).GetComponent<Animator>();
        thisAnim2 = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        shotsLeft = magazineSize;
        controlEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (controlEnabled)
        {
            Shoot();
            Jump();
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
            if ((contact.normal.y > 0) && (rb.IsTouchingLayers(ground) || rb.IsTouchingLayers(snow)))
            {
                isGrounded = true;
                shotsLeft = magazineSize;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
    }
}
