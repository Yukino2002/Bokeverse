using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PllayerMovement : MonoBehaviour {
    public float moveSpeed;

    private bool isMoving;
    private Vector2 input;

    private Animator animator;
    // get reference to the animator component
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (!isMoving) {
            // get axis raw returns 1, 0, or -1
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // remove diagonal movement
            if (input.x != 0) {
                input.y = 0;
            }

            if (input != Vector2.zero) {
                // set the animator to the direction of the player
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                // starts the coroutine
                StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    // Does a routine over a period of time, returns asset type IEnumerator
    IEnumerator Move(Vector3 targetPos) {
        isMoving = true;
        // moves the player towards the target position by a very small amount
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // returns and updates the position of the player by that small amount
        transform.position = targetPos;
        isMoving = false;
    }
}
