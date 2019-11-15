using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDamage : MonoBehaviour
{
    public int damagePerHit = 25;
    public float timeBetweenHits = 0.3f;
    public float range = 20f;

    float timer;                                    // A timer to determine when to fire.
    Ray shootRay;                                   // A ray from the gun end forwards.
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    LineRenderer hitLine;
    Vector3 linePosition;

    void Awake() {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");

        //hitLine = GetComponent<LineRenderer>();
    }

    private void Update() {
        timer += Time.deltaTime;

        if(Input.GetButton("Fire1") && timer >= timeBetweenHits) {
            DoDamage();
        }
    }

    void DoDamage() {
        timer = 0f;
        linePosition = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
        shootRay.origin = linePosition;
        shootRay.direction = transform.forward;

        //hitLine.enabled = true;
        //hitLine.SetPosition(0, linePosition);
        
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask)) {
            damage damage = shootHit.collider.GetComponent<damage>();

            if (damage != null) {
                damage.TakeDamage(damagePerHit);
                Debug.Log("Damage Sent");
            }

            //hitLine.SetPosition(1, shootHit.point);
        }
        else {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            //hitLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            //Debug.Log("No Damage");
        }
    }
}
