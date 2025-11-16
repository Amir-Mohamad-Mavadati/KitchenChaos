using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator Animator;
    private Player Player;


    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        Animator.SetBool("IsWalking", Player.GetIsWalking());
    }
}
