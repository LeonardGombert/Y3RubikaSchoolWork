using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leonard;

namespace Leonard
{
    public class Requirements : MonoBehaviour, IComparable<Requirements>
    {
        public List<int> listOfInts = new List<int>();
        public List<int> list2 = new List<int>();

        public int[] arrayOfInt;
        public int[] arrayOfInt2;

        public int value;

        public int[][] my2DJaggedArray = new int[4][];
        public int[][][] my3DJaggedArray = new int[4][][];

        public LayerMask selectedLayerMask;

        // Start is called before the first frame update
        void Start()
        {
            TestList();
            TestArray();

            TestJaggedArray();
        }

        // Update is called once per frame
        void Update()
        {
            FireRaycast();
        }

        void TestList()
        {
            listOfInts.Add(123);
            listOfInts.Add(13);
            listOfInts.Add(23);
            listOfInts.Add(3);

            list2 = listOfInts;

            listOfInts.RemoveAt(1);
        }

        private void TestArray()
        {
            arrayOfInt = new int[4];
            arrayOfInt[0] = 123;
            arrayOfInt[1] = 13;
            arrayOfInt[2] = 23;
            arrayOfInt[3] = 3;

            arrayOfInt2 = arrayOfInt;

            arrayOfInt[0] = 99;
        }

        private void TestJaggedArray()
        {
            #region 2D Jagged Array
            //create a "staircase" array
            for (int i = 0; i < my2DJaggedArray.Length; i++)
            {
                my2DJaggedArray[i] = new int[i + 1];
            }

            //another example of a variable-length array
            my2DJaggedArray[0] = new int[2];        // // // //    
            my2DJaggedArray[1] = new int[4];        // //    //        -> looks like this
            my2DJaggedArray[2] = new int[1];           //    //
            my2DJaggedArray[3] = new int[5];           //    //
                                                       //  
            #endregion

            #region 3D Jagged Array
            my3DJaggedArray[0] = new int[3][];
            my3DJaggedArray[0][0] = new int[9];     //hop "inside" of the first dimension and
            my3DJaggedArray[0][1] = new int[5];     //define the intern dimensions (y and z)
            my3DJaggedArray[0][2] = new int[1];

            my3DJaggedArray[1] = new int[3][];
            my3DJaggedArray[1][0] = new int[6];
            my3DJaggedArray[1][1] = new int[3];
            my3DJaggedArray[1][2] = new int[7];

            my3DJaggedArray[2] = new int[3][];
            my3DJaggedArray[1][0] = new int[36];     //you don't have to maintain the same amount of x values, but
            my3DJaggedArray[1][1] = new int[84];     //doing so will give you a cubic matrix by the end
            my3DJaggedArray[1][2] = new int[23];

            my3DJaggedArray[3] = new int[3][];
            my3DJaggedArray[1][0] = new int[1];
            my3DJaggedArray[1][1] = new int[8];
            my3DJaggedArray[1][2] = new int[4];

            //my3DJaggedArray[0][0][0] = 99;
            #endregion
        }


        void FireRaycast()
        {
            Vector3 pos = transform.position;
            Vector3 direction = Vector3.up;

            RaycastHit hit;

            float maxDistance = 7f;

            Debug.DrawRay(pos, direction * maxDistance, Color.red);

            if (Physics.Raycast(pos, direction, out hit, maxDistance, selectedLayerMask))
            {
                Debug.DrawRay(pos, direction * maxDistance, Color.green);
            }
        }

        public int CompareTo(Requirements other)
        {
            if (value < other.value) return 1;

            else if (value > other.value) return -1;

            else return 0;
        }
    }
}
