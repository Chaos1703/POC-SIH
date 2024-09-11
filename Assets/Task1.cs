using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Added for TextMeshPro

public class Task1 : MonoBehaviour
{
    public GameObject[] Text;
    public GameObject[] Trees;

    public float gazeDuration = 5f;  // Time required to gaze at the tree
    private float gazeTimer = 0f;
    private int treeCount = 0;
    private GameObject currentTree;
    public Camera mainCamera;    // Main camera from the XR rig

    void Update()
    {
        CheckGaze();
        endTask();
    }

    void CheckGaze()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        // Perform a raycast to detect what the user is looking at
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if the user is looking at a tree
            if (IsTree(hitObject))
            {
                gazeTimer += Time.deltaTime;

                // If the gaze lasts for the required duration and the tree hasn't been counted
                if (gazeTimer >= gazeDuration && currentTree != hitObject)
                {
                    treeCount++;
                    ShowTreeCount(hitObject, treeCount);
                    currentTree = hitObject;  // Mark this tree as counted
                    gazeTimer = 0f;  // Reset the timer
                }
            }
            else
            {
                gazeTimer = 0f;  // Reset timer if not looking at a tree
                currentTree = null;  // Reset currentTree if not looking at a tree
            }
        }
        else
        {
            gazeTimer = 0f;  // Reset if not hitting any object
            currentTree = null;  // Reset currentTree if not hitting any object
        }
    }

    // Check if the object is one of the trees
    bool IsTree(GameObject obj)
    {
        foreach (GameObject tree in Trees)
        {
            if (obj == tree)
                return true;
        }
        return false;
    }

    // Show the tree count UI next to the tree
    void ShowTreeCount(GameObject tree, int count)
    {
        int treeIndex = System.Array.IndexOf(Trees, tree);

        if (treeIndex != -1 && treeIndex < Text.Length)
        {
            Text[treeIndex].SetActive(true);  // Show the corresponding UI text
            TextMeshProUGUI textComponent = Text[treeIndex].GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = " " + count;
            }
        }
    }

    void endTask()
    {
        foreach (GameObject textObj in Text)
        {
            if (!textObj.activeSelf)
            {
                return;  // If any text object is not active, return early
            }
        }
        foreach (GameObject obj in Text)
        {
            obj.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

}