using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float damage = 15;   //伤害
    public float speed = 30;   //速度
    private Vector3 target;
    public GameObject boomPrafab;
    public GameObject enemy;
    bool targetDie = false;

    public void Start()
    {
       
        Destroy(this.gameObject, 4);
    }

    public void SetTargetPos(Vector3 tar)
    {
        this.target = tar;
    }

    public void SetTargetDie()
    {
        targetDie = true;
    }


    public void SetTarget(GameObject target)
    {
        this.enemy = target;
    }

    //子弹触碰到敌人（敌人减伤害，特效，子弹销毁）
    private void OnTriggerEnter(Collider enemyCol) 
    {

        if (enemyCol.tag=="enemy") {
            enemyCol.GetComponent<enemyManager>().ReduceBlood(damage); //减伤害
            Die();
        }
    }

    // Update is called once per frame
    void Update() {

        if (enemy != null && enemy.GetComponent<enemyManager>().isDie)
        {
            Die();
        }

        this.transform.LookAt(target);
        this.transform.Translate(Vector3.forward);

    }

    void Die() //自身销毁
    {
        Destroy(gameObject); //销毁自身
        GameObject boom;
        boom = Instantiate(boomPrafab, this.transform.position, Quaternion.identity);  
        Destroy(boom, 2);
    }
}
