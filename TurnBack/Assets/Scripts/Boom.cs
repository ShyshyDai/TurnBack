using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour {
    public GameObject[] GeoPrefab;
    private Vector3 pos;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {

            for (int i = 1; i < 10; i++)
            {

                Color CubeRanCol = new Color();
                CubeRanCol.r= Random.Range(0f, 1f);
                CubeRanCol.g = Random.Range(0f, 1f);
                CubeRanCol.b = Random.Range(0f, 1f);
               
                float x = Random.Range(10, 100);
                float y = Random.Range(10, 100);
                float z = Random.Range(10, 100);
                float GeoScale = Random.Range(0.5f, 1);

                pos = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
                Quaternion RanRota = Quaternion.Euler(new Vector3(x, y, z));
                GameObject CubeGo=Instantiate(GeoPrefab[Random.Range(0, GeoPrefab.Length)], pos, RanRota);
                //CubeGo.GetComponent<MeshRenderer>().material.SetColor("_Color", CubeRanCol);//RGB空间
                CubeGo.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0.1f, 0.5f, 0f, 1f, 0.5f, 1f, 0f, 1f);//HSV空间
                CubeGo.GetComponent<Transform>().localScale = new Vector3(GeoScale, GeoScale, GeoScale);


            } 
        }
	}
}
