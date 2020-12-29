using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10f;
    public float distance = 100f;
    public int bullet = 1; //1= default,

    public Camera Cam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Fire1 is the left mouse button
        {
            Fire();
        }
    }

    void Fire()
    {
        Debug.Log("Shot");
        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, distance))
        {
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage, bullet);
            }
        }
    }
}
