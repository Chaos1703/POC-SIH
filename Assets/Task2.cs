using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Task2 : MonoBehaviour
{
    public GameObject[] balls;
    public GameObject Text;
    private int count = 3;

    public void updateCount()
    {
        count--;
        Text.GetComponent<TextMeshProUGUI>().text = "Balls left: " + count;
    }
}
