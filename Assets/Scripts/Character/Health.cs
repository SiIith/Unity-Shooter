using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float startHealth = 5;
    public Image bloodBar;

    private float currentHealth;

    private void OnEnable()
    {
        currentHealth = startHealth;
        if(bloodBar!=null)
            bloodBar.fillAmount = (float)currentHealth/(float)startHealth; 
    }

    public void TakeDamage(float DmgAmount)
    {
        currentHealth -= DmgAmount;
        bloodBar.fillAmount = (float)currentHealth/(float)startHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void AddBlood(int addAmount){
        if(currentHealth < startHealth){
            currentHealth += addAmount;
            bloodBar.fillAmount = (float)currentHealth/(float)startHealth;
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
        // if player dies, unlock cursor and goes to end menu
        if (this.tag == "Player"){
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("EndMenu");
        }
    }

    public float getCurrentHealth(){
        return currentHealth;
    }

}


