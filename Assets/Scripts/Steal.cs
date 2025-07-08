using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Steal : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float stealRadius;
    [SerializeField]
    private LayerMask stealable;
    [SerializeField]
    private AudioSource stealSfx;
    [SerializeField]
    private GameObject pickUpIndicator;
    private ScoreSystem scoreSystem;
    public Dictionary<string, int> values = new Dictionary<string, int>();

    void Start()
    {
        cam = Camera.main;
        scoreSystem = GameObject.Find("GameManager").GetComponent<ScoreSystem>();
        values.Add("Diamond", 250);
        values.Add("Chest", 1239);

        values.Add("Ruby", 508);
    }
    void Update()
    {
        // stores all colliders found within the radius
        Collider2D[] coli = Physics2D.OverlapCircleAll(transform.position, stealRadius, stealable);

        // check if first element exists
        /*if (coli.Length >= 1)
        {
            if (!pickUpIndicator.activeSelf)
            {
                // enable the indicator
                pickUpIndicator.SetActive(true);
            }
            // offsetter of the text
            Vector3 offset = new Vector3(0, -1, 0);
            // bring text to world pos as a screen pos
            pickUpIndicator.transform.position = cam.WorldToScreenPoint((transform.position + offset));
            // indicate that first item can be picked up
            pickUpIndicator.GetComponent<Text>().text = "E to pick up " + coli[0].gameObject.name;
        }
        else
        {
            // disasble the indicator
            pickUpIndicator.SetActive(false);
        }
*/
        if (Input.GetKeyDown(KeyCode.E))
        {
            // check if first element exists
            if(coli.Length >= 1)
            {
                stealSfx.pitch = Random.Range(0.9f, 1.15f);
                // get score according to worth
                stealSfx.Play();
                // find the atifact in the array
                foreach(KeyValuePair<string, int> kvp in values)
                {
                    // use substring to avoid names like Diamond (1)
                    if(coli[0].gameObject.name.Contains(kvp.Key))
                    {
                        scoreSystem.addScore(kvp.Value);
                        break;
                    }
                }
                // remove it
                Destroy(coli[0].gameObject, 0.001f);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stealRadius);
        //Draws the same circle the physics drew but you can see it
    }

}
