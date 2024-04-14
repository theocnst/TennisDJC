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

public class ShotManager : MonoBehaviour
{
    public Shot flat = new Shot(5, 8);
    public Shot topSpin = new Shot(3, 15);
    public Shot flatServe = new Shot(5, 8);
    public Shot topSpinServe = new Shot(3, 15);
    public Shot defaultHit = new Shot(7, 8);
}
