using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var gameObjet = new GameObject();
        var spriteRederer = gameObject.AddComponent<SpriteRenderer>();
        var tex = Resources.Load<Texture2D>("Assets/Img/GAME/NoteSkin/00/UpRight Hold Body Active 6x1.png");
        var sprite = Sprite.Create(tex, new Rect(0, 0, 32, 32), new Vector2(16, 16));
        spriteRederer.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
