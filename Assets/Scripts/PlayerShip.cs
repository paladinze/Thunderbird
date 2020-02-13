using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float bulletSpeed = 10f;
    float bulletFiringStopPeriod = 0.1f;
    [SerializeField] GameObject mainWeaponPrefab;
    float minX;
    float maxX;
    float minY;
    float maxY;
    private float boundaryPadding;
    private Coroutine fireCoroutine;

    void Start() {
        setMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetButtonDown("Fire1") && fireCoroutine == null) {
            fireCoroutine = StartCoroutine(RepeatFire());
        }
        if (Input.GetButtonUp("Fire1") && fireCoroutine != null) {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    private void FireSingleBullet()
    {
        float shipTipYPos = GetComponent<Renderer>().bounds.max.y;
        Vector2 bulletPos = new Vector2(transform.position.x, shipTipYPos);
        GameObject bullet = Instantiate(
            mainWeaponPrefab, bulletPos, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
    }

    private void setMoveBoundaries() {
        boundaryPadding = GetComponent<Renderer>().bounds.extents.x;
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + boundaryPadding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - boundaryPadding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + boundaryPadding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - boundaryPadding;
    }

    private void Move() {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float newXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private IEnumerator RepeatFire() {
        while (true) {
            FireSingleBullet();
            yield return new WaitForSeconds(bulletFiringStopPeriod);
        }
    }
}
