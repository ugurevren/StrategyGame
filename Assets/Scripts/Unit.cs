using System.Collections;
using System.Collections.Generic;
using GridSystem;
using Interfaces;
using UnityEngine;

public class Unit : Poolable, IAttackable
{
    public UnitData unitData;
    public int width;
    public int height;
    public int health = 10;
    
        public GameObject target;
        private Pathfinding _pathfinding;
        public Sprite _sprite;
        private SpriteRenderer _spriteRenderer;
        private List<Vector3> path;
    
    
        private void Start()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = _sprite;
            // TODO bu ne? fmlkdsmlkf
            _pathfinding=Pathfinding.Instance;
        }   

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !GridTester.Instance.GetBuildingMode()&&!GridTester.Instance.GetProductionMode())
            {
                var worldPosition = GridTester.Instance.GetMouseWorldPosition();
                Grid<GridObject>.Instance.GetXY(worldPosition, out var x, out var y);
                if (x == _origin.x && y == _origin.y)
                {
                    StartCoroutine(WaitForSecondInput());
                }

            }
        }
        public void Initialize(Vector2Int gridPos)
        {
            // Initialization logic
            transform.position = Grid<GridObject>.Instance.GetWorldPosition(gridPos.x, gridPos.y);
            // Set other properties like origin or grid position
        }
        
        private IEnumerator WaitForSecondInput()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(1)); // Wait for left mouse button click

            var worldPosition = GridTester.Instance.GetMouseWorldPosition();
            Grid<GridObject>.Instance.GetXY(worldPosition, out var x, out var y);
            switch (Grid<GridObject>.Instance.GetGridObject(x,y).Type)
            {
                case GridObject.GridType.Empty:
                    MoveAndAttack(worldPosition,false);
                    break;
                case GridObject.GridType.Enemy:
                    MoveAndAttack(worldPosition,true);
                    break;
            }
       
        }

        public void SetUnitType(UnitData data)
        {
            unitData = data;
        }

        private void MoveAndAttack(Vector3 targetPosition,bool attack)
        {
            // Convert world position to grid coordinates
            var startNode = _pathfinding.GetGrid().GetGridObject(transform.position);
            var endNode = _pathfinding.GetGrid().GetGridObject(targetPosition);
        
        

       
            // Find the path to the target
            var gridPath = _pathfinding.FindPath(startNode.x, startNode.y, endNode.x, endNode.y);
            if (gridPath != null)
            {
                path = new List<Vector3>();
                foreach (var node in gridPath)
                {
                    path.Add(node.GetWorldPosition());
                }
            }
            FollowPath();
            UpdateOrigin(endNode.x,endNode.y);
     
        
        }
        private void Attack()
        {
            var dead = false;
            while (!dead)
            {
                //TODO attack
            }
        
        
        }
    
        private void FollowPath()
        {
            if (path != null)
            {
                StartCoroutine(FollowPathCoroutine());
            }
        }
    
        private IEnumerator FollowPathCoroutine()
        {
            var endPosition = path[path.Count - 1];
            for (int i = 1; i < path.Count; i++)
            {
                var startPosition = transform.position;
                endPosition = path[i];
                var distance = Vector3.Distance(startPosition, endPosition);
                var direction = (endPosition - startPosition).normalized;
                var time = distance / 5f;
                var elapsedTime = 0f;
                while (elapsedTime < time)
                {
                    transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / time));
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }
            path.Clear(); // Clear the path list after the soldier has finished moving
            if (Grid<GridObject>.Instance.GetGridObject(endPosition).Type == GridObject.GridType.Enemy)
            {
                Attack();
            }
            
        }
        private void UpdateOrigin(int x, int y)
        {
            _origin = new Vector2Int(x, y);
        }
    
        public void SetOrigin(Vector2Int origin)
        {
            _origin = origin;
        }
    
    public bool TakeDamage(int damage, out bool isDead)
    {
        Debug.Log("Unit Take Damage");
        return isDead = true;
    }
}
