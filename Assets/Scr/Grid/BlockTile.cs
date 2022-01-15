using UnityEngine;

public enum BlockType
{
    Small,
    Big
}

public enum BlockColor
{
    Green,
    Blue,
    Orange,
    Red,
    Purple
}

public class BlockTile : MonoBehaviour
{
    private const string BLOCK_BIG_PATH = "Sprites/BlockTiles/Big/Big_{0}_{1}";
    private const string BLOCK_SMALL_PATH   = "Sprites/BlockTiles/Small/Small_{0}_{1}";
    
    [SerializeField] 
    private BlockType _type = BlockType.Big;
    [SerializeField] 
    private BlockColor _color = BlockColor.Green;
    [SerializeField]
    private int _score = 10;
    
    public int Score => _score;
    
    private SpriteRenderer _renderer;
    private Collider2D _collider;
    
    private int _totalHits = 1;
    private int _currentHits = 0;
    private int _id;

     public void SetData(int id, BlockColor color)
    {
        _id = id;
        _color = color;
        
    }

    public void Init()
    {
        _currentHits = 0;
        _totalHits = _type == BlockType.Big ? 2 : 1;

        _collider = GetComponent<Collider2D>();
        _collider.enabled = true;
        
        _renderer = GetComponentInChildren<SpriteRenderer>();

        _renderer.sprite =GetBlockSprite(_type, _color, 0);
        
    }
    
    public void OnHitCollision(ContactPoint2D contactPoint)
    {
        _currentHits++;
        if (_currentHits >= _totalHits)
        {
            _collider.enabled = false;
            gameObject.SetActive(false);
            ArkanoidEvent.OnBlockDestroyedEvent?.Invoke(_id);
        }
        else
        {
            _renderer.sprite = GetBlockSprite(_type, _color, _currentHits);
        }
    }
    
    static Sprite GetBlockSprite(BlockType type, BlockColor color, int state)
    {
        string path = string.Empty;
        if (type == BlockType.Big)
        {
            path = string.Format(BLOCK_BIG_PATH, color, state);
        }
                if (type == BlockType.Small)
        {
            path = string.Format(BLOCK_SMALL_PATH, color, state);
        }


        if (string.IsNullOrEmpty(path))
        {
            return null;
        }
    
        return Resources.Load<Sprite>(path);

    }
}