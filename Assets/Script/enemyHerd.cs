using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  //序列化,将对象实例的状态存储到存储媒体的过程
public class enemyHerd{
    public GameObject enemyPrefab;
    public int enemyNum;
    public float enemyCreatRate;
}
