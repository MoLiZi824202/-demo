using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManger : MonoBehaviour
{
    public TurretData laserTurretData;//激光炮塔数据
    public TurretData missileTurretData;//导弹炮塔数据
    public TurretData standardTurretData;//标准炮塔数据

    public Text moneyText;//拥有金钱文本

    public Animator moneyAnimator;//金钱动画

    private int money = 1000;//初始金钱

    //表示当前选择的炮台（要建造的炮台）
    private TurretData selectedTurretData;

    void ChangeMoney(int change=0)//金钱改变方法
    {
        money += change;
        moneyText.text = "￥" + money;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                //开发炮台的建造
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider=Physics.Raycast(ray,out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    mapCube MapCube = hit.collider.GetComponent<mapCube>();//得到点击的mapCube
                    if (selectedTurretData != null && MapCube.turretGo == null)
                    {
                        //可以创建
                        if (money >= selectedTurretData.cost)
                        {
                            ChangeMoney(-selectedTurretData.cost);
                            MapCube.BuildTurret(selectedTurretData.turretPrefab);

                        }
                        else
                        {
                            //TODO提示钱不够
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else
                    {
                        //TODO 升级处理
                    }
                }
            }
        }
    }

    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }
    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }
    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }
}
