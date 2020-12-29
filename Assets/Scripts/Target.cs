
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 10f;

    public void TakeDamage (float amount, int bullet)
    {
        health -= amount;

        //check which bullet hit
        if(bullet = 1)
        {

        }

        if (health <= 0f)
        {
            die(); 
        }
    }

    void die()
    {
        //code für sterben
        //respawn funktion
    }
}
