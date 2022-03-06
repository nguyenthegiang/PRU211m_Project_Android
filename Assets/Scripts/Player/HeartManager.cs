using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    //maximum number of health, set in Unity Editor
    [SerializeField]
    public int maxHealth;
    [SerializeField]
    public int numOfHearts;

    public GameObject[] hearts;
    [SerializeField]
    public Sprite fullHealth;
    [SerializeField]
    public Sprite emptyHealth;

    public int health;

    private void Start()
    {
        health = maxHealth;
        numOfHearts = maxHealth;
    }

    //Change the heart animation
    public void ChangeHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].GetComponent<SpriteRenderer>().enabled = true;
                //hearts[i].GetComponent<SpriteRenderer>().sprite = fullHealth;
            }
            else
            {
                //hearts[i].GetComponent<SpriteRenderer>().sprite = emptyHealth;
                hearts[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    //minus health by 1
    public void MinusHeart()
    {
        health--;
        ChangeHearts();
    }

    //restore full health
    public void RestoreHealth()
    {
        health = maxHealth;
        ChangeHearts();
    }
}
