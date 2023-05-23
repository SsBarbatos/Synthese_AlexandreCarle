using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip _backgroundMusic = default;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(_backgroundMusic, Camera.main.transform.position, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
