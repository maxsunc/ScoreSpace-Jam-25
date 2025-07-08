using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyBehavior : MonoBehaviour
{
    public Direction[] steps;
    public int stepIndex = 0;
    public int moveSpeed = 3;

   // private GameObject rotatableItem = null;
    private Rigidbody2D rb;
    private float reqXDistance, reqYDistance;
    // directions the enemy moves
    private int dirX, dirY;
    private Vector3 turnSmoothVelocity;
    private float flashLightZRotation = 90f;
    public AudioSource alertedSound;
    public AudioSource questionSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // set the rotateableObject to flashlight
       // rotatableItem = GameObject.Find("Flashlight");
        CalculateDirection();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistances();
    }

    void FixedUpdate()
    {
        // move enemy in dir
        Vector2 dir = new Vector2(dirX, dirY);
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
        //rotatableItem.transform.up = Vector3.SmoothDamp(transform.up, new Vector3(dirX,dirY,0), ref turnSmoothVelocity, 0.3f);
        // Rotate the cube by converting the angles into a quaternion.
      //  Quaternion target = Quaternion.Euler(0, 0, flashLightZRotation);

        // Dampen towards the target rotation
      //  rotatableItem.transform.rotation = Quaternion.Slerp(rotatableItem.transform.rotation, target, Time.fixedDeltaTime * 3f);
    }
    private void CalculateDirection()
    {
        int disX = steps[stepIndex].distanceX;
        int disY = steps[stepIndex].distanceY;
        // distance to travel to
        reqXDistance = transform.position.x + disX;
        reqYDistance = transform.position.y + disY;

        if (steps[stepIndex].distanceX < 0)
        {
            // distacne x is negative
            // we want to move toward negative
            dirX = -1;
            dirY = 0;
        }
        else if (steps[stepIndex].distanceX > 0)
        {
            // distance x positive
            dirX = 1;
            dirY = 0;
        }
        else if (steps[stepIndex].distanceY < 0)
        {
            dirY = -1;
            dirX = 0;
        }
        else if (steps[stepIndex].distanceY > 0)
        {
            dirY = 1;
            dirX = 0;
        }
    }
    private void CheckDistances()
    {
        // check if passed the reqDistance
        if (dirX < 0)
        {
            // moving right
            // check for less than the distance cuz backwards
            if (transform.position.x <= reqXDistance)
            {
                // increment or reset stepIndex
                stepIndex = (stepIndex == steps.Length - 1) ? 0 : stepIndex + 1;
                CalculateDirection();
              //  flashLightZRotation = (flashLightZRotation >= 360) ? 90 : flashLightZRotation + 90;
            }
        }
        else if (dirX > 0)
        {
            // moving left
            // check for less than the distance cuz backwards
            if (transform.position.x >= reqXDistance)
            {
                // increment or reset stepIndex
                stepIndex = (stepIndex == steps.Length - 1) ? 0 : stepIndex + 1;
                CalculateDirection();
               // flashLightZRotation = (flashLightZRotation >= 360) ? 90 : flashLightZRotation + 90;
            }
        }
        else if (dirY < 0)
        {
            // moving down
            // check for less than the distance cuz backwards
            if (transform.position.y <= reqYDistance)
            {
                // increment or reset stepIndex
                stepIndex = (stepIndex == steps.Length - 1) ? 0 : stepIndex + 1;
                CalculateDirection();
                //flashLightZRotation = (flashLightZRotation >= 360) ? 90 : flashLightZRotation + 90;
            }
        }
        else if (dirY > 0)
        {
            // moving up
            // check for less than the distance cuz backwards
            if (transform.position.y >= reqYDistance)
            {
                // increment or reset stepIndex
                stepIndex = (stepIndex == steps.Length - 1) ? 0 : stepIndex + 1;
                CalculateDirection();
              //  flashLightZRotation = (flashLightZRotation >= 360) ? 90 : flashLightZRotation + 90;
            }
        }
    }

    public void FoundPlayer()
    {
        // called when player is caught
        alertedSound.Play();
        this.gameObject.GetComponent<Animator>().SetBool("found", true);
        // relay to endGame
        StartCoroutine(GameObject.Find("GameManager").GetComponent<EndGame>().Death());
    }

}