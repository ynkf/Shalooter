using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    private Inventory inventory;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    public void TakeDamage(int damage, int bullet)
    {
        //the bullet is nessecary for later upgrades like magic bullets etc..
        health -= damage;

        if (health > 0)
        {
            try
            {
                inventory.takeDamage(health);                
            }
            catch
            {

            }
        }

        else
        {
            die();
        }

    }

    void die()
    {
        if(gameObject.name == "destroyable")
        {
            Destroy(gameObject);
        }

        else
        {
            //do code what happen when he dies (respawn etc..)
            //call UI function
            inventory.die();
            
        }
    }
}
