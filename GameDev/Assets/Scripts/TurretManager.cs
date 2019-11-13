using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public int damagePerHit = 50;
    public float timeBetweenHits = 1f;
    public float range = 50f;
    public Transform barrel;

    float timer = 1f;
    Ray shootray;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer hitLine;
    Vector3 linePosition;

    private void Awake() {
        shootableMask = LayerMask.GetMask("Shootable");
        hitLine = GetComponent<LineRenderer>();
        timer = timeBetweenHits;
    }

    private void OnTriggerStay(Collider other) {
        timer -= 1f * Time.deltaTime;
        if (other.tag == "Enemy") {
            transform.LookAt(other.transform);
            if (timer <= 0f) {
                Debug.Log("Shot fired");
                Fire(other.transform);         
            }
        }
    }

    void Fire (Transform target) {
        Debug.Log("Shot at: " + target.position);
        timer = 1f;
        linePosition = barrel.position;
        shootray.origin = linePosition;
        shootray.direction = barrel.forward;

        if (Physics.Raycast(shootray, out shootHit, range, shootableMask)) {
            Debug.Log("Shot Collided");
            damage damage = shootHit.collider.GetComponent<damage>();

            if (damage != null) {
                damage.TakeDamage(damagePerHit);
            }
            hitLine.enabled = true;
            hitLine.SetPosition(0, linePosition);
            hitLine.SetPosition(1, shootHit.transform.position);            
        } 

    }
}
