using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private float initialHealth = 300;
    private float currentHealth;

    [SerializeField] private HealthBar healthBar;
    private Entity entity;

    private void Awake()
    {
        ResetHealth();
    }

    private void Start()
    {
        entity = GameObject.FindObjectOfType<Entity>();
    }

    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        float percentage = (currentHealth / initialHealth);
        Debug.Log(percentage);
        healthBar.UpdateBar(percentage);
        if(currentHealth <= 0)
        {
            End();
        }
    }

    private void ResetHealth()
    {
        currentHealth = initialHealth;
    }

    private void End()
    {
        if (entity.isEnemy)
        {
            SceneManager.LoadScene("Defeat");
        }
        else
        {
            int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
            int levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);
            if (currentLevel >= levelsUnlocked)
            {
                PlayerPrefs.SetInt("LevelsUnlocked", currentLevel + 1);
                PlayerPrefs.Save();
            }
            SceneManager.LoadScene("Victory");
        }
    }
}