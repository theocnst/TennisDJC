using UnityEngine;

public class Shot
{
    public float upForce;
    public float hitForce;

    public Shot(float upForce, float hitForce)
    {
        this.upForce = upForce;
        this.hitForce = hitForce;
    }
}

[SerializeField]
public class ShotManager : MonoBehaviour
{
    public Shot flat = new Shot(8, 11);
    public Shot topSpin = new Shot(3.5f, 15);
    public Shot flatServe = new Shot(8, 11);
    public Shot topSpinServe = new Shot(3.5f, 15);
    public Shot defaultHit = new Shot(10, 10);
}
