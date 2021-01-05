using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage, int bullet)
    {
        //the bullet is nessecary for later upgrades like magic bullets etc..
        health -= damage;

        if (health > 0)
        {
            try
            {
                gameObject.GetComponent<Inventory>().takeDamage(health);
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
            gameObject.GetComponent<Inventory>().die();
        }
    }
}
