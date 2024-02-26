using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Card : MonoBehaviour
{
    AudioSource player;
    bool played = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("SFXPlayer").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // mouse is over the card
        if (hit.collider != null && hit.transform.gameObject.name == this.name && !played)
        {
            player.Play();
            played = true;
        }

        // otherwise it's not played
        else if (hit.collider == null || hit.transform.gameObject.name != this.name)
            played = false;
    }
}
