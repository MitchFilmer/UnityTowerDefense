using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public AudioClip clip;
    public Animation menuAnimation;
    public GameObject[] controlPages;

    private AudioSource source;
    private int activePage = -1;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        CloseControls();
    }

    public void LoadMain (string name)
    {
		source.Stop();
        StartCoroutine("LoadLevel", (name));
    }

    IEnumerator LoadLevel(string name)
    {
        source.PlayOneShot(clip, 1f);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NextPage()
    {
        activePage++;
        controlPages[activePage].SetActive(true);
        if (activePage >= 1)
        {
            controlPages[activePage - 1].SetActive(false);
        }
    }

    public void CloseControls()
    {
        activePage = -1;
        controlPages[0].SetActive(false);
        controlPages[1].SetActive(false);
    }
}
