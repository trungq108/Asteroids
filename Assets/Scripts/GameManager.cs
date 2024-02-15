using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public float respawnTime = 2f;
    private int live = 2;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOver;

    private void Awake()
    {
        gameOver.gameObject.SetActive(false);
    }
    public void AsteroidDestroy(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();
        if(asteroid.size < 1.5)
        { score += 100; }
        else if (asteroid.size < 2.5)
        { score += 50; }
        else { score += 25; }
    }
    private void Update()
    {
        scoreText.text = score.ToString();

    }
    public void OnPlayerDead()
    {
        live--;
        explosion.transform.position = player.transform.position;
        explosion.Play();
        if (live <= 0)
        {
            GameOver();
            Invoke("NewGame", respawnTime);
        }
        else
        {
            Invoke("ReSpawn", respawnTime); 
        }
    }

    void ReSpawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.TurnoffCollision();
        player.Invoke("TurnOnCollision", 3f);
    }
    void GameOver()
    {
        gameOver.gameObject.SetActive(true);

    }
    void NewGame()
    {     
        live = 3;
        score = 0;      
        Invoke("ReSpawn", respawnTime);
        Invoke("GameOverText", respawnTime);
    }
    void GameOverText()
    {
        gameOver.gameObject.SetActive(false);
    }
}
