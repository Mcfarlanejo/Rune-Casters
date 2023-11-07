using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public Vector3 aimVelocity;

    private float horizontalMovement = 0;
    private float verticalMovement = 0;

    public Transform firePoint;
    public Transform rotationBeam;
    public GameObject projectilePrefab;
    public GameObject aoePrefab;
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
    public TMP_Text countdownAOE;
    public float aoeTimer = 0;

    public SelfSpell activeSelfSpell;
    public Button selfButton;
    public TMP_Text countdownSelf;
    public float selfTimer = 0;
    public TMP_Text selfBuffText;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileOne = (ProjectileSpell)spells[0];
        activeProjectile = projectileOne;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = movementJoystick.Horizontal * 5;
        verticalMovement = movementJoystick.Vertical * 5;

        if (rotationJoystick.Horizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(movementJoystick.Horizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        rb.velocity = new Vector2(horizontalMovement, verticalMovement);

        float rotationAngle = Mathf.Atan2(rotationJoystick.Horizontal, rotationJoystick.Vertical) * Mathf.Rad2Deg;
        rotationBeam.transform.rotation = Quaternion.Euler(0, 0, -rotationAngle);

        if (rotationJoystick.Horizontal != 0 || rotationJoystick.Vertical != 0)
        {
            if (canAttack)
            {
                canAttack = false;
                Fire();
                AudioManager.instance.spellCast.Play();
                StartCoroutine(FireDelay());
            }
        }

        if (aoeTimer != 0)
        {
            aoeTimer -= Time.deltaTime;

            countdownAOE.text = Convert.ToInt32(aoeTimer).ToString();
        }
        if (selfTimer != 0)
        {
            selfTimer -= Time.deltaTime;
            
            countdownSelf.text = Convert.ToInt32(selfTimer).ToString();
        }
    }

    private void Fire()
    {
        GameObject newSpell = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        newSpell.GetComponent<Projectile>().spell = activeProjectile;
        newSpell.GetComponent<Projectile>().parent = gameObject;
    }

    private IEnumerator FireDelay()
    {
        //Debug.Log((float)(100 - (activeProjectile.castDelay + PlayerStats.instance.castSpeed.GetValue()) * 1 + PlayerStats.instance.castSpeedPercentage.GetValue()) / 100);
        yield return new WaitForSeconds((float)(100 - (activeProjectile.castDelay + PlayerStats.instance.castSpeed.GetValue()) * 1 + PlayerStats.instance.castSpeedPercentage.GetValue())/100);
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
        if (projectileTwo != null)
        {
            activeProjectile = projectileTwo;
            projectileOneButton.gameObject.GetComponent<Image>().enabled = false;
            projectileTwoButton.gameObject.GetComponent<Image>().enabled = true;
        }
    }

    public void CastAOE()
    {
        if (activeAOE != null) 
        { 
            countdownAOE.gameObject.SetActive(true);
            
            GameObject newSpell = Instantiate(aoePrefab, gameObject.transform.position, Quaternion.identity);
            newSpell.GetComponent<AOE>().spell = activeAOE;
            newSpell.GetComponent<AOE>().parent = gameObject;
            aoeTimer = 60 - newSpell.GetComponent<AOE>().spell.castDelay;

            AudioManager.instance.spellCast.Play();
            StartCoroutine(AOEDelay(activeAOE.castDelay));
        }
    }

    public IEnumerator AOEDelay(float delay)
    {
        aoeButton.interactable = false;
        yield return new WaitForSeconds(60 - delay);
        aoeButton.interactable = true;
        aoeTimer = 0;
        countdownAOE.gameObject.SetActive(false);
    }

    public void CastSelf()
    {
        if (activeSelfSpell != null)
        {
            countdownSelf.gameObject.SetActive(true);
            selfBuffText.gameObject.SetActive(true);
            string temp = "";
            switch (activeSelfSpell.element)
            {
                case Element.Fire:
                    temp = "DMG UP";
                    break;
                case Element.Earth:
                    temp = "DEF UP";
                    break;
                case Element.Water:
                    temp = "C-SPD UP";
                    break;
                case Element.Wind:
                    temp = "SPD UP";
                    break;
            }
            selfBuffText.text = temp;

            PlayerStats.instance.AddTempStats(activeSelfSpell);
            selfTimer = 60 - activeSelfSpell.castDelay;

            AudioManager.instance.spellCast.Play();
            StartCoroutine(SelfDelay(activeSelfSpell.castDelay));
        }
    }

    public IEnumerator SelfDelay(float delay)
    {
        selfButton.interactable = false;
        yield return new WaitForSeconds(60 - delay);
        selfButton.interactable = true;
        selfTimer = 0;
        countdownSelf.gameObject.SetActive(false);
        selfBuffText.gameObject.SetActive(false);
    }
}
