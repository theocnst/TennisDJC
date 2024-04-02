using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 3f;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if ((h != 0 || v != 0) && !GetComponent<PlayerHitting>().IsHitting)
        {
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime);
        }
    }
}
