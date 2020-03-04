using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maxence;

namespace Maxence
{
    public class PlayerInput : MonoBehaviour
    {
        public CarController controller;

        void Update()
        {
            controller.horizontalInput = Input.GetAxis("Horizontal");
            controller.verticalInput = Input.GetAxis("Vertical");
        }
    }
}
