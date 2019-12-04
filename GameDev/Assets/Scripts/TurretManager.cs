using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public int damagePerHit = 50;
    public float timeBetweenHits = 1f;
    public float range = 50f;
    public Transform barrel;
    public Light barrelLight;

    float timer = 0f;
    float effectDisplayTime = .14f;
    Ray shootray;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer hitLine;
    Vector3 linePosition;
    AudioSource audio;

    private void Awake() {
        shootableMask = LayerMask.GetMask("Shootable");
        hitLine = GetComponent<LineRenderer>();
        hitLine.enabled = false;
        barrelLight.enabled = false;
        audio = GetComponent<AudioSource>();
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= effectDisplayTime) {
            DisableEffects();
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Enemy") {
            transform.LookAt(other.transform);
            if (timer >= timeBetweenHits) {
                Fire(other.transform);         
            }
        }
    }

    void DisableEffects() {
        hitLine.enabled = false;
        barrelLight.enabled = false;
    }

    void Fire (Transform target) {
        timer = 0f;
        linePosition = barrel.position;
        shootray.origin = linePosition;
        shootray.direction = barrel.forward;

        if (Physics.Raycast(shootray, out shootHit, range, shootableMask)) {
            damage damage = shootHit.collider.GetComponent<damage>();

            if (damage != null) {
                damage.TakeDamage(damagePerHit);
            }
            hitLine.enabled = true;
            barrelLight.enabled = true;
            hitLine.SetPosition(0, linePosition);
            hitLine.SetPosition(1, shootHit.transform.position);
            audio.Play();
        } 

    }
}
