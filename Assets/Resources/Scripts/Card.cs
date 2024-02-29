using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Card : MonoBehaviour
{
    AudioSource audioPlayer;
    GameManager gameManager;

    bool audioPlayed = false;
    
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
        }

        // otherwise it's not played
        else if (hit.collider == null || hit.transform.gameObject.name != this.name)
        {
            audioPlayed = false;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }

    private void OnMouseDown()
    {
        if (!WasPlayed)
        {
            // do stuff
            WasPlayed = true;
            gameManager.availableCardSlots[handIndex] = true;
            gameObject.SetActive(false);
        }
    }
}
