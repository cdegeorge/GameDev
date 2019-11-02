using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public int damagePerHit = 50;
    public float timeBetweenHits = 1f;
    public float range = 50f;
    public GameObject barrel;

    float timer;
    Ray shootray;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer hitLine;
    Vector3 linePosition;

    private void Awake() {
        shootableMask = LayerMask.GetMask("Shootable");
        hitLine = GetComponent<LineRenderer>();
    }

    private void Update() {
        timer += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Enemy") {
            transform.LookAt(other.transform);
            Fire(other.transform);
        }
    }

    void Fire (Transform target) {
        timer = 0f;
        linePosition = barrel.transform.position;
        shootray.origin = linePosition;
        shootray.direction = target.transform.position;

        hitLine.enabled = true;
        hitLine.SetPosition(0, linePosition);

        if (Physics.Raycast(shootray, out shootHit, range, shootableMask)) {
            damage damage = shootHit.collider.GetComponent<damage>();

            if (damage != null) {
                damage.TakeDamage(damagePerHit);
                Debug.Log("Turret: Damage Sent");
            }

            hitLine.SetPosition(1, shootHit.point);
        }
    }
}
