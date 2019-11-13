using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public int damagePerHit = 50;
    public float timeBetweenHits = 1f;
    public float range = 500f;
    public Transform barrel;

    float timer;
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

    private void Update() {
    }

    private void OnTriggerStay(Collider other) {
        timer--; ;
        if (other.tag == "Enemy") {
            transform.LookAt(other.transform);
            if (timer <= 0f)
                Fire(other.transform);
        }
    }

    void Fire (Transform target) {
        timer = 1f;
        linePosition = barrel.position;
        shootray.origin = linePosition;
        shootray.direction = Vector3.forward;

        hitLine.enabled = true;
        hitLine.SetPosition(0, linePosition);

        if (Physics.Raycast(shootray, out shootHit, range, shootableMask)) {
            damage damage = shootHit.collider.GetComponent<damage>();

            if (damage != null) {
                damage.TakeDamage(damagePerHit);
                Debug.Log("Turret: Damage Sent");
            }

            hitLine.SetPosition(1, target.position);
        }

    }
}
