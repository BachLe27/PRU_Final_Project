
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth;
    public int currentHealth;
    public int attckDamage;
    public TextMeshProUGUI levelUpText;

    public float moveSpeed = 5f;

    public BarController healthBar;
    public BarController expBar;

    Animator animator;
    bool takingDamage = false;
    public float damageCooldown = 1f;

    public Image gameOver; // Reference to the UI winning announcement
    public Text gameOverTxt;
    public Button restart;
    public Text restartTxt;

    public int currentExp = 0;
    public int currentLevel = 0;
    public int[] expForNextLevel =
    {
        100, 200, 300, 400, 500
    };

    // Movement
    Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;

    bool isFlashing = false;
    GameObject gun;
    public GameObject[] upgradedGunPrefab;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        gun = GameObject.FindGameObjectWithTag("Weapon");
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.setMaxValue(maxHealth);
        healthBar.setValue(currentHealth);
        expBar.setMaxValue(expForNextLevel[currentLevel]);
        expBar.setValue(currentExp);
        levelUpText.gameObject.SetActive(false);
        gameOver.enabled = false;
        gameOverTxt.enabled = false;
        restart.enabled = false;
        restartTxt.enabled = false;
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
        GunController gunController = gun.GetComponent<GunController>();
        AudioManager.Instance.PlaySFX("Shooting");
        gunController.Shoot();
    }

    void HandleMoving()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void TakeDamage(int damage)
    {
        var splash = gameObject.GetComponent<Splash>();
        StartCoroutine(splash.FlashRoutine());
        StartCoroutine(StartDamageCooldown());
        currentHealth -= damage;
        healthBar.setValue(currentHealth);
        // Start the flashing coroutine when taking damage.
    }

    void ReplaceGunWithUpgradedGun()
    {
        if (gun != null && upgradedGunPrefab != null)
        {
            Vector3 gunPosition = gun.transform.position;
            Quaternion gunRotation = gun.transform.rotation;

            // Instantiate the upgraded gun in the same position and rotation as the previous gun.
            gun = Instantiate(upgradedGunPrefab[currentLevel], gunPosition, gunRotation);
            gun.transform.parent = GameObject.FindGameObjectWithTag("PlayerWeapon").transform;
        }
    }

    void LevelUp()
    {
        currentLevel += 1;
        maxHealth += 300;
        currentHealth += 200;
        currentExp = 0;
        healthBar.setMaxValue(maxHealth);
        healthBar.setValue(currentHealth);
        expBar.setMaxValue(expForNextLevel[currentLevel]);
        expBar.setValue(currentExp);
        if (levelUpText != null)
        {
            levelUpText.text = "Level Up!";
            levelUpText.gameObject.SetActive(true);
            StartCoroutine(HideLevelUpText());
        }
        ReplaceGunWithUpgradedGun();
    }

    IEnumerator HideLevelUpText()
    {
        yield return new WaitForSeconds(1.5f); // Adjust the duration as needed
        levelUpText.gameObject.SetActive(false);
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
        if (currentExp >= expForNextLevel[currentLevel] && currentLevel < expForNextLevel.Length)
        {
            LevelUp();
        }
        if (levelUpText.gameObject.activeInHierarchy == true)
        {
            levelUpText.gameObject.transform.rotation = Camera.main.transform.rotation;
            levelUpText.gameObject.transform.position = gameObject.transform.position + new Vector3(0, 2, 0);
        }
        if (currentHealth < 0)
        {
            Destroy(gameObject);
            //make game stop

            gameOver.enabled = true;
            gameOverTxt.enabled = true;
            restart.enabled = true;
            restartTxt.enabled = true;
            
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
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

    public void EarnXP(int xp)
    {
        currentExp += xp;
        expBar.setValue(currentExp);
        AudioManager.Instance.PlaySFX("Shooting");
    }

    public void EarnHP(int hp)
    {
        currentHealth += hp;
        healthBar.setValue(currentHealth);
    }

    IEnumerator StartDamageCooldown()
    {
        takingDamage = true;
        yield return new WaitForSeconds(damageCooldown);
        takingDamage = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            MonsterController monster = collision.gameObject.GetComponent<MonsterController>();
            if (monster != null && !takingDamage)
            {
                TakeDamage(monster.monsterDamage);
                StartCoroutine(StartDamageCooldown());
            }
        }
    }
}
