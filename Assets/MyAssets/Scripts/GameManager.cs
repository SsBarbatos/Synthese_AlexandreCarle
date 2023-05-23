using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _background = default;
    [SerializeField] private List<Sprite> _backgroundsList = new List<Sprite>();
    private SpriteRenderer _backgroundSprite = default;

    private int _score;

    UIManager uiManager;
    SpawnManager spawnManager;

    [SerializeField] private GameObject _portal = default;

    private AudioSource music;
    [SerializeField] private List<AudioClip> _backgroundMusic = new List<AudioClip>();

    private void Awake()
    {
        int managerNbr = FindObjectsOfType<GameManager>().Length;

        if (managerNbr > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        spawnManager = FindObjectOfType<SpawnManager>();

        _backgroundSprite = _background.GetComponent<SpriteRenderer>();

        music = GetComponent<AudioSource>();
        music.clip = _backgroundMusic[0];
        music.Play();
        music.volume = 0.3f;

        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseScore(int points)
    {
        _score += points;
        uiManager.UpdateScore();
    }

    public int GetScore()
    {
        return _score;
    }

    public void SceneChanging()
    {
        if(_score == 10)
        {
            spawnManager.StopSpawn();
            _portal.SetActive(true);
        }
        else if (_score == 25)
        {
            spawnManager.StopSpawn();
            _portal.SetActive(true);
        }
        else if (_score == 50)
        {
            spawnManager.StopSpawn();
            _portal.SetActive(true);
        }
    }

    public void Score10()
    {
        _backgroundSprite.sprite = _backgroundsList[1];
        _background.transform.localScale = new Vector2(1.8f, 1.497139f);

        spawnManager.SetSpawnDelay(2f);

        music.Stop();
        music.clip = _backgroundMusic[1];
        music.Play();
        music.volume = 0.5f;
    }

    public void Score25()
    {
        _backgroundSprite.sprite = _backgroundsList[2];
        _background.transform.localScale = new Vector2(3f, 2.527771f);

        spawnManager.SetSpawnDelay(1.5f);

        music.Stop();
        music.clip = _backgroundMusic[2];
        music.Play();
        music.volume = 1f;
    }

    public void Score50()
    {
        _backgroundSprite.sprite = _backgroundsList[3];
        _background.transform.localScale = new Vector2(0.9425075f, 0.974074f);

        spawnManager.SetSpawnDelay(1f);

        music.Stop();
        music.clip = _backgroundMusic[3];
        music.Play();
        music.volume = 0.6f;
    }

    public void AudioStop()
    {
        music.Stop();
    }

    public void AudioStart()
    {
        music.Play();
    }

    public void EndGameAudioClip()
    {
        music.clip = _backgroundMusic[4];
    }
}
