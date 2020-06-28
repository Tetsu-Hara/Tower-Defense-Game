using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mapCube : MonoBehaviour {

    [HideInInspector] //在Inspector处不显示
    public GameObject TurruetGo; //保存cube上创建的炮台
    private Renderer ren; //渲染器
    [HideInInspector]
    public bool isUpgrade=false; //用于判断该位置的炮塔是否升级过
    [HideInInspector]
    public TurretData turretDataType; //保存建造炮塔的类型

    void Start()
    {
        ren = GetComponent<Renderer>();
    }

    public void CreatTurret(TurretData turretDate) 
    {
        this.turretDataType = turretDate;  //记录建造炮塔的类型
        isUpgrade = false;
        TurruetGo = GameObject.Instantiate(turretDataType.turretPrefab, transform.position,Quaternion.identity);
    }

    public void upgardeTurret()
    {
        if (isUpgrade == true) return; //如果升级过就不升级
        else
        {
            Destroy(TurruetGo);//销毁原来的
            isUpgrade = true;
            TurruetGo = GameObject.Instantiate(turretDataType.turretUpPrefab, transform.position, Quaternion.identity);
        }
    }

    public void destoryTurret()
    {
        Destroy(TurruetGo);
        isUpgrade = false;
        TurruetGo = null;
        turretDataType = null;
    }



    private void OnMouseEnter()
    {
         if (TurruetGo==null && EventSystem.current.IsPointerOverGameObject()==false) //判断  无炮台；鼠标不在ui上
        {
            ren.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        ren.material.color = Color.white;
    }
}
