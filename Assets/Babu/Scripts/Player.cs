using Codice.CM.Client.Differences;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



        void Start()
        {
            thisRigi = this.GetComponent<Rigidbody>();
        }


        void Update()
        {
            Move();
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
    }
}
