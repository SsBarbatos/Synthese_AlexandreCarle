using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private int ctr;

    GameManager gameManager;
    SpawnManager spawnManager;
    Enemy enemy;

    [SerializeField] private GameObject _port = default;


    // Start is called before the first frame update
    void Start()
    {
        ctr = 0;

        gameManager = FindObjectOfType<GameManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
        enemy = FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "PlayerAttack")
        {
            if(ctr == 0)
            {
                ctr++;

                gameManager.Score10();

                collision.transform.position = new Vector3(-7.996f, -3.4f, 0f);

                enemy.SetSpeed(2f);
                spawnManager.StartSpawn();

                _port.SetActive(false);
            }
            else if (ctr == 1)
            {
                ctr++;

                gameManager.Score25();

                collision.transform.position = new Vector3(-7.996f, -3.4f, 0f);

                enemy.SetSpeed(1.5f);
                spawnManager.StartSpawn();

                _port.SetActive(false);
            }
            else if (ctr == 2)
            {
                gameManager.Score50();

                collision.transform.position = new Vector3(-7.996f, -3.4f, 0f);

                enemy.SetSpeed(1f);
                spawnManager.StartSpawn();

                _port.SetActive(false);
            }
        }
    }
}
