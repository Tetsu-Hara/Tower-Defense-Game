using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour {

    public enemyHerd[] e_Herds;
    public Transform startPos;
    public float nextHerdRate = 4;

    public GameObject SpawnEffect;  //敌人生成的特效

    public static int countEnemyLive = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemy());
	}
	

    IEnumerator SpawnEnemy()
    {
        foreach(enemyHerd e_Hard in e_Herds)
        {
            for (int i=0;i<e_Hard.enemyNum;i++)  //同一波敌人
            {
                GameObject.Instantiate(e_Hard.enemyPrefab, startPos.position, Quaternion.identity);
                countEnemyLive++;
                GameObject effect;
                effect= GameObject.Instantiate(SpawnEffect, startPos.position, Quaternion.identity);
                Destroy(effect, 2);
                if (i!=e_Hard.enemyNum-1)  //最后一个不用等小时间，直接等大时间
                {
                    yield return new WaitForSeconds(e_Hard.enemyCreatRate); //每一个后面都等几秒再创建
                }
            }
            while (countEnemyLive >0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(nextHerdRate); //等待几秒，再有下一波敌人
        }

        while (countEnemyLive >0)
        {
            yield return 0;
        }
        //Win
        endCube.Instance.Win();
    }



}
