using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] AudioClip[] humanGrunts;
    [SerializeField] int health;
    [SerializeField] int maxHealth;
    [SerializeField] TextMeshProUGUI _health;
    public static Heart instance;
    private void Awake()
    {
        instance = this;
    }

    public void ChangeHealth(int healthChange)
    {
        if(health > 0)
        {
            health += healthChange;
            _health.text = _health.text = "Pylon Health: " + health.ToString();
            int ran = Random.Range(0, humanGrunts.Length);
            SoundManager.instance.PlaySoundFXClip(humanGrunts[ran], transform, .2f);
        }

        if(health <= 0)
        {
            TheDirector.instance.UpdateGameState(TheDirector.GameState.Defeat);
            _health.text = _health.text = "Pylon Health: 0";
        }
    }
    public void RestartGameForHeart()
    {
        _health.text = "Pylon Health: " + maxHealth.ToString();
        health = maxHealth;
    }
    
}
