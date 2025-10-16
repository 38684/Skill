
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndlessRunner : MonoBehaviour
{
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpForce = 7.5f;
    [SerializeField] Animator animator;
    
    private InputAction jump;
    private float velocity;
    private float jumpTime;

    enum State { running, jumping };
    State currentState = State.running;

    private void Start()
    {
        jump = InputSystem.actions.FindAction("Jump");

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Jump")
            {
                jumpTime = clip.length;
            }
        }
    }

    private void Update()
    {
        Debug.Log(velocity);
        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);

        jump.performed += context =>
        {
            if (currentState == State.running)
            {
                currentState = State.jumping;
                velocity = jumpForce;
                animator.SetBool("isJumping", true);
                StartCoroutine(EndJump());
            }
        };

        if (transform.position.y > -3f)
            velocity += gravity * Time.deltaTime;
        else
            currentState = State.running;

        if (currentState == State.running)
            velocity = 0;
    }

    IEnumerator EndJump()
    {
        yield return new WaitForSeconds(jumpTime);
        animator.SetBool("isJumping", false);
        velocity = 0;
    }
}
