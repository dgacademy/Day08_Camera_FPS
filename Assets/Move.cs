using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public float moveSpeed = 8f;
    public GameObject healFX;

    CharacterController con;
    Vector3 moveDirection = Vector3.zero;

    float jumpSpeed = 8f;
    float gravity = 20f;

    GameObject fx;
    bool isHealing = false;

    void Start () {
        con = GetComponent<CharacterController>();
	}
	
	void Update () {
        if (con.isGrounded)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            moveDirection = (new Vector3(h, 0, v)).normalized;

            //transform.LookAt(transform.position + moveDirection);
            moveDirection = transform.TransformDirection(moveDirection);

            moveDirection *= moveSpeed;
            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        con.Move(moveDirection * Time.deltaTime);
	}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.name == "Step3")
        {
            if (!isHealing) {
                fx = Instantiate(healFX, transform.Find("FXPos"));
                isHealing = true;
                Invoke("RemoveHealFX", 1.9f);
            }
        }
    }

    void RemoveHealFX()
    {
        Destroy(fx.gameObject);
        isHealing = false;
    }
}
