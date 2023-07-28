using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace keysystem
{
    public class keyraycast : MonoBehaviour
   {
        [SerializeField] private int raylength = 5;
        [SerializeField] private LayerMask layermaskinteract;
        [SerializeField] private string exclusivelayername = null;

        private key raycastedobject;
        [SerializeField] private KeyCode opendoorkey = KeyCode.Mouse0;
        [SerializeField] private Image crosshair = null;

        private bool iscrosshairactive;
        private bool doonce;

        private string interactabletag = "interactiveobject";

        private void Update()
        {
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            int mask = 1 << LayerMask.NameToLayer(exclusivelayername) | layermaskinteract.value;

            if (Physics.Raycast(transform.position, fwd, out hit, raylength, mask)) 
            {
                if (hit.collider.CompareTag(interactabletag))
                {
                    if (!doonce)
                    {
                        raycastedobject = hit.collider.gameObject.GetComponent<key>();
                        crosshairchange(true);
                    }
                    iscrosshairactive = true;
                    doonce = true;

                    if (Input.GetKeyDown(opendoorkey))
                    {
                        raycastedobject.objectinteraction();
                    }

                }
            }
            else
            {
                if (iscrosshairactive)
                {
                    crosshairchange(false);
                    doonce = false;
                }
            }
        }
        void crosshairchange(bool on)
        {
            if ( on && !doonce)
            {
                crosshair.color = Color.red; 
            }
            else
            {
                crosshair.color = Color.white;
                iscrosshairactive = false; 
            }
                 
        }
    }

}

