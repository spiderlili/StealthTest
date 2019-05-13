using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public GameObject player;

    bool _isPlayerAtExit = false;
    bool _isPlayerCaught = false;
    public float displayImageDuration = 3.0f;

    public CanvasGroup exitBackgroundImageCanvas;
    public CanvasGroup caughtBackgroundImageCanvas;

    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    bool _hasAudioPlayed;

    float _Timer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            _isPlayerAtExit = true;
        }
    }

    public void PlayerCaught()
    {
        _isPlayerCaught = true;
    }

    //Update is getting called every frame, checking whether the player’s character is at the exit. 
    private void Update()
    {
        if (_isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvas, false, exitAudio);
        }
        else if(_isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvas, true, caughtAudio);
        }
    }

    // fade the Canvas Group and then quit the game.
    void EndLevel(CanvasGroup imageCanvasGroup, bool restartGame, AudioSource audioSource)
    {
        //timer to ensure  the game doesn't end before the fade has finished.
        _Timer += Time.deltaTime;
        imageCanvasGroup.alpha = _Timer / fadeDuration;
        if(_Timer > fadeDuration + displayImageDuration)
        {
            if (restartGame)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }

        }
    }
}
