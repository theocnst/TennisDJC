using UnityEngine;

public class Bot : MonoBehaviour
{
    float speed = 3f;
    int difficultyLevel = 1; // Start at the easiest level

    Animator animator;
    public Transform ball;
    public Transform aimTarget;
    public Transform[] targets; // Assuming this is already populated in the editor with the middle and corner aim targets

    ShotManager shotManager;
    Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        shotManager = GetComponent<ShotManager>();
    }

    void Update()
    {
        CheckDifficultyInput();
        Move();
    }

    void CheckDifficultyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            difficultyLevel = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            difficultyLevel = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            difficultyLevel = 3;

        AdjustDifficultySettings();
    }

    void AdjustDifficultySettings()
    {
        switch (difficultyLevel)
        {
            case 1:
                speed = 1.5f; // Moves slowly
                break;
            case 2:
                speed = 2.75f; // Normal speed
                break;
            case 3:
                speed = 4.5f; // Moves faster
                break;
        }
    }

    Vector3 PickTarget()
    {
        if (difficultyLevel == 1)
        {
            // Only hits the middle aim target
            return targets[1].position; // Assuming the middle target is at index 1
        }
        else if (difficultyLevel == 2)
        {
            // Can hit the first and third aim targets
            int[] validTargets = { 0, 2 }; // Assuming indexes 0 and 2 are the corners
            int selectedTarget = validTargets[Random.Range(0, validTargets.Length)];
            return targets[selectedTarget].position;
        }
        else
        {
            // Can hit all aim targets
            return targets[Random.Range(0, targets.Length)].position;
        }
    }

    Shot PickShot()
    {
        if (difficultyLevel == 1)
            return shotManager.defaultHit; // Assuming there is a default hit in shotManager
        else
        {
            // Can use flat and top spin
            int randomValue = Random.Range(0, 2);
            return randomValue == 0 ? shotManager.topSpin : shotManager.flat;
        }
    }

    void Move()
    {
        targetPosition.x = ball.position.x;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TennisBall"))
        {
            Shot currentShot = PickShot();
            Vector3 dir = PickTarget() - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);

            Vector3 ballDir = ball.position - transform.position;
            if (ballDir.x <= 0)
                animator.Play("forehand");
            else
                animator.Play("backhand");

            ball.GetComponent<TennisBall>().hitter = "Bot";
        }
    }
}
