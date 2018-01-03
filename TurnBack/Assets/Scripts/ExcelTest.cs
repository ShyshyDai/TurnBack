using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExcelTest : MonoBehaviour
{
    string CameraName;
    string UnitName;
    int startFlag;
    string number;
    int i;
    FileStream getTimeData;
    StreamWriter getTimeStream;

    // Use this for initialization
    void Start()
    {
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

    //1
    void numberChange()
    {
     
        number = i.ToString().PadLeft(4, '0');//在字符串左边用0补足totalWidth长度
    }

    
    public void writeTimeData()
    {
        Debug.Log("04");
        getTimeStream.WriteLine(System.DateTime.Now);
        getTimeStream.Close();
        getTimeData.Close();
    }
    /*
    public void closeWrite()
    {
        Debug.Log("05");
        getTimeStream.Close();
        getTimeData.Close();
    }
    */
 
}