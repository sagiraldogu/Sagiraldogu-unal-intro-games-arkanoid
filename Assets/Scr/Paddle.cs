using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _movementLimit = 7; 
    private Vector3 _targetPosition;// variable que permite tener la ultima posicion 
    
    private Camera _cam;  // identifica la camera 
    private Camera Camera {
        get{
            if (_cam == null)
            {
                _cam = Camera.main;
            }
            return _cam;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _targetPosition.x = Camera.ScreenToWorldPoint(Input.mousePosition).x; //permtite identificar la posicion del mouse 
        _targetPosition.x = Mathf.Clamp(_targetPosition.x, -_movementLimit, _movementLimit); //crea un rango 
        _targetPosition.y = this.transform.position.y;
   
   transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _speed);
    }
}
