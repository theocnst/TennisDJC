using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform aimTarget;
    float speed = 3f;

    bool hitting;

    public Transform ball;
    Animator animator;

    ShotManager shotManager;
    Shot currentShot;

    private void Start()
    {
        animator = GetComponent<Animator>();
        shotManager = GetComponent<ShotManager>();
        currentShot = shotManager.topSpin;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.F))
        {
            hitting = true;
            currentShot = shotManager.topSpin;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            hitting = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            hitting = true;
            currentShot = shotManager.flat;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            hitting = false;
        }

        if (hitting)
        {
            aimTarget.Translate(new Vector3(0, 0, h) * speed * 2 * Time.deltaTime);
        }

        if ((h != 0 || v != 0) && !hitting)
        {
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime);
        }

        Vector3 ballDir = ball.position - transform.position;

        Debug.DrawRay(transform.position, ballDir);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TennisBall"))
        {
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);

            Vector3 ballDir = ball.position - transform.position;

            if (ballDir.x >= 0)
                animator.Play("forehand");
            else
                animator.Play("backhand");
        }
    }
}
