using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject settingsScreen;

    private bool isPaused;
    private bool canUseEscape = true;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (canUseEscape)
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            StartCoroutine(StartPause());
        }
        else
        {
            StartCoroutine(StartUNPause());
        }
    }

    public void GoToSettings()
    {
        canUseEscape = false;
        settingsScreen.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(false);
    }

    public void GoFromSettings()
    {
        canUseEscape = true;
        settingsScreen.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(true);
    }

    private IEnumerator StartPause()
    {
        canUseEscape = false;
        pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
        //pauseMenuAnimator.SetBool("isPaused", true);
        yield return new WaitForSecondsRealtime(0f);
        canUseEscape = true;
    }

    private IEnumerator StartUNPause()
    {
        canUseEscape = false;
        //pauseMenuAnimator.SetBool("isPaused", false);
        yield return new WaitForSecondsRealtime(0f);
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;
        canUseEscape = true;
    }

}
