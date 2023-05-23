using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private AudioClip _backgroundMusic = default;
    [SerializeField] private TextMeshProUGUI _txtScoreFinal = default;

    private int _score;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(_backgroundMusic, Camera.main.transform.position, 0.3f);

        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore()
    {
        _score = gameManager.GetScore();
    }

    public void EndGame()
    {
        _txtScoreFinal.text = "Vous avez tue " + _score + " enemies";
        StartCoroutine(GameOverBlinkRoutine());
    }

    IEnumerator GameOverBlinkRoutine()
    {
        while (true)
        {
            _txtScoreFinal.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            _txtScoreFinal.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }
    }
}
