using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscenes : MonoBehaviour
{
    [SerializeField] private GameObject[] cutscenes;

    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float waitToLoad;

    private bool fadeToBlack; 
    private bool fadeOutBlack;

    private int counter;
    private bool areCutScenesEnabled;

    private void Awake()
    {
        foreach (GameObject g in cutscenes)
        {
            g.SetActive(false);
        }

        fadeOutBlack = true;
        fadeToBlack = false;
    }

    private void Update()
    {
        if (fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }

        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (areCutScenesEnabled && Input.anyKeyDown)
        {
            StartCoroutine(ShowNextCutscene());
        }
    }

    public IEnumerator ShowNextCutscene()
    {
        StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        StartFadeOutBlack();

        if (counter + 1 == cutscenes.Length)
        {
            Debug.Log("Changing scene");
            SceneManager.LoadScene("Cards");
        }
        else
        {
            cutscenes[counter].gameObject.SetActive(false);
            counter++;
            cutscenes[counter].gameObject.SetActive(true);
        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void StartFadeOutBlack()
    {
        fadeToBlack = false;
        fadeOutBlack = true;
    }

    public void PlayGame()
    {
        StartCoroutine(StartGame());
    }

    public void GoBackFromPage(GameObject pageToDisctivate)
    {
        StartCoroutine(UnloadCredits(pageToDisctivate));
    }

    public void GoToPage(GameObject pageToActivate)
    {
        StartCoroutine(LoadCredits(pageToActivate));
    }

    private IEnumerator StartGame()
    {
        StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        StartFadeOutBlack();

        cutscenes[0].gameObject.SetActive(true);

        areCutScenesEnabled = true;
    }

    private IEnumerator LoadCredits(GameObject pageToActivate)
    {
        StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        pageToActivate.SetActive(true);

        StartFadeOutBlack();

    }

    private IEnumerator UnloadCredits(GameObject pageToDisctivate)
    {
        StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        pageToDisctivate.SetActive(false);

        StartFadeOutBlack();
    }
}
