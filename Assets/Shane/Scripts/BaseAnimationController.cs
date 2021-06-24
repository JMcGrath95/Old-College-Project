using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BaseAnimationController : MonoBehaviour
{
    protected Animator animator;
    protected string currentAnimation;

    public virtual void Awake() => animator = GetComponent<Animator>();

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }
}
