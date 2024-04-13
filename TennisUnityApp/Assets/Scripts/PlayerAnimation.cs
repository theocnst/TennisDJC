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
        SoundManager.Instance.PlayClip("soft_hit", 30);
    }

    public void PlayBackhandAnimation()
    {
        animator.Play("backhand");
        SoundManager.Instance.PlayClip("soft_hit", 30);
    }

    public void PlayServeAnimation()
    {
        animator.Play("serve");
        SoundManager.Instance.PlayClip("soft_hit", 30);
    }

    public void PlayServePrepareAnimation()
    {
        animator.Play("serve-prepare");
        SoundManager.Instance.PlayClip("soft_hit", 30);
    }
}
