using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private int _points = 1;
    [SerializeField] private List<AudioClip> _dying = new List<AudioClip>();
    [SerializeField] private GameObject _beerPrefab = default;

    private Player _player;
    private Animator _anim;
    private int _lifePoints = 1;
    bool mouvementDroite;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _anim = GetComponent<Animator>();
        mouvementDroite = false;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovements();
    }

    private void EnemyMovements()
    {
        if (transform.position.x > 8f)
        {
            mouvementDroite = false;

        }
        else if (transform.position.x < -8f)
        {
            mouvementDroite = true;

        }
        
        if(mouvementDroite)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
            _anim.SetBool("RunLeft", false);
            _anim.SetBool("Run", true);
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
            _anim.SetBool("RunLeft", true);
            _anim.SetBool("Run", false);
        }
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _player.Damage();
        }

        if (collision.tag == "PlayerAttack")
        {
            Damage();
        }
    }

    public void Damage()
    {
        _lifePoints--;

        int random = Random.Range(0, 2);
        AudioSource.PlayClipAtPoint(_dying[random], Camera.main.transform.position, 0.2f);

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.IncreaseScore(_points);

        if (_lifePoints < 1)
        {
            Destroy(gameObject);
            gameManager.SceneChanging();

            int beer = Random.Range(0, 10);

            if(beer == 5)
            {
                Instantiate(_beerPrefab, transform.position, Quaternion.identity);
            }
        }
    }



    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            UIManager uiManager = FindObjectOfType<UIManager>();
            uiManager.AjouterScore(_points);

            Destroy(collision.gameObject);
            EnemyDestruction();
        }

        if (collision.tag == "Player")
        {
            _player.Damage();
            EnemyDestruction();
        }
    }

    private void EnemyDestruction()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    */
}
