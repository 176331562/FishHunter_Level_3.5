using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    void Start()
    {
        int[] array = new int[20];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = Random.Range(0, 101);
        }

        for (int step = array.Length/2; step > 0; step/=2)
        {
            for (int i = step; i < array.Length; i++)
            {
                int sortIndex = i - step;
                int nowSort = array[i];

                while (sortIndex >= 0 && array[sortIndex] > nowSort)
                {
                    array[sortIndex + step] = array[sortIndex];
                    sortIndex -= step;
                }

                array[sortIndex + step] = nowSort;
            }
        }
        for (int i = 0; i < array.Length; i++)
        {
            Debug.LogError(array[i]);
        }
        
    }


}
