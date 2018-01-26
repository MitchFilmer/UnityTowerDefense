using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public GameObject[] controlPages;


	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(false);
	}

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void NextPage()
    {
        this.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void ClosePages()
    {
        this.gameObject.SetActive(false);
    }


}
