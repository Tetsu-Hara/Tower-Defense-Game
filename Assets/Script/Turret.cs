using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public List<GameObject> enemys = new List<GameObject>();
    public float  attackRate = 1; //多少秒攻击一次
    private float time=0;

    public GameObject bulletPrefab;
    public Transform firePos;
    public Transform TurretHead;

    //激光
    public bool useLaser = false;
    public float damageRate = 50; //激光每秒70伤害
    public LineRenderer lineRenderer;
    public GameObject laserEffect; //激光特效

    //敌人进入炮塔范围
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            enemys.Add (other.gameObject);
        }
    }
    //敌人出炮塔范围
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }
    private void Start()
    {
        time = attackRate;
    }

    private void Update()
    {
        
        //控制炮头转向
        if (enemys.Count > 0 && enemys[0] != null)
        {
            TurretHead.LookAt(enemys[0].transform);
        }

        if (useLaser == false)
        {  //不使用激光
            time += Time.deltaTime;
            if (enemys.Count > 0 && time > attackRate)
            {
                time = 0;
                Attack();
            }
        }
        else if (enemys.Count > 0)
        {
            if (lineRenderer.enabled == false)
            {
                lineRenderer.enabled = true;
                laserEffect.SetActive(true);
            }
            if (enemys[0] == null)
            {
                enemyListUpdate();
            }
            if (enemys.Count > 0)
            {
                Vector3 endPos = new Vector3(enemys[0].transform.position.x,transform.position.y+2, enemys[0].transform.position.z);
                lineRenderer.SetPositions(new Vector3[] { firePos.position, endPos });
                enemys[0].GetComponent<enemyManager>().ReduceBlood(damageRate * Time.deltaTime);
                laserEffect.transform.position = endPos;
                laserEffect.transform.LookAt(endPos);

            }
        }
        else
        {
            lineRenderer.enabled = false;
            laserEffect.SetActive(false);
        }
    }

    void Attack()
    {

        if (enemys[0]==null)
        {
            enemyListUpdate();
        }

        if (enemys.Count > 0)
        {
            GameObject b = GameObject.Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            Vector3 position = new Vector3(enemys[0].transform.position.x, enemys[0].transform.position.y + 1, enemys[0].transform.position.z);
            b.GetComponent<bullet>().SetTargetPos(position);
            b.GetComponent<bullet>().SetTarget(enemys[0]);
        }
        else time = attackRate;
        
    }


    //有敌人死亡时，更新敌人列表
    void enemyListUpdate()
    {
        List<int> enemyIndex = new List<int>();
        for (int index=0;index<enemys.Count;index++)
        {
            if (enemys[index]==null)
            {
                enemyIndex.Add(index);  //把空对象的索引存起来
            }
        }

        for (int i=0;i<enemyIndex.Count;i++)
        {
            enemys.RemoveAt(enemyIndex[i] - i); //RemoveAt:按照索引移除；enemyIndex[i] - i：对象被移除了会自动向前进一个，减去移动的位置
        }
    }

}
