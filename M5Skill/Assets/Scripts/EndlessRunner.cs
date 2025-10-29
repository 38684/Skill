
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

    enum State { running, jumping };
    State currentState = State.running;

    private void Start()
    {
        jump = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);

        jump.performed += context =>
        {
            if (currentState == State.running)
            {
                currentState = State.jumping;
                velocity = jumpForce;
                animator.SetBool("isJumping", true);
            }
        };

        if (transform.position.y > -3f)
            velocity += gravity * Time.deltaTime;
        else
            currentState = State.running;

        if (currentState == State.running)
        {
            animator.SetBool("isJumping", false);
            velocity = 0;
        }
    }
}
