using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject fadeOutPanel;

    [SerializeField]
    private GameObject submissionPanel;
    public bool testMode;


    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public IEnumerator Restart() {
        fadeOutPanel.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SubmitScore() {
        StartCoroutine(Restart());
    }

    public IEnumerator Death()
    {
        if (!testMode)
        {
            GameObject.Find("Jail").GetComponent<Animation>().Play();
            GameObject.Find("Jail").GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(3f);
            fadeOutPanel.SetActive(true);
            if(GameObject.Find("Player"))
                GameObject.Find("Player").SetActive(false);

            StartCoroutine(StartFade(this.GetComponent<AudioSource>(), 3, 0));
            yield return new WaitForSeconds(3f);
            fadeOutPanel.GetComponent<Animator>().Play("FadeOut");
            submissionPanel.SetActive(true);
            submissionPanel.transform.Find("ScoreText").GetComponent<Text>().text = "Score: " + this.GetComponent<ScoreSystem>().getScore();
        }

    }

}
