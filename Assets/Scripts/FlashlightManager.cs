using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashlightManager : MonoBehaviour
{
    public float lightAngle = 50;
    public float radius = 5;
    private float outerRadius = 0;
    private bool checking = true;
    private EnemyBehavior enemyBehavior;

    Light2D light = null;

    // Start is called before the first frame update
    void Start()
    {
        light = this.GetComponentInChildren<Light2D>();
        enemyBehavior = this.GetComponentInParent<EnemyBehavior>();
        if(light) {
            light.pointLightInnerAngle = lightAngle;
            light.pointLightOuterAngle = lightAngle + 25;
            light.pointLightInnerRadius = radius - 1;
            light.pointLightOuterRadius = radius;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(light) {
            light.pointLightInnerAngle = lightAngle;
            light.pointLightOuterAngle = lightAngle + 25;
            light.pointLightInnerRadius = radius - 1;
            light.pointLightOuterRadius = radius;
            if (checking) {
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (InLight(player.transform))
                    {
                        checking = false;
                        enemyBehavior.FoundPlayer();
                    }
                }
            }
        }
    }

    public bool InLight(Transform position) {
        float offset = (90 - lightAngle/2);
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        float newRadius = radius - 1f;
        hits.Add(Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos((Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + offset))), Mathf.Sin((Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + offset)))), newRadius));
        Debug.DrawRay(transform.position, new Vector2(Mathf.Cos((Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + offset))), Mathf.Sin((Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + offset)))) * newRadius, Color.green);
        for(int i = 1; i <= Mathf.Floor(lightAngle / 10); i ++) {
            hits.Add(Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos((Mathf.Deg2Rad * i * 10) + (Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + offset))), Mathf.Sin((Mathf.Deg2Rad * i * 10) + (Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + offset)))), newRadius));
            Debug.DrawRay(transform.position, new Vector2(Mathf.Cos((Mathf.Deg2Rad * i * 10) + (Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + offset))), Mathf.Sin((Mathf.Deg2Rad * i * 10) + (Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + offset)))) * newRadius, Color.green);
        }
        hits.Add(Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos((Mathf.Deg2Rad * lightAngle) + (Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + offset))), Mathf.Sin((Mathf.Deg2Rad * lightAngle) + (Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + offset)))), newRadius));
        Debug.DrawRay(transform.position, new Vector2(Mathf.Cos((Mathf.Deg2Rad * lightAngle) + (Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + offset))), Mathf.Sin((Mathf.Deg2Rad * lightAngle) + (Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + offset)))) * newRadius, Color.green);

        foreach(RaycastHit2D hit in hits) {
            if (hit.collider != null) {
                if(hit.collider.gameObject.tag != "Player")
                    continue;
                return true;
            }
        }
        return false;
    }
}
