using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;
    public static Heart instance;
    private void Awake()
    {
        instance = this;
    }

    public void ChangeHealth(int healthChange)
    {
        Debug.Log($"{health} is the new amount");
        health += healthChange;
        if(health <= 0)
        {
            TheDirector.instance.UpdateGameState(TheDirector.GameState.Defeat);
        }
    }
    public void RestartGameForHeart()
    {
        health = maxHealth;
    }
    
}
