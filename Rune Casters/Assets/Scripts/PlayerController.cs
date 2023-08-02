using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick movementJoystick;
    public Joystick rotationJoystick;

    private float horizontalMovement = 0;
    private float verticalMovement = 0;

    public Transform firePoint;
    public GameObject currentSpell;
    private bool canAttack = true;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
        GameObject newSpell = Instantiate(currentSpell, firePoint.position, firePoint.rotation);
        newSpell.GetComponent<Projectile>().SetValues();
    }

    private IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(1);
        canAttack = true;
    }
}
