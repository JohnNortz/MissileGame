using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBurstScript : MonoBehaviour {

    private float timer = 2f;
    private float _fontSize = 10;
    public float Score;
    public RectTransform _this;

    private float vel = 80f;
    private float posX;
    private float posY;
	// Use this for initialization
	void Start () {
        _this = gameObject.GetComponent<RectTransform>();
        posX = _this.anchoredPosition.x;
        posY = _this.anchoredPosition.y;

        if (_this.anchoredPosition.x > 1000f) posX = 900f;
        _fontSize = _fontSize * (1 + (Score/500));
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        if (timer > 1)
        {
            _fontSize += 15f * Time.deltaTime;
            GetComponent<Text>().color = new Color(0f, 1f, 0f, 1f);
        }
        if (timer < 1)
        {
            _fontSize -= 15f * Time.deltaTime;
        }
        if (timer < .5) GetComponent<Text>().color = new Color((1 - (timer / .5f)), (0 + (timer / .5f)), 0, (0 + (timer / .5f)));
        if (timer <= 0) Destroy(this.gameObject);

        
        GetComponent<Text>().fontSize = (int)_fontSize;

        posY += vel * Time.deltaTime;
        vel -= 130 * Time.deltaTime;

        _this.anchoredPosition = new Vector2(posX, posY);
	}
}
