using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private GameObject _rulesPanel = default;

    private bool _rulesOn = false;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CloseGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(0);

            gameManager.AudioStop();

            Destroy(gameManager);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        gameManager.AudioStart();
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
}
