using System.Collections;
using System.Collections.Generic;
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
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // mouse is over the card
        if (hit.collider != null && hit.transform.gameObject.name == this.name)
        {
            if (!audioPlayed)
            {
                audioPlayer.Play();
                audioPlayed = true;
            }

            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
            transform.localScale = new Vector2(1.2f, 1.2f);
        }

        // otherwise it's not played
        else if (hit.collider == null || hit.transform.gameObject.name != this.name)
        {
            audioPlayed = false;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            transform.localScale = new Vector2(1, 1);
        }

        //transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(1, 1), Time.deltaTime * 20);
    }

    protected abstract void Action(Ship ship);

    protected void OnMouseDown()
    {
        if (!WasPlayed)
        {
            // do stuff
            WasPlayed = true;
            gameManager.availableCardSlots[handIndex] = true;
            gameManager.PerformShipAction(Action);
            gameObject.SetActive(false);
        }
    }
}
