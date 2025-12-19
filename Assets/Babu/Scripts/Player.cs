using Codice.CM.Client.Differences;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Common.EventTracking.TrackFeatureUseEvent.Features.DesktopGUI.Filters;

namespace Babu
{
    public class Player : MonoBehaviour
    {
        
        //총알 딜레이
        public float bulletTime = 0.1f;
        //총알 딜레이만큼 시간이 지나갔는지 체크
        public float reloadTime = 0f;
        Rigidbody thisRigi;
        //플레이어의 이동속도
        public float speed = 2.0f;
        //총알 프리팹
        public GameObject objButllet;
        //총알이 생성될 위치
        public Transform BulletPoint;

        public int hp = 1;
        public int maxHp = 1;

        GameManager gameManager;

        public GameObject[] item;



        void Start()
        {
            GameObject gamManagerObject =
                GameObject.FindGameObjectWithTag("GameManager");
            if (gamManagerObject != null)
            {
                gameManager = gamManagerObject.GetComponent<GameManager>();
            }
            if (gameManager == null)
            {
                Debug.LogError("게임 매니저가 존재하지 않습니다.");

            }
            thisRigi = this.GetComponent<Rigidbody>();
        }


        void Update()
        {
            Move();
            fireBullet();   
        }

        private void Move()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(moveX, 0, moveZ);
            thisRigi.velocity = move * speed;

            //현재 플레이의 위치의 월드 좌표계를 스크린 좌표계로 바꾼다
            Vector3 poslnWorld = Camera.main.WorldToScreenPoint(this.transform.position);

            //스크린 좌표계에서 움직일 수 있는 범위를 제한한다.
            float posX = Mathf.Clamp(poslnWorld.x, 0, Screen.width);
            float posZ = Mathf.Clamp(poslnWorld.y, 0, Screen.height);

            // 제한된 이동을 다시 월드 좌표계로 변경한다
            Vector3 poslnScreen = Camera.main.ScreenToWorldPoint(new Vector3(posX, posZ, 0));
            
            //이동시키다
            thisRigi.position = new Vector3(poslnScreen.x, 0, poslnScreen.z);

        }

        void fireBullet()
        {
            reloadTime += Time.deltaTime; // 총알 재장전 시간

            if(Input.GetButton("Fire1") && (bulletTime <= reloadTime))
            {
                reloadTime = 0f;
                GameObject bullet = Instantiate(objButllet, BulletPoint.position, this.transform.rotation);
                bullet.GetComponent<Bullet>().SetBullet(BulletPoint.position + Vector3.forward); 

            }

        }

        void fireBullet2()
        {
            if(Input.GetButtonDown("Fire2") && (GameDataManager.Instance.bombTime 
                <= GameDataManager.Instance.bombing))
            {
                if(GameDataManager.Instance.bomb == 0)
                {
                    GameDataManager.Instance.isBomb = true;
                }
                else
                {
                    GameDataManager.Instance.bomb--;
                    GameDataManager.Instance.bombing = 0;
                    for(int i = 0; i < gameManager.listEnemys.Count; i++)
                    {
                        if (gameManager.listEnemys[i].GetComponent<Enemy>() != null)
                        {
                            gameManager.listEnemys[i].GetComponent<Enemy>().hp -= 1;
                            if (gameManager.listEnemys[i].GetComponent<Enemy>().hp  < 0)
                            {
                                gameManager.listEnemys[i].GetComponent<Enemy>().InitItem();
                                Destroy(gameManager.listEnemys[i].gameObject);
                            }
                        }
                        else
                        {
                        }
                    }
                }
            }

        }





        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Bullet"))
            {
                GameDataManager.Instance.hp -= 1;
                if (GameDataManager.Instance.hp < 1)
                {
                    Destroy(gameObject);
                }
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Enemy"))
            {
                GameDataManager.Instance.hp -= 1;
            }

            if (other.CompareTag("Item"))
            {
                switch (other.GetComponent<Item>().itemStatus)
                {
                    case ItemStatus.hp:
                        if(GameDataManager.Instance.hp < GameDataManager.Instance.maxHp)
                        {
                            GameDataManager.Instance.hp += 1f;
                        }
                        break;
                    case ItemStatus.upgrade:
                        if (GameDataManager.Instance.upgrade < GameDataManager.Instance.maxUpgrade)
                        {
                            GameDataManager.Instance.upgrade++;
                        }
                        break;
                    case ItemStatus.bomb:
                        if (GameDataManager.Instance.bomb < GameDataManager.Instance.maxBomb)
                        {
                            GameDataManager.Instance.bomb++;
                        }
                        break;
                }
                Destroy(other.gameObject);
                return;
            }


        }

    }
}
