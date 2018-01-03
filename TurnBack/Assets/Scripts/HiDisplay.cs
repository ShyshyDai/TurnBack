using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HiDisplay : MonoBehaviour {

    SteamVR_LaserPointer slp;
    SteamVR_TrackedController stc;
    GameObject target = null;

    string CameraName;
    string UnitName;
    int startFlag;
    string number;
    int i;
    FileStream getTimeData;
    StreamWriter getTimeStream;

    // Use this for initialization

    void Start () {
        slp = GetComponent<SteamVR_LaserPointer>();//获取射线对象
        slp.PointerIn += OnpointerIn;//响应射线的进入
        slp.PointerOut += OnpointerOut;//响应射线的离开
        stc = GetComponent<SteamVR_TrackedController>();
        stc.TriggerClicked += OnTriggerClicked;
        stc.TriggerUnclicked += OnTriggerUnclicked;

        CameraName = "camera";
        UnitName = "unit";
        i = 1;
        number = "";
        numberChange();

        CameraName = "camera";
        UnitName = "unit";
        i = 1;
        number = "";
        numberChange();

        if (!File.Exists("..\\" + CameraName + number + ".csv"))
        {
            getTimeData = new FileStream("..\\" + CameraName + number + ".csv", FileMode.Create, FileAccess.Write);
            Debug.Log("creat");
        }
        else
        {
            i = i + 1;
            numberChange();
            while (File.Exists("..\\" + CameraName + number + ".csv"))
            {
                i = i + 1;
                numberChange();
            }
            getTimeData = new FileStream("..\\" + CameraName + number + ".csv", FileMode.Create, FileAccess.Write);

            Debug.Log("exist create" + number);
        }

        getTimeStream = new StreamWriter(getTimeData);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnpointerIn(object sender, PointerEventArgs e)
    {
        GameObject obj = e.target.gameObject;
        if (obj.tag.Equals("Cube_0"))
        {
            target = obj;
        }
    }

    void OnpointerOut(object sender, PointerEventArgs e)
    {
        if (target != null)
        {
            target = null;
        }
    }

    void OnTriggerClicked(object sender, ClickedEventArgs e)
    {
        if (target != null)
        {
            //Rigidbody r = target.GetComponent<Rigidbody>();
            //Destroy(r);
            //target.transform.position = transform.position;
            //target.transform.parent = transform;
            Debug.Log("Cube_0");
        }

    }

    void OnTriggerUnclicked(object sender, ClickedEventArgs e)
    {
        if (target != null)
        {
            //target.AddComponent<Rigidbody>().AddForce(transform.forward * 500);
            //target.transform.parent = null;
            //target.GetComponent<Renderer>().material.color = Color.black;
            Debug.Log("01");
            getTimeStream.WriteLine(System.DateTime.Now);
            Debug.Log("02");
            getTimeStream.Close();
            getTimeData.Close();
            Debug.Log("03");
            Destroy(target);
        }
       
            
    }

    
    void numberChange()
    {

        number = i.ToString().PadLeft(4, '0');//在字符串左边用0补足totalWidth长度
    }
}


