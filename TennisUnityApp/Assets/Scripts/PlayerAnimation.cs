using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayForehandAnimation()
    {
        animator.Play("forehand");
    }

    public void PlayBackhandAnimation()
    {
        animator.Play("backhand");
    }

    public void PlayServeAnimation()
    {
        animator.Play("serve");
    }

    public void PlayServePrepareAnimation()
    {
        animator.Play("serve-prepare");
    }
}
