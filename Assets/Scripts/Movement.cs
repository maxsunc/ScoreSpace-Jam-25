using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb;
    // for direction of movement
    private Vector2 move;
    // determines if movement is happened
    private bool isMoving;
    private bool isDashing;
    // how fast we going
    [SerializeField]
    private float moveSpeed;
    // dash speed
    [SerializeField]
    private float dashSpeed;
    // how long dash lasts for
    [SerializeField]
    private float dashTime;
    public GameObject illusionObject;
    public AudioSource walkSfx;
    public ParticleSystem ps;
    // the cooldown for dash
    [SerializeField]
    private float dashCooldown;
    private SpriteRenderer sprite;
    private Animator animator;
    private float baseSpeed;
    



    void Awake()
    {
        baseSpeed = moveSpeed;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isMoving && isDashing == false)
        {
            moveSpeed = dashSpeed;
            // start the reset
            StartCoroutine(ResetSpeed());
        }
    }

    void FixedUpdate()
    {
        movePlayer();
        Move();
    }

    void Move()
    {
        rb.MovePosition(rb.position + move.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void movePlayer()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        

        // check if we are moving
        isMoving = (move.x != 0 || move.y != 0) ? true : false;
        if (isMoving)
        {
            ps.Play();
            animator.SetFloat("x", move.x);
            animator.SetFloat("y", move.y);

            if (!walkSfx.isPlaying)
            {
                walkSfx.Play();
            }
        }
        else {
            ps.Stop();
        }
        animator.SetBool("isMoving", isMoving);
        if (Input.GetKeyDown(KeyCode.Space) && isMoving && isDashing == false)
        {
            moveSpeed = dashSpeed;
            // start the reset
            StartCoroutine(ResetSpeed());
        }
        
    }

    IEnumerator ResetSpeed()
    {
        SpawnIllusion();
        isDashing = true;
        // time waited before dash
        yield return new WaitForSeconds(dashTime/3);
        SpawnIllusion();
        yield return new WaitForSeconds(dashTime / 3);
        SpawnIllusion();
        yield return new WaitForSeconds(dashTime / 3);
        moveSpeed = baseSpeed;
        yield return new WaitForSeconds(dashCooldown);

        isDashing = false;
    }

    private void SpawnIllusion()
    {
        GameObject illusion = Instantiate(illusionObject, transform.position, transform.rotation);
        illusion.GetComponent<SpriteRenderer>().sprite = sprite.sprite;
        Destroy(illusion, 1f);
    }
}
