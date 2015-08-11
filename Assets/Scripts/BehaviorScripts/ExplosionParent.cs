using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ExplosionParent : MonoBehaviour {

	public int chain;
    public List<GameObject> Children;
    public float PointBase;
    public float Score;
    public float sizeMulitplyer;

    public GameObject myGO;
    public Font CodeSmall;
    // Use this for initialization
	void Start () {
        if (sizeMulitplyer == null) sizeMulitplyer = 1f;
        foreach (Transform child in transform)
        {
            if (child.name == "TestBoom")
            {
                var giveExplode = child.gameObject.GetComponent<ExplosionScript>();
                giveExplode.chain = chain;
                var size = giveExplode.MaxSize * sizeMulitplyer;

                giveExplode.MaxSize = size;
                giveExplode.TimeToFull = giveExplode.TimeToFull * sizeMulitplyer;
            }
            
        }
        if (sizeMulitplyer == 0) Destroy(this.gameObject);
        //  SCOREING APERATUS WILL APPEAR HERE

        Score = PointBase * (chain * chain);

        myGO =  GameObject.Find("TestCanvas");
        Canvas myCanvas = myGO.GetComponent<Canvas>();
        var childGO = new GameObject();
        childGO.transform.parent = myGO.transform;
        Text textComponent = childGO.AddComponent<Text>();
        textComponent.font = CodeSmall;
        textComponent.text = Score.ToString();
        if (Score == 0) textComponent.text = "";
        textComponent.alignment = TextAnchor.LowerLeft;

        RectTransform childRectTransform = childGO.GetComponent<RectTransform>();
        childRectTransform.anchoredPosition3D = Camera.main.WorldToScreenPoint(this.transform.position); 
        childRectTransform.sizeDelta = new Vector2(10f, 10f);
        childRectTransform.anchorMin = new Vector2(.035f, .035f);
        childRectTransform.anchorMax = new Vector2(1f, 1f);
        childGO.AddComponent<ScoreBurstScript>();
        childGO.GetComponent<ScoreBurstScript>().Score = Score;


        GameObject[] scoreFind;
        scoreFind = GameObject.FindGameObjectsWithTag("Score");
        foreach (GameObject go in scoreFind)
        {
            if(go != null)go.GetComponent<Score>().AddScore(Score);
        }


	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
