using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera Cam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire()
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            //call damage on player
        }
    }
}
