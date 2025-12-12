
using System.Collections;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] float startDelay;
    [SerializeField] float speed;
    [SerializeField] float g;
    [SerializeField] float v;
    [SerializeField] float endPosition;
    [SerializeField] Animator animator;
    Vector2 startPosition;
    float jumpStartDelay = 0.5f;
    float t;
    float y;
    bool isJumping;

    private void Start()
    {
        startPosition = transform.position;

        StartCoroutine(Jumping());
    }

    IEnumerator Jumping()
    {
        yield return new WaitForSeconds(startDelay);

        isJumping = true;
        animator.SetBool("Jumping", true);

        yield return new WaitForSeconds(jumpStartDelay);

        while (isJumping)
        {
            t = Mathf.Clamp((Time.time - startDelay - jumpStartDelay) * speed, 0, endPosition - startPosition.x);


            y = -0.5f * g * Mathf.Pow(t, 2) + v * t + startPosition.y;

            transform.position = new Vector2(t + startPosition.x, y);

            yield return new WaitForSeconds(Time.deltaTime);

            if (transform.position.x >= endPosition)
            {
                isJumping = false;
                animator.SetBool("Jumping", false);
            }
        }


        yield break;
    }
}
