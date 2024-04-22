using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Animator animator;
    private Rigidbody rb;
    private Vector3 movement = Vector3.zero;
    private Quaternion rotation = Quaternion.identity;

    [HeaderAttribute("회전 속도")]
    public float turnSpeed = 20f;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal, 0f, vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        animator.SetBool("IsWalking", isWalking);

        Vector3 desiredFoward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.fixedDeltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredFoward);
    }

    private void OnAnimatorMove()
    {
        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);
        rb.MoveRotation(rotation);
    }
}
