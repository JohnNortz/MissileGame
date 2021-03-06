﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlacementAdjustment : MonoBehaviour {

    public Vector3 offset;
    public Canvas canvas;
    public GameObject lockTo;


	void Start () {
          Vector3 pos;
          float width = canvas.GetComponent<RectTransform>().sizeDelta.x;
          float height = canvas.GetComponent<RectTransform>().sizeDelta.y;
          float x = Camera.main.WorldToScreenPoint(transform.position).x / Screen.width;
          float y = Camera.main.WorldToScreenPoint(transform.position).y / Screen.height;
          pos = new Vector3(width * x - width / 2, y * height - height / 2) + offset;    
          this.transform.position = pos;    
      }
	
	// Update is called once per frame
	void Update () {
	
	}
}
