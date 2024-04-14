using System.Security.Cryptography;
using UnityEngine;

public class PlayerHitting : MonoBehaviour
{
    public Transform aimTarget;
    public Transform ball;
    public bool IsHitting { get; private set; }
    private ShotManager shotManager;
    private Shot currentShot;
    [SerializeField] private Transform serveLeft;
    [SerializeField] private Transform serveRight;
    public bool isServingLeft = true;

    private void Start()
    {
        shotManager = GetComponent<ShotManager>();
        currentShot = shotManager.topSpin;
    }

    void Update()
    {
        HandleHittingInput();
        HandleServingInput();
    }

    private void HandleHittingInput()
    {
        IsHitting = Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.E);

        if (IsHitting)
        {
            currentShot = Input.GetKeyDown(KeyCode.F) ? shotManager.topSpin : shotManager.flat;
            aimTarget.Translate(new Vector3(0, 0, Input.GetAxisRaw("Horizontal")) * 6f * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.E))
        {
            ball.GetComponent<TennisBall>().hitter = "Character";
            IsHitting = false;
        }
    }

    private void HandleServingInput()
    {
        IsHitting = Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.T);

        if (IsHitting)
        {
            var playerAnimation = GetComponent<PlayerAnimation>();
            currentShot = Input.GetKeyDown(KeyCode.R) ? shotManager.flatServe : shotManager.topSpinServe;
            aimTarget.Translate(new Vector3(0, 0, Input.GetAxisRaw("Horizontal")) * 6f * Time.deltaTime);
            playerAnimation.PlayServePrepareAnimation();
        }

        if (Input.GetKeyUp(KeyCode.R) || Input.GetKeyUp(KeyCode.T))
        {
            var playerAnimation = GetComponent<PlayerAnimation>();
            Vector3 dir = aimTarget.position - transform.position;
            ball.transform.position = transform.position + new Vector3(0.2f, 1, 0);
            ball.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);
            ball.GetComponent<TennisBall>().hitter = "Character";
            ball.GetComponent<TennisBall>().playing = true;
            playerAnimation.PlayServeAnimation();
            IsHitting = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TennisBall"))
        {
            PlayHitEffect(currentShot);
            HandleBallHit(other);
        }
    }

    private void PlayHitEffect(Shot shot)
    {
        switch (shot)
        {
            case var s when s == shotManager.flat || s == shotManager.flatServe:
                SoundManager.Instance.PlayClip("soft_hit");
                break;
            case var s when s == shotManager.topSpin || s == shotManager.topSpinServe:
                SoundManager.Instance.PlayClip("hard_hit");
                break;
        }
    }

    private void HandleBallHit(Collider ballCollider)
    {
        Vector3 dir = aimTarget.position - transform.position;
        ballCollider.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);

        var playerAnimation = GetComponent<PlayerAnimation>();
        Vector3 ballDir = ball.position - transform.position;
        if (ballDir.x >= 0)
            playerAnimation.PlayForehandAnimation();
        else
            playerAnimation.PlayBackhandAnimation();

        ball.GetComponent<TennisBall>().hitter = "Character";
    }

    public void Reset()
    {
        transform.position = isServingLeft ? serveLeft.position : serveRight.position;
        isServingLeft = !isServingLeft;
    }
}
