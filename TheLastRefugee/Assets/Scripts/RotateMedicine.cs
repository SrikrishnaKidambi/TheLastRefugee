using UnityEngine;

public class CollectibleRotate : MonoBehaviour
{
    [SerializeField] int rotateSpeed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    //this is a new comment being written
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}