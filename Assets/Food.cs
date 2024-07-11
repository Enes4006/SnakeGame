using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foof : MonoBehaviour
{
    public BoxCollider2D GridArea;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        Bounds bounds = this.GridArea.bounds;
        float x=Random.Range(bounds.min.x,bounds.max.x); // x ekseninde mevcut konuma yuvarlanır
        float y=Random.Range(bounds.min.y,bounds.max.y); // y ekseninde mevcut konuma yuvarlanır

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            RandomizePosition();
        }
        
    }
}
