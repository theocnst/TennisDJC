using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 3f;
    bool isMoving = false;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if ((h != 0 || v != 0) && !GetComponent<PlayerHitting>().IsHitting)
        {
            if (!isMoving)
            {
                SoundManager.Instance.PlayLoop("footsteps");
                isMoving = true;
            }
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime);
        }
        else if (isMoving)
        {
            SoundManager.Instance.StopLoop("footsteps");
            isMoving = false;
        }
    }
}
