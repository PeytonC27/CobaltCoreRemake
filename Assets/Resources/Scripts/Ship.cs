using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] ShipComponent[] shipParts;
    [SerializeField] int maxHealth;
    InputManager input;
    protected int health;

    protected Vector3 newLocation;
    protected HealthBar healthBar;
    protected LineDrawer lineDrawer;

    private void Start()
    {
        input = GetComponent<InputManager>();
        healthBar = GetComponentInChildren<HealthBar>();
        lineDrawer = GameObject.FindGameObjectWithTag("LineDrawer").GetComponent<LineDrawer>();
        BuildShip();
        newLocation = transform.position;

        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (input.PressingLeft)
            MoveShip(-1);
        else if (input.PressingRight)
            MoveShip(1);

        if (input.LeftClick)
            Fire();

        // move the ship's center
        transform.position = Vector3.Lerp(transform.position, newLocation, 20 * Time.deltaTime);
    }

    protected void BuildShip()
    {
        int offset = shipParts.Length / 2;

        for (int i = 0; i < shipParts.Length; i++)
            shipParts[i] = Instantiate(
                shipParts[i], 
                new Vector2(transform.position.x - offset + i, transform.position.y), 
                Quaternion.identity, 
                transform
            );
    }

    protected void MoveShip(short shamt)
    {
        newLocation = newLocation + new Vector3(shamt, 0);
    }

    void Fire()
    {
        foreach (var part in shipParts)
        {
            RaycastHit2D hit = Physics2D.Raycast(part.transform.position + Vector3.up, Vector2.up, 6);

            if (part is Gun && hit.collider != null) 
            {
                if (hit.collider.tag == "ShipComponent")
                {
                    Gun gun = part as Gun;
                    Debug.DrawRay(part.transform.position + Vector3.up, Vector2.up * 6, Color.yellow, 1);
                    Debug.Log("Shot enemy!");

                    // have the enemy take damage
                    hit.transform.parent.gameObject.GetComponentInChildren<HealthBar>().TakeDamage(gun.GetDamage());

                    // drawing the effect
                    lineDrawer.Draw(gun.transform, hit.transform);
                }
            }  
        }
    }
}
