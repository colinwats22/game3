using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace keysystem
{


    public class key : MonoBehaviour
    {
        [SerializeField] private bool frontdoor = false;
        [SerializeField] private bool frontkey = false;
        [SerializeField] private keyscript _keyscript = null;

        private keydoorcontroller doorobject;

        private void Start()
        {
            if (frontdoor)
            {
                doorobject = GetComponent<keydoorcontroller>(); 
            }
        }
        public void objectinteraction()
        {
            if (frontdoor)
            {
                doorobject.playanimation();
            }
            else if (frontkey)
            {
                _keyscript.hasfrontkey = true;
                gameObject.SetActive(false); 
            }
        }
    }
}