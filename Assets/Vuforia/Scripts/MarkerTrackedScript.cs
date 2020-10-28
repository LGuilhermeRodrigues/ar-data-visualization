using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyNameSpace {

    public class MarkerTrackedScript : MonoBehaviour
    {

        private int dataQuery;

        // Start is called before the first frame update
        public void Start()
        {
            Debug.Log("Hello there from the MarkerTrackedScript");
            int[] data = { 1, 2, 3, 5, 5, 6, 3, 1 };
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}
