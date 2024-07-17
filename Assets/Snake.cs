using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    int skor = 0;
    public TextMeshProUGUI skor_text;

    private Vector2 _direction;

    private List<Transform> _segment=new List<Transform>();

    public Transform segmentPrefab;

    public int initialSize = 4;//baslangic boyutu

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if(Input.GetKeyDown(KeyCode.S)) 
        {
            _direction = Vector2.down;  
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right; 
        }
    }
    private void FixedUpdate()
    {
        for(int i=_segment.Count-1; i>0;i--) // her seferinde yilanin indexi degisir
        {
            _segment[i].position = _segment[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x)+ _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f);
    }
    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segment[_segment.Count - 1].position;

        _segment.Add(segment);
    }

    private void ResetState() //oyuncu kaybedince durumu sifirlar
    {
        for (int i=1;i<_segment.Count; i++)
        {
            Destroy(_segment[i].gameObject);
        }
        _segment.Clear();
        _segment.Add(this.transform);

        for(int i=1;i<this.initialSize;i++)
        {
            _segment.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero; // durum sifirlaninca x, y ve z bilesenleri hepsi 0'da
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food") // yemege carpinca Grow fonksiyonuna git
        {
            Grow();
            skor+=10;
            skor_text.text = skor.ToString();
        }
        else if(collision.tag=="Obstacle") // engel yani duvarlara carpinca ResetState fonksiyonuna git
        {
            ResetState();
            skor = 0;
            skor_text.text = skor.ToString();
        }

    }
}
