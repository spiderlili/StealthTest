using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public GameObject player;
    bool _isPlayerAtExit = false;
    public float displayImageDuration = 3.0f;

    public CanvasGroup exitBackgroundImageCanvas;
    float _Timer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            _isPlayerAtExit = true;
        }
    }

    //Update is getting called every frame, checking whether the player’s character is at the exit. 
    private void Update()
    {
        if (_isPlayerAtExit)
        {
            EndLevel();
        }
        
    }

    // fade the Canvas Group and then quit the game.
    void EndLevel()
    {
        //timer to ensure  the game doesn't end before the fade has finished.
        _Timer += Time.deltaTime;
        exitBackgroundImageCanvas.alpha = _Timer / fadeDuration;
        if(_Timer > fadeDuration + displayImageDuration)
        {
            Application.Quit();
        }
    }
}
