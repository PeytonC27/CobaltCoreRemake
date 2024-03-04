using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    protected AudioSource audioPlayer;
    protected GameManager gameManager;
    protected bool audioPlayed = false;
    
    public bool WasPlayed { get; private set; }
    public int handIndex;


    private void Start()
    {
        audioPlayer = GameObject.FindGameObjectWithTag("SFXPlayer").GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        WasPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

        // if many or one hits were found
        if (hits.Length > 0)
        {
            RaycastHit2D hit;
            if (hits.Length == 1)
                hit = hits[0];
            else
                hit = FindClosestHit(hits);

            // mouse is over the card
            if (hits.Length != 0 && hit.transform.gameObject.name == this.name )
                AddHighlight();
            else
                RemoveHighlight();
        }

        // otherwise it's not played
        else
            RemoveHighlight();
    }

    public abstract void Action(Ship ship);

    protected void OnMouseDown()
    {
        if (!WasPlayed)
        {
            // do stuff
            WasPlayed = true;
            gameObject.SetActive(false);
            gameManager.OnCardPlayed(this);
        }
    }

    RaycastHit2D FindClosestHit(RaycastHit2D[] hits)
    {
        RaycastHit2D closestHit = hits[0];
        float closestDistance = Mathf.Abs(Camera.main.ScreenToWorldPoint(Input.mousePosition).z - closestHit.transform.position.z);

        for (int i = 1; i < hits.Length; i++)
        {
            float distance = Mathf.Abs(Camera.main.ScreenToWorldPoint(Input.mousePosition).z - hits[i].transform.position.z);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHit = hits[i];
            }
        }

        return closestHit;
    }
    
    void AddHighlight()
    {
        if (!audioPlayed)
        {
            audioPlayer.Play();
            audioPlayed = true;
        }

        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 999;
        transform.localScale = new Vector2(1.1f, 1.1f);

        // move the card up so its hitbox has precedence
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
    }

    void RemoveHighlight()
    {
        audioPlayed = false;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = handIndex;
        transform.localScale = new Vector2(1, 1);

        // move hitbox back
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
