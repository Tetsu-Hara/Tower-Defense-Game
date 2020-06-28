using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buildTurret : MonoBehaviour {

    public TurretData turret01;
    public TurretData turret02;
    public TurretData turret03;

    private TurretData selectTurret; //当前在ui面板所选择的炮台(数据)
    private mapCube selectMapCube; //在3d场景中选择建造炮台的cube（cube有记录炮台的类型）

    //自己拥有的钱
    private int money = 150;
    public Text moneyText;
    public Animator moneyAnim;


    //炮塔升级
    public GameObject upgradeCanvas;
    public Button upgradeButton;
    private Animator upgradeCanvasAnim;

    private void Start()
    {
        moneyAnim = moneyText.GetComponent<Animator>();
        upgradeCanvasAnim = upgradeCanvas.GetComponent<Animator>();
    }

    void moneyUpdate(int changeMoney)
    {
        money += changeMoney;
        moneyText.text = "￥" + money;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectTurret!=null) //如果选择了炮台
            {
                if (!EventSystem.current.IsPointerOverGameObject()) //判断鼠标不在UI上
                {
                    Ray ray;
                    RaycastHit hit;
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray,out hit,1000,LayerMask.GetMask("MapCube")))
                    {
                       
                        mapCube hitCube = hit.collider.GetComponent<mapCube>(); //得到射线射到的Cube（的mapCube组件）
                        if (hitCube.TurruetGo==null) //判断，MapCube上有没有炮台
                        {
                            //没有炮台->创建
                            if (money>=selectTurret.cost)
                            {
                                moneyUpdate(-selectTurret.cost);
                                hitCube.CreatTurret(selectTurret);
                            }
                            else
                            {
                                //提示钱不够
                                moneyAnim.SetBool("noMoney", true);
                            }
                        }
                        else if(hitCube.TurruetGo!=null)//有炮台->升级（显示升级按钮）
                        {


                            if (hitCube == selectMapCube && upgradeCanvas.activeInHierarchy) //判断选择的是否为同一个炮台(同一个炮台在同一个cube上) && 判断ui有没有显示出来
                            {
                                StartCoroutine(HideUpgradeUI()); //隐藏
     
                            }
                            else
                            {
                                ShowUpgrade(hitCube.transform.position, hitCube.isUpgrade);  // if 有升级过isUpgrade=isDisableUpgrade 
                               
                            }                                                               


                            selectMapCube = hitCube;//保存每次选择的炮台
                        }

                    }
                }
            }
            
        }
    }


    public void OnTurret01(bool isOn)
    {
        if (isOn) selectTurret = turret01;
    }

    public void OnTurret02(bool isOn)
    {
        if (isOn) selectTurret = turret02;
    }

    public void OnTurret03(bool isOn)
    {
        if (isOn) selectTurret = turret03;
    }



    //显示炮塔升级按钮
    void ShowUpgrade(Vector3 pos, bool isDisableUpgrade=false)  //isDisableUpgrade:是否禁用升级按钮；默认不禁用
    {
        StopCoroutine("HideUpgrade");
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true); //显示
        upgradeCanvas.transform.position = pos;
        upgradeButton.interactable = !isDisableUpgrade; //不禁用升级按钮
    }

    //隐藏炮塔升级按钮
    IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnim.SetTrigger("hide");
        yield return new WaitForSeconds(0.8f);
        upgradeCanvas.SetActive(false); 
       
    }

    //按下升级按钮
    public void OnUpgradeButtonDown()
    {
        if (money >= selectMapCube.turretDataType.cost)
        {
            selectMapCube.upgardeTurret();
            StartCoroutine(HideUpgradeUI());
            moneyUpdate(-selectMapCube.turretDataType.cost); //减去升级的钱

        }
        else
        {
            moneyAnim.SetBool("noMoney", true); //显示钱不够
        }
       
    }

    //按下拆除按钮
    public void OnDismantleButtonDown()
    {
        selectMapCube.destoryTurret();
        StartCoroutine(HideUpgradeUI());
    }


}
