using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    GameOver gameOver;
    [SerializeField] GameObject canvasObject;
    public float maxHealth;
    public float currentHealth;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gameOver = canvasObject.GetComponent<GameOver>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            gameOver.Show();
            gameOver.RunWaitForXSeconds(5);
        }
    }
}
