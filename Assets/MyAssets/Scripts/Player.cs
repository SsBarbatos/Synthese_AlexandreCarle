using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _anim;
    private float _movementSpeed = 5f;
    float posX;
    private bool attackBlocked = false;
    private int _lifePoints = 3;

    [SerializeField] private AudioClip _drinking = default;
    [SerializeField] private AudioClip _sword = default;

    UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovements();
        Attack();
    }

    private void PlayerMovements()
    {
        Vector3 direction = new Vector3(posX, 0f, 0f);
        posX = Input.GetAxis("Horizontal");

        transform.Translate(direction * Time.deltaTime * _movementSpeed);
        
        if (posX < 0f)
        {
            _anim.SetBool("RunLeft", true);
            _anim.SetBool("RunRight", false);
            _anim.SetBool("Attack", false);
        }
        else if (posX > 0f)
        {
            _anim.SetBool("RunLeft", false);
            _anim.SetBool("RunRight", true);
            _anim.SetBool("Attack", false);
        }
        else
        {
            _anim.SetBool("RunLeft", false);
            _anim.SetBool("RunRight", false);
            _anim.SetBool("Attack", false);
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            Vector3 jump = new Vector3(0f, 0.5f, 0f);
            transform.Translate(jump * Time.deltaTime * 2000);

            _anim.SetBool("Jump", true);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.9f, 1f), 0f);

        if (transform.position.x >= 9)
        {
            transform.position = new Vector3(-9f, transform.position.y, 0f);
        }
        else if (transform.position.x <= -9)
        {
            transform.position = new Vector3(9f, transform.position.y, 0f);
        }
    }

    private void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Input.inputString != "")
        {
            if (attackBlocked)
                return;
            else
            {
                if (posX < 0f)
                {
                    _anim.SetBool("AttackLeft", true);
                }
                else
                {
                    _anim.SetBool("Attack", true);
                }

                transform.tag = "PlayerAttack";
                AudioSource.PlayClipAtPoint(_sword, Camera.main.transform.position, 0.1f);
                attackBlocked = true;
            }

            StartCoroutine(DelayAttack());
        }
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.3f);
        attackBlocked = false;

        yield return new WaitForSeconds(0.2f);
        transform.tag = "Player";
    }

    public void Damage()
    {
        _lifePoints--;

        uiManager.ChangeLivesDisplayImage(_lifePoints);

        if (_lifePoints < 1)
        {
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            spawnManager.StopSpawn();

            Destroy(gameObject);
        }
    }

    public void Heal()
    {
        AudioSource.PlayClipAtPoint(_drinking, Camera.main.transform.position, 1.5f);

        if (_lifePoints == 3)
        {
            return;
        }
        else
        {
            _lifePoints++;
            uiManager.ChangeLivesDisplayImage(_lifePoints);
        }
    }
}
