using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _txtScore = default;

    [SerializeField] private Image _livesDisplayImage = default;
    [SerializeField] private Sprite[] _liveSprites = default;

    [SerializeField] private GameObject _pausePanel = default;
    [SerializeField] private GameObject _rulesPanel = default;

    GameManager gameManager;

    private int _score = default;
    private bool _pauseOn = false;
    private bool _rulesOn = false;

    // Start is called before the first frame update

    private void Start()
    {
        _score = 0;

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        UpdateScore();
        PauseMenu();
    }

    private void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_pauseOn)
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
            _pauseOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _pauseOn)
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
            _pauseOn = false;
        }
    }

    public void ResumeGame()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        _pauseOn = false;
    }

    public void RulesMenu()
    {
        if (_rulesOn)
        {
            _rulesPanel.SetActive(false);
            _rulesOn = false;
        }
        else
        {
            _rulesPanel.SetActive(true);
            _rulesOn = true;
        }    
    }

    public void UpdateScore()
    {
        _score = gameManager.GetScore();
        _txtScore.text = "Ennemies tues   " + _score.ToString();
    }

    public void ChangeLivesDisplayImage(int noImage)
    {
        if (noImage < 0)
        {
            noImage = 0;
        }

        _livesDisplayImage.sprite = _liveSprites[noImage];

        if (noImage == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        SceneManager.LoadScene(2);

        gameManager.EndGameAudioClip();
    }
}
