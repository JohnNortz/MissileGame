using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SupplyBoxMotion : MonoBehaviour {

    public float motion;  // 1 = 1 to 1, moves at tank speed;  2 = half as fast as tank, in background; <1 = foreground;
    public string Item_Name;
    public bool namePlate = false;
    public int amount;
    public string type;

    public GameObject myGO;
    public Font Ariel;

    public GameObject passedPlate;
    // Update is called once per frame
    void Start()
    {

        if (type == null || type == "") type = "Standard";
        if (type == "Turret" || type == "Blast Shield") Item_Name = "Hardware Upgrade";
        else if (type == "Children Missiles" || type == "Blast Radius" || type == "Hard Shell") Item_Name = "Warhead Upgrade";
        else Item_Name = type + " Ammo Box";
        if (type == "Blast Shield") amount = 1;
        if (amount == null || amount == 0)
        {
            amount = 40;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.left * motion * Time.deltaTime);
        if (transform.position.x < -.87)
        {
            var playerManagerObj = GameObject.Find("Main Camera").camera;
            if (type == "Standard" || type == "BigSlow" || type == "EMP" || type == "Blast Shield" || type == "Blast Radius" || type == "Children Missiles" || type == "Hard Shell") playerManagerObj.GetComponent<PlayerManager>().getAmmo(type, amount);
            if (type == "Turret")
            {
                var place = playerManagerObj.GetComponent<PlayerManager>();
                if (type == "Turret")
                {
                    if (place.behavior1)
                    {
                        if (place.behavior1Length > .4f)
                        {
                            place.behavior1Length -= .2f;
                        }
                    }
                    else
                    {
                        place.behavior1 = true;
                        place.behavior1Length = 1.6f;
                    }
                }
            }



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
            textComponent.color = new Color(0, 0, 1, 1);
            textComponent.text = Item_Name;

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