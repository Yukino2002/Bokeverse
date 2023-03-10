using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    public LayerMask longGrassLayer;
    public LayerMask sensei;
    public LayerMask healer;

    private int partyCount;
    private bool starterBokemon;
    public bool StarterBokemon { get => starterBokemon; set => starterBokemon = value; }

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject healthIndicator;
    [SerializeField] BokemonParty bokemonParty;

    // create an event for encounters in the long grass
    public event Action OnEncounter;

    private bool isMoving;
    private Vector2 input;

    private Animator animator;

    private void Start() {
        starterBokemon = true;
    }
    
    // run during compile time
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame, handleUpdate called from the game controller
    public void HandleUpdate() {
        partyCount = bokemonParty.Bokemons.Count;
        if (bokemonParty.GetHealthyBokemon() == null && partyCount > 0) {
            healthIndicator.SetActive(true);
        } else {
            healthIndicator.SetActive(false);
        }

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

                // checks and starts the coroutine
                if (IsWalkable(targetPos, partyCount)){
                    canvas.SetActive(false);
                    StartCoroutine(Move(targetPos));
                }
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

        CheckForEncounters();
    }

    // function to check if the player can move to a certain position, with multiple use cases
    private bool IsWalkable(Vector3 targetPos, int partyCount) {
        // first parameter is the position, second is the radius, third is the layer of the object we want to check
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null) {
            return false;
        }

        if (partyCount == 0 && Physics2D.OverlapCircle(targetPos, 0.2f, longGrassLayer) != null) {
            canvas.SetActive(true);
            return false;
        }

        if (Physics2D.OverlapCircle(targetPos, 0.2f, sensei) != null) {
            Debug.Log("Sensei");
            if (starterBokemon && partyCount == 0) {
                starterBokemon = false;
                bokemonParty.GetStarterBokemon();
            }
            
            return false;
        }

        if (Physics2D.OverlapCircle(targetPos, 0.2f, healer) != null) {
            if (partyCount > 0) {
                bokemonParty.HealBokemons();
            }
            
            return false;
        }

        return true;
    }

    // function to check for encounters in the long grass
    private void CheckForEncounters() {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, longGrassLayer) != null && bokemonParty.GetHealthyBokemon() != null) {
            if (UnityEngine.Random.Range(1, 101) <= 10) {
                // otherwise the player will continue to move after the encounter
                animator.SetBool("isMoving", false);
                OnEncounter();
            }
        }
    }
}
