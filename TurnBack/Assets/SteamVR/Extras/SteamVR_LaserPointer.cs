//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;

public struct PointerEventArgs
{
    public uint controllerIndex;//手柄索引
    public uint flags;//？
    public float distance;//激光原点到命中点
    public Transform target;//命中物体的Transform对象
}

public delegate void PointerEventHandler(object sender, PointerEventArgs e);//？


public class SteamVR_LaserPointer : MonoBehaviour
{
    public bool active = true;
    public Color color;//激光颜色
    public float thickness = 0.002f;//粗细
    public GameObject holder;//空的GO用于做激光束的parent
    public GameObject pointer;//激光束本身Cube
    bool isActive = false;//是否为第一次调用
    public bool addRigidBody = false;//是否为刚体
    public Transform reference;//？
    public event PointerEventHandler PointerIn;//用于触发激光命中和离开事件
    public event PointerEventHandler PointerOut;

    Transform previousContact = null;//上次激光命中的物体的Trans对象，用于判断是否击中同一个物体

	// Use this for initialization
	void Start ()
    {
        holder = new GameObject();//创建激光束父物体
        holder.transform.parent = this.transform;//将holder的trans的爸爸设为当前脚本所在的物体
        holder.transform.localPosition = Vector3.zero;//位置设置在爸爸坐标系的O
		holder.transform.localRotation = Quaternion.identity;

		pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);//创建激光束
        pointer.transform.parent = holder.transform;//激光束的爸爸
        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);//设置Cube的大小
        pointer.transform.localPosition = new Vector3(0f, 0f, 50f);//位置设置在爸爸的（0，0，50）处，因为Cube的中心在中心
		pointer.transform.localRotation = Quaternion.identity;
		BoxCollider collider = pointer.GetComponent<BoxCollider>();
        if (addRigidBody)//如果是刚体，则对应的Collider只设置为触发器，否则把Collider销毁
        {
            if (collider)
            {
                collider.isTrigger = true;
            }
            Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
            rigidBody.isKinematic = true;
        }
        else
        {
            if(collider)
            {
                Object.Destroy(collider);
            }
        }
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;
	}

    public virtual void OnPointerIn(PointerEventArgs e)
    {
        if (PointerIn != null)
            PointerIn(this, e);
    }

    public virtual void OnPointerOut(PointerEventArgs e)
    {
        if (PointerOut != null)
            PointerOut(this, e);
    }


    // Update is called once per frame
	void Update ()
    {
        if (!isActive)//第一次调用将holder设为active，当前物体transform的第一个child就是holder
        {
            isActive = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        //命中物体的最远距离
        float dist = 100f;
        //当前物体要求挂载SteamVR_TrackedController这个脚本
        SteamVR_TrackedController controller = GetComponent<SteamVR_TrackedController>();
        //创建一个射线（？？）
        Ray raycast = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool bHit = Physics.Raycast(raycast, out hit);//计算射线命中的场景物体

        if(previousContact && previousContact != hit.transform)
        {
            PointerEventArgs args = new PointerEventArgs();
            if (controller != null)
            {
                args.controllerIndex = controller.controllerIndex;
            }
            args.distance = 0f;
            args.flags = 0;
            args.target = previousContact;
            OnPointerOut(args);
            previousContact = null;
        }
        if(bHit && previousContact != hit.transform)
        {
            PointerEventArgs argsIn = new PointerEventArgs();
            if (controller != null)
            {
                argsIn.controllerIndex = controller.controllerIndex;
            }
            argsIn.distance = hit.distance;
            argsIn.flags = 0;
            argsIn.target = hit.transform;
            OnPointerIn(argsIn);
            previousContact = hit.transform;
        }
        if(!bHit)
        {
            previousContact = null;
        }
        if (bHit && hit.distance < 100f)
        {
            dist = hit.distance;
        }

        if (controller != null && controller.triggerPressed)
        {
            pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
        }
        else
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
        }
        pointer.transform.localPosition = new Vector3(0f, 0f, dist/2f);
    }
}
