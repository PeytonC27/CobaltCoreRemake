using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyShip : Ship
{
    private void Start()
    {
        lineDrawer = GameObject.FindGameObjectWithTag("LineDrawer").GetComponent<LineDrawer>();
        BuildShip();
        newLocation = transform.position;
    }

    private void Update()
    {
        // move the ship's center
        transform.position = Vector3.Lerp(transform.position, newLocation, 20 * Time.deltaTime);
    }

    public void Move(short amount)
    {
        MoveShip((short)Mathf.Min(amount, Mathf.Sign(amount)*3));
    }

    public IEnumerator FireCannons(int damage)
    {
        foreach (var part in shipParts)
        {
            RaycastHit2D hit = Physics2D.Raycast(part.transform.position + Vector3.down, Vector2.down, 6);

            if (part is Gun && hit.collider != null)
            {
                if (hit.collider.tag == "ShipComponent")
                {
                    Gun gun = part as Gun;
                    Debug.DrawRay(part.transform.position + Vector3.down, Vector2.down * 6, Color.yellow, 1);
                    Debug.Log("Shot enemy!");

                    // have the enemy take damage
                    hit.transform.parent.gameObject.GetComponentInChildren<HealthBar>().TakeDamage(damage);

                    // drawing the effect
                    lineDrawer.Draw(gun.transform, hit.transform);

                    // playing the sound
                    gun.PlayGunSound();

                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
}
