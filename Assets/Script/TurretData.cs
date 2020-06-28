using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   //序列化
public class TurretData {

    public GameObject turretPrefab;
    public GameObject turretUpPrefab;
    public int cost=75; //买炮台所需的金币
    public TurretType type; //炮台类型
}

public enum TurretType
{
    Turret01,
    Turret02,
    Turret03
}
