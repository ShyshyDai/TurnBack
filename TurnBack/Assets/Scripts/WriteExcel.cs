using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WriteExcel : MonoBehaviour {
    string fileName;
    public GameObject Wcube;//?
    int startFlag;//?
    string number;
    int i;
    FileStream test1;
    StreamWriter streamW;
    // Use this for initialization
    void Start () {
        fileName = "result";
        i = 1;
        number = "001";
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
        
            if (startFlag == 0)
            {
                startFlag = 1;

                if (!File.Exists("..\\" + fileName + number + ".txt"))
                {
                    test1 = new FileStream("..\\" + fileName + number + ".txt", FileMode.Create, FileAccess.Write);
                    Debug.Log("creat");
                }
                else
                {
                    i = i + 1;
                    numberChange();
                    while (File.Exists("..\\" + fileName + number + ".txt"))
                    {
                        i = i + 1;
                        numberChange();
                    }
                    test1 = new FileStream("..\\" + fileName + number + ".txt", FileMode.Create, FileAccess.Write);
                    Debug.Log("exist create" + number);
                }
            }
            streamW = new StreamWriter(test1);
        }
        
       if (startFlag == 1)
       {
            streamW.WriteLine("旋转角度：" + Wcube.transform.rotation);//直接在这行里面进行内容添加即可
            test1.Close();

       }
    }

    void numberChange()
    {
        if (i / 100 > 0)
        {
            number = i.ToString();
        }
        else if (i / 10 > 0)
        {
            number = "0" + i.ToString();
        }
        else
        {
            number = "00" + i.ToString();
        }
    }
}

