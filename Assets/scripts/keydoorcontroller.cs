using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace keysystem
{


    public class keydoorcontroller : MonoBehaviour
    {
        private Animator dooranim; 
        private bool dooropen = false;
        [Header("animation names")]
        [SerializeField] private string openanimationname = "dooropen";
        [SerializeField] private string closeaniimationname = "doorclose";
        [SerializeField] private int timetoshowui = 1;
        [SerializeField] private GameObject showdoorlockedui = null;
        [SerializeField] private keyscript _keyscript = null;
        [SerializeField] private int waittimer = 1;
        [SerializeField] private bool pauseinteraction = false;
        public GameObject door; 
        // Start is called before the first frame update
 private void Awake()
        {
            dooranim = gameObject.GetComponent<Animator>(); 
        }

     private IEnumerator pausedoorinteraction()
        {
            pauseinteraction = true;
            yield return new WaitForSeconds(waittimer);
            pauseinteraction = false; 
        }

        public void playanimation()
        {
            if(_keyscript.hasfrontkey)
            {
                opendoor();
            }

//else if has another key can open door too
            else
            {
                StartCoroutine(showdoorlocked());

            }
        }

        void opendoor()
        {
            if (!dooropen && !pauseinteraction)
            {
                door.SetActive(false); 
                dooranim.Play(openanimationname, 0, 0.0f);
                dooropen = true;
                StartCoroutine(pausedoorinteraction());
            }
            else if (dooropen && !pauseinteraction)
            {
                dooranim.Play(closeaniimationname, 0, 0.0f);
                dooropen = false;
                StartCoroutine(pausedoorinteraction());
            }
        }
        IEnumerator showdoorlocked()
        {
            showdoorlockedui.SetActive(true);
            yield return new WaitForSeconds(timetoshowui);
            showdoorlockedui.SetActive(false); 
        }
    }
}