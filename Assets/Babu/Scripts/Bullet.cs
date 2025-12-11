using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Babu
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private Vector3 destination;
        [SerializeField]
        private bool isThrow = false;
        public float speed = 1.0f;
        
        //방향
        public Vector3 dir;

        void Update()
        {
            if (isThrow)
            {
                //방향계산에 따른 조준탄
                this.transform.position += dir.normalized * Time.deltaTime * speed;
            }
        }

        public void SetBullet(Vector3 _destination)
        {
            destination = _destination;
            dir = destination - this.transform.position;
            isThrow = true;

            //방향계산
            dir = destination - this.transform.position;
        }
    }
}
