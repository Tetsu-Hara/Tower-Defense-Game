using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyManager : MonoBehaviour {

    public GameObject Enemy;
    public Transform target;
    private NavMeshAgent navAgent;
    private Animator anim;
    private float hp=500;
    private float totalhp;
    private Slider hpSlider;
    int attackID;

    public float endCubeDamage = 2;

    // Use this for initialization
    void Start () {
        navAgent = Enemy.GetComponent<NavMeshAgent>();
        anim = Enemy.GetComponent<Animator>();
        hpSlider = GetComponentInChildren<Slider>();
        attackID = Animator.StringToHash("Attacking");
        totalhp = hp;
	}
	
	// Update is called once per frame
	void Update () {
        enemyMove();
    }

    void enemyMove()
    {
        navAgent.destination = target.position;
        anim.SetBool(attackID, false);
        if (Vector3.Distance(Enemy.transform.position, target.position) < 8)
        {
            anim.SetBool(attackID, true);
            navAgent.speed = 0;
            endCube.Instance.hp -= endCubeDamage * Time.deltaTime;
        }
    }

    void OnDestroy()
    {
        enemySpawn.countEnemyLive--;
    }

    public void ReduceBlood(float damage)
    {
        hp -= damage;
        hpSlider.value = hp / totalhp;
        if (hp<=0)
        {
            EnrmyDie();
        }
    }

    public bool isDie = false;

    void EnrmyDie()
    {
        Destroy(this.gameObject);
        isDie = true;
    }




}
