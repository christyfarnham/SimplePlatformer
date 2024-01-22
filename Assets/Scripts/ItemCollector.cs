using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    public int numCoins = 0;
    public static ItemCollector instance;

    private PlayerMovement playerMovement;

    public ParticleSystem collectParticle;

    [SerializeField] public Text coinsText;

    void awake()
    {
        instance = this;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        numCoins++;
        coinsText.text = "Coins: " + numCoins;
        collectParticle.Play();
    }

    public void Update()
    {
        instance = this;
    }

    public void UpdateScore()
    {
        playerMovement.numCoins = numCoins + 1;
        Debug.Log("Playermovement num = " + playerMovement.numCoins);
        playerMovement.ChangeScore(playerMovement.numCoins);
    }
    
}
