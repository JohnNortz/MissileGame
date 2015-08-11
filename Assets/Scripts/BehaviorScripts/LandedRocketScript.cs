using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LandedRocketScript : MonoBehaviour {


    public float motion = 1;  // 1 = 1 to 1, moves at tank speed;  2 = half as fast as tank, in background; <1 = foreground;
    public bool namePlate = false;

    public GameObject myGO;
    public Font Ariel;
    public GameObject Ship;
    public GameObject passedPlate;
    private float ran = 0;
    public Animator anim;
    public GameObject Pad;
    // Update is called once per frame
    void Start()
    {
        ran = Random.Range(-12, 8);

        float offset = Random.Range(0, .2f);
        motion = .8f - offset;
        var temp = transform.position;
        var tempS = transform.localScale;
        temp.y += offset * 2;
        temp.z = 13 + (offset * 35);
        tempS.x = tempS.x - offset;
        tempS.y = tempS.y - offset;

        transform.position = temp;
        transform.localScale = tempS;

        var pos = this.transform.position;
        pos.y -= .26f;
        pos.z -= .2f;
        var pad = Instantiate(Pad, pos, Quaternion.identity) as GameObject;
        var padData = pad.GetComponent<DeadSpriteMotion>();
        padData.motion = motion;
        pad.transform.localScale = new Vector3(transform.localScale.x * .5f, .3f * transform.localScale.y, 1f);
    }

    void Update()
    {
        transform.Translate(Vector3.left * motion * Time.deltaTime);
        if (transform.position.x < ran)
        {
            anim.SetBool("Launch", true);
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
            textComponent.color = new Color(0, 1, 0, 0);
            textComponent.text = "Colony Ship";

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

    public void Launch()
    {
        var pos = this.transform.position;
        pos.z = 0;
        var rocket = Instantiate(Ship, pos, Quaternion.identity) as GameObject;
        rocket.GetComponent<ShipBehavior>().givenX = this.transform.position.x;
        Destroy(passedPlate.gameObject);
        Destroy(this.gameObject);
    }
}