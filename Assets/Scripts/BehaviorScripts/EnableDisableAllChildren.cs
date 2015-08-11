using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnableDisableAllChildren : MonoBehaviour {

    // Use this for initialization
	void Start () {
        this.GetComponent<RawImage>().enabled = true;
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(1000f, 0f);
	}

    public void flop ()
    {
        if (this.GetComponent<RectTransform>().anchoredPosition.x != 0) this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
        else this.GetComponent<RectTransform>().anchoredPosition = new Vector2(1000f, 0f);

        Time.timeScale = 0;
    }

    public void Restart ()
    {
        Time.timeScale = 1;
        Application.LoadLevel("GameSceneNewUI");
    }
}
