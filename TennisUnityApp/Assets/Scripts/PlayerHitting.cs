using System.Security.Cryptography;
using UnityEngine;

public class PlayerHitting : MonoBehaviour
{
    public Transform aimTarget;
    public Transform ball;
    public bool IsHitting { get; private set; }
    ShotManager shotManager;
    Shot currentShot;
    [SerializeField] Transform serveLeft;
    [SerializeField] Transform serveRight;
    public bool isServingLeft = true;

    private void Start()
    {
        shotManager = GetComponent<ShotManager>();
        currentShot = shotManager.topSpin;
    }

    void Update()
    {
        IsHitting = false;

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.E))
        {
            IsHitting = true;
            currentShot = Input.GetKeyDown(KeyCode.F) ? shotManager.topSpin : shotManager.flat;
            aimTarget.Translate(new Vector3(0, 0, Input.GetAxisRaw("Horizontal")) * 3f * 2 * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.E))
        {
            IsHitting = false;
        }

        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.T))
        {
            IsHitting = true;
            var playerAnimation = GetComponent<PlayerAnimation>();
            currentShot = Input.GetKeyDown(KeyCode.R) ? shotManager.flatServe : shotManager.kickServe;
            aimTarget.Translate(new Vector3(0, 0, Input.GetAxisRaw("Horizontal")) * 3f * 2 * Time.deltaTime);
            playerAnimation.PlayServePrepareAnimation();
        }

        if (Input.GetKeyUp(KeyCode.R) || Input.GetKeyUp(KeyCode.T))
        {
            IsHitting = false;
            var playerAnimation = GetComponent<PlayerAnimation>();
            ball.transform.position = transform.position + new Vector3(0.2f, 1, 0);
            Vector3 dir = aimTarget.position - transform.position;
            ball.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);
            playerAnimation.PlayServeAnimation();
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TennisBall"))
        {
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);

            Vector3 ballDir = ball.position - transform.position;

            var playerAnimation = GetComponent<PlayerAnimation>();
            if (ballDir.x >= 0)
                playerAnimation.PlayForehandAnimation();
            else
                playerAnimation.PlayBackhandAnimation();
        }
    }

    public void Reset()
    {
        if (isServingLeft)
            transform.position = serveLeft.position;
        else
            transform.position = serveRight.position;
        
        isServingLeft = !isServingLeft;
    }
}
