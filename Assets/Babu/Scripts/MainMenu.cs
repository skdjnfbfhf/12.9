using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Babu
{
    public class MainMenu : MonoBehaviour
    {

        public GameObject MenuBack;
        public GameObject Manual;
        public GameObject Story;




        public void BtnManual()
        {
            MenuBack.GetComponent<Animator>().SetTrigger("Close");
            Invoke("OpenManual", 1.5f);
        }

        public void BtnStory()
        {
            MenuBack.GetComponent<Animator>().SetTrigger("Close");
            Invoke("OpenStory", 1.5f);
        }

        void OpenManual()
        {
            Manual.SetActive(true);
            Manual.GetComponent<Animator>().SetTrigger("Open");
        }

        void OpenMenuBack()
        {
            MenuBack.GetComponent<Animator>().SetTrigger("Open");
        }

        void OpenStroy()
        {
            Story.SetActive(true);
            Story.GetComponent<Animator>().SetTrigger("Open");
        }


    }
}
