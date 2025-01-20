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
    public float torqueForce = 1.25f;
    private Rigidbody2D rb;
    public GameObject shotPoint;
    public LayerMask ground; 
    private Animator thisAnim;
    private Animator thisAnim2;

    private void Shoot()
    {
        if (Input.GetKeyDown(fireWeapon)){
            thisAnim.SetTrigger("Shoot");
            thisAnim2.SetTrigger("Shoot");
            rb.AddForce(-shotPoint.transform.right * recoilStrength, ForceMode2D.Impulse);
        }     
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            rb.AddForce(new Vector2(0f, jumpForce));
    }

    private void ChangeDirection()
    {
        float shotPointX = shotPoint.transform.right.x;
        if (Input.GetKeyDown(KeyCode.Q) && shotPointX > 0f)
            gameObject.transform.Rotate(new Vector3(0f, 180f, 0f), Space.World);

        if (Input.GetKeyDown(KeyCode.E) && shotPointX < 0f)
            gameObject.transform.Rotate(new Vector3(0f, -180f, 0f), Space.World);
    }

    private void Balance()
    {
        if (Input.GetKey(KeyCode.A) && rb.angularVelocity < 80f && !isGrounded)
            rb.AddTorque(torqueForce);

        if (Input.GetKey(KeyCode.D) && rb.angularVelocity > -80f && !isGrounded)
            rb.AddTorque(-torqueForce);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        thisAnim = transform.GetChild(0).GetComponent<Animator>();
        thisAnim2 = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = rb.IsTouchingLayers(ground);
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            Shoot();
            Jump();
            ChangeDirection();
            Balance();
        }
    }
}
