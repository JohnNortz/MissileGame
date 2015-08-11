using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeadSpriteMotion : MonoBehaviour {

    public float motion;  // 1 = 1 to 1, moves at tank speed;  2 = half as fast as tank, in background; <1 = foreground;
    public string playerName;
    public bool namePlate = false;

    public GameObject myGO;
    public Font Ariel;

    public GameObject passedPlate;
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.left * motion * Time.deltaTime);
        if (transform.position.x < -20)
        {
            Destroy(passedPlate.gameObject);
            Destroy(this.gameObject);
        }

        if (transform.position.x < 20 && namePlate == false)
        {
            myGO = GameObject.Find("TestCanvas");
            Canvas myCanvas = myGO.GetComponent<Canvas>();
            myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var childGO = new GameObject();
            childGO.transform.parent = myGO.transform;
            Text textComponent = childGO.AddComponent<Text>();
            textComponent.font = Ariel;
            textComponent.color = new Color(0, 0, 0, 1);

            if (playerName != "" && playerName != null) textComponent.text = playerName.ToString();
            else Destroy(this.gameObject);

            textComponent.alignment = TextAnchor.LowerLeft;

            RectTransform childRectTransform = childGO.GetComponent<RectTransform>();
            childRectTransform.anchoredPosition3D = Camera.main.WorldToScreenPoint(this.transform.position);
            childRectTransform.sizeDelta = new Vector2(10f, 10f);
            childRectTransform.anchorMin = new Vector2(0f, .02f);
            childRectTransform.anchorMax = new Vector2(1f, 1f);
            passedPlate = childGO;

            namePlate = true;
        }
        if (namePlate)
        {
            passedPlate.GetComponent<RectTransform>().anchoredPosition3D = Camera.main.WorldToScreenPoint(this.transform.position);
            if (passedPlate.GetComponent<Text>().text == "") Destroy(this.gameObject);
        }
 	}
}
