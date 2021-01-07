using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public int damage = 10;
    public int range = 100;
    public int magazine = 5;
    public int bullets = 20;
    public int maxbullets = 20;
    public int impactForce = 30;
    public float fireRate = 15;

    public Camera Cam;
    public Inventory Inventory;

    private float nextTimeToFire = 0;
    private AudioSource mAudioSource;

    void Start()
    {
        mAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            mAudioSource.Play();
            nextTimeToFire = Time.time + 1f/fireRate;
            Fire();
        }
    }

    void Fire()
    {
        RaycastHit hit;
        if(magazine != 0)
        {
            bullets -= 1;
            if(bullets == 0 && magazine != 0)
            {
                bullets = maxbullets;
                magazine -= 1;
            }

            if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
            {
                PlayerHealth target = hit.transform.GetComponent<PlayerHealth>();
                if (target != null)
                {
                    target.TakeDamage(damage, 0);
                }

                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            }
            Inventory.updateAmmo(magazine, bullets);
        }
    }

    public void changedWeapon()
    {
        Inventory.updateAmmo(magazine, bullets);
    }
}
