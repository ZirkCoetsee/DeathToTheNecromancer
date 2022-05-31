using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform pointOfOrigin;
    public Projectile projectile;
    public float msBetweenShots = 100f;
    public float pointOfOriginVelocity = 40f;

    float nextShotTime;

    public void Shoot()
    {
        //Stops user from shooting every milisecond
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000f;
            Projectile newProjectile = Instantiate(projectile, pointOfOrigin.position, pointOfOrigin.rotation) as Projectile;
            newProjectile.SetSpeed(pointOfOriginVelocity);
        }
    }
}
