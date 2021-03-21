using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BlockHealthScript : MonoBehaviour
{
    TextMeshPro healthText;
    public int health = 6;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponentInChildren<TextMeshPro>();
        SetHealthText();
    }

    private void SetHealthText()
    {
        healthText.text = health.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            DecreaseHealth();
        }
    }

    private void DecreaseHealth()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            SetHealthText();
        }
    }
}
