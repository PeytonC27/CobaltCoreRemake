using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] ShipComponent[] shipParts;
    [SerializeField] int maxHealth;
    InputManager input;
    protected int health;
    public int moves;
    public int damageBuff;

    protected Vector3 newLocation;
    protected HealthBar healthBar;
    protected TMP_Text moveDisplay;
    protected LineDrawer lineDrawer;

    private void Start()
    {
        input = GetComponent<InputManager>();
        healthBar = GetComponentInChildren<HealthBar>();
        moveDisplay = gameObject.transform.Find("EvasionDisplay").GetComponentInChildren<TMP_Text>();

        lineDrawer = GameObject.FindGameObjectWithTag("LineDrawer").GetComponent<LineDrawer>();
        BuildShip();
        newLocation = transform.position;

        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        // movement
        if (input.PressingLeft && moves > 0)
            MoveShip(-1);
        else if (input.PressingRight && moves > 0)
            MoveShip(1);

        // move the ship's center
        transform.position = Vector3.Lerp(transform.position, newLocation, 20 * Time.deltaTime);

        // update text
        moveDisplay.text = moves.ToString();
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
        moves--;
    }

    public void Fire(int damage)
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
                    hit.transform.parent.gameObject.GetComponentInChildren<HealthBar>().TakeDamage(damage);

                    // drawing the effect
                    lineDrawer.Draw(gun.transform, hit.transform);

                    // playing the sound
                    gun.PlayGunSound();
                }
            }  
        }
    }

    public void CardAction(Card card)
    {
        Debug.Log(card.name + " was played!");
    }
}
