using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//This class is for implementing IDamageable interface and let Player, Enemies inherit from this class
//So as to not implement the same logic on each entity
public class DamageableEntity : MonoBehaviour, IDamageable
{
    [SerializeField] UnityEvent OnPlayerDeathEvent;
    [SerializeField] Image HealthBar;

    public float startingHealth = 10f;
    public float startingSpeed = 5f;
    public float startingDamage = 1f;
    public float maxHealth;
    public float maxSpeed;
    public float maxDamage;

    //Only derived classes can access this object
    public float currentHealth;
    protected bool dead;

    public event System.Action OnDeath;


    protected virtual void Start()
    {
        currentHealth = startingHealth;
        maxHealth = startingHealth;
        maxSpeed = startingSpeed;
        maxDamage = startingDamage;
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        currentHealth -= damage;
        HandleHealthChange();

        if (currentHealth <= 0 && !dead)
        {
            Die();
        }
    }    
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HandleHealthChange();

        if (currentHealth <= 0 && !dead)
        {
            if (gameObject.tag == "Player")
            {
                PlayerDeath();

            }
            else
            {
                Die();

            }
        }
    }

    protected void Die()
    {
        dead = true;
        //Call the ondeath event
        if (OnDeath != null)
        {
            OnDeath();
        }
 

        //Could replace this logic with object Pooling??
        GameObject.Destroy(gameObject);
    }

    public void PlayerDeath()
    {
        dead = true;
        OnPlayerDeathEvent.Invoke();
        //Open Menu
    }

    public void HandleHealthChange()
    {
        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        HealthBar.fillAmount = currentHealthPct;
    }
}
