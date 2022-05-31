using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    float speed = 10f;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
 
    // Update is called once per frame
    void Update()
    {
        //Check collision with ray
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position,transform.forward);
        RaycastHit hit;

        //Use ray to check if ray collides with trigger colliders
        if (Physics.Raycast(ray, out hit,moveDistance,collisionMask,QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    private void OnHitObject(RaycastHit hit)
    {
        Debug.Log("Hit");
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        DamageableEntity damageableEntity = hit.collider.GetComponent<DamageableEntity>();
        if (damageableObject != null)
        {

            damageableObject.TakeHit(damageableEntity.maxDamage, hit);
        }
        GameObject.Destroy(gameObject);
    }
}
