using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Babu
{
    public class Background : MonoBehaviour
    {
        public float mapSpeed;
        public float mapSizeZ;
        private Vector3 startPos;

        void Start()
        {
            startPos = this.transform.position;
            //mapSizeZ = this.transform.localScale.y;
        }

        private void Update()
        {
            float newPosition = Mathf.Repeat(this.transform.position.z +
               Time.deltaTime * mapSpeed, mapSizeZ);
            transform.position = new Vector3(startPos.x, startPos.y,
                newPosition);
        }

    }
}
