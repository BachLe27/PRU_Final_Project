using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    Animator animator;
    public float bulletForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetButtonDown("Fire1"))
         {
            animator.SetBool("Shooting", true);
            Shoot();
        }
    }

    private void LateUpdate()
    {
        animator.SetBool("Shooting", false);
    }

    public void OnShootingDone()
    {
        
    }

    void Shoot()
    {
        GameObject bullet =  Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.up *  bulletForce, ForceMode2D.Impulse);
    }
}
