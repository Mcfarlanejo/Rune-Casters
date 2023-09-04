using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerController>();
            }
            return _instance;
        }
    }
    static PlayerController _instance;

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public Joystick movementJoystick;
    public Joystick rotationJoystick;

    private float horizontalMovement = 0;
    private float verticalMovement = 0;

    public Transform firePoint;
    public GameObject projectilePrefab;
    private bool canAttack = true;

    private Rigidbody2D rb;

    public List<Spell> spells = new List<Spell>();

    public ProjectileSpell activeProjectile;
    //These 2 are hotswappable at any time
    public ProjectileSpell projectileOne;
    public Button projectileOneButton;
    public ProjectileSpell projectileTwo;
    public Button projectileTwoButton;

    public AOESpell activeAOE;
    public Button aoeButton;
    public SelfSpell activeSelfSpell;
    public Button selfButton;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileOne = (ProjectileSpell)spells[0];
        activeProjectile = projectileOne;
    }

    // Update is called once per frame
    void Update()
    {
        projectilePrefab.GetComponent<Projectile>().spell = activeProjectile;

        horizontalMovement = movementJoystick.Horizontal * 5;
        verticalMovement = movementJoystick.Vertical * 5;

        rb.velocity = new Vector2(horizontalMovement, verticalMovement);

        float rotationAngle = Mathf.Atan2(rotationJoystick.Horizontal, rotationJoystick.Vertical) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -rotationAngle);

        if (rotationJoystick.Horizontal != 0 || rotationJoystick.Vertical != 0)
        {
            if (canAttack)
            {                
                canAttack = false;
                Fire();
                StartCoroutine(FireDelay());
            }
        }
    }

    private void Fire()
    {
        GameObject newSpell = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        newSpell.GetComponent<Projectile>().parent = gameObject;
    }

    private IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(.5f);
        canAttack = true;
    }

    public void SetProjectileOneActive()
    {
        activeProjectile = projectileOne;
        projectileOneButton.gameObject.GetComponent<Image>().enabled = true;
        projectileTwoButton.gameObject.GetComponent<Image>().enabled = false;
    }

    public void SetProjectileTwoActive()
    {
        activeProjectile = projectileTwo;
        projectileOneButton.gameObject.GetComponent<Image>().enabled = false;
        projectileTwoButton.gameObject.GetComponent<Image>().enabled = true;
    }

    public void CastAOE()
    {

    }

    public void CastSelf()
    {

    }
}
