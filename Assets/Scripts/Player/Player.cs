using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth;
    public int health;
    public int attckDamage;
    public float moveSpeed = 5f;

    public Transform firePoint;
    Animator animator;

    // Movement
    Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;

    // Bullet
    public GameObject bulletPrefab;
    public float bulletForce = 20f;


    GameObject gun;
    public GameObject upgradedGunPrefab;

    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Weapon");
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Shooting", true);
            Shoot();
        }
        if (Input.GetMouseButtonDown(1))
        {
            LevelUp();
        }
    }

    void Shoot()
    {
        //Ray2D ray = new Ray2D(firePoint.position, firePoint.up);
        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        //if (hit.collider != null)
        //{
        //    GameObject hitObject = hit.collider.gameObject;
        //    MonsterController monster = hitObject.GetComponent<MonsterController>();
        //    PlayerController player = new PlayerController();
        //    if (monster != null)
        //    {
        //        //Destroy(hitObject.GameObject());
        //        monster.TakeDamage(player.attackDamage);
        //    }
        //}

        GunController gunController = gun.GetComponent<GunController>();
        gunController.Shoot();

        //GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    void HandleMoving()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void ReplaceGunWithUpgradedGun()
    {
        if (gun != null && upgradedGunPrefab != null)
        {
            Vector3 gunPosition = gun.transform.position;
            Quaternion gunRotation = gun.transform.rotation;

            Destroy(gun); // Destroy the current gun.

            // Instantiate the upgraded gun in the same position and rotation as the previous gun.
            gun = Instantiate(upgradedGunPrefab, gunPosition, gunRotation);
            gun.transform.parent = GameObject.FindGameObjectWithTag("PlayerWeapon").transform;
        }
    }

    void LevelUp()
    {
        ReplaceGunWithUpgradedGun();
    }


    void HandleRotationByMouse()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMoving();
        HandleShooting();
    }

    private void FixedUpdate()
    {
        HandleRotationByMouse();
    }
    private void LateUpdate()
    {
        if (animator.GetBool("Shooting"))
        {
            animator.SetBool("Shooting", false);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Take Damage!");
        health -= damage;
        if (health < 0)
        {
            Destroy(gameObject);            
        }
    }
}
