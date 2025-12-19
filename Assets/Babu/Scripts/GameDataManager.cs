using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Babu
{
    public class GameDataManager : Singleton<GameDataManager>
    {
        public float gameTime = 0;
        public int gameScore;
        public string curld;

        //플레이이에 대한 정보
        public float bombTime = 2.0f;
        public float bombing = 0f;
        public bool isBomb = false;
        public float NoBombSpeed = 1.0f;

        public float fixTime = 60.0f;
        public float fixing = 0f;
        public bool isFix = false;
        public float NoFixSpeed = 1.0f;

        public float hp = 10f;
        public float maxHp = 10f;
        public int upgrade = 0;
        public int maxUpgrade = 3;
        public int bomb = 0;
        public int maxBomb = 3;

        private void Start()
        {
            SaveData();
        }



        public void SaveData()
        {
            if(PlayerPrefs.HasKey("id"))
            {
                string id = PlayerPrefs.GetString("id");
                Debug.Log(id);
            }
        }

    }
}
