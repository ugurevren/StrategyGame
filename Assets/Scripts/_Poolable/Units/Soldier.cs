using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

namespace _Poolable.Units
{
    public class Soldier : Unit
    {
        private List<Vector3> _path;
        private void Start()
        {
            OnRightClick.AddListener(OnRightClickHandler);
        }

        private void OnRightClickHandler()
        {
            StartMovement();
        }
        private void StartMovement()
        {
            var worldPosition = GridTester.Instance.GetMouseWorldPosition();
            Grid<GridObject>.Instance.GetXY(worldPosition, out var x, out var y);
            switch (Grid<GridObject>.Instance.GetGridObject(x, y).Type)
            {
                case GridObject.GridType.Empty:
                    MoveAndAttack(worldPosition, false);
                    break;
                case GridObject.GridType.Enemy:
                    MoveAndAttack(worldPosition, true);
                    break;
            }
        }

        private void MoveAndAttack(Vector3 targetPosition, bool attack)
        {
            // Convert world position to grid coordinates
            var startNode = Pathfinding.Instance.GetGrid().GetGridObject(transform.position);
            var endNode = Pathfinding.Instance.GetGrid().GetGridObject(targetPosition);


            // Find the path to the target
            var gridPath = Pathfinding.Instance.FindPath(startNode.x, startNode.y, endNode.x, endNode.y);
            if (gridPath != null)
            {
                _path = new List<Vector3>();
                foreach (var node in gridPath)
                {
                    _path.Add(node.GetWorldPosition());
                }
            }

            FollowPath();
            var initPos = Grid<GridObject>.Instance.GetWorldPosition(_origin.x, _origin.y);
            GridTester.Instance.SetGridType(initPos, GridObject.GridType.Empty);
            _origin = new Vector2Int(endNode.x, endNode.y);
        }

        private void AttackEnemy(GridObject gridObject)
        {
            var enemy = gridObject.GetPoolable() as Unit;
            var isDead =false;
            while (!isDead)
            {
                isDead = enemy.TakeDamage(unitData.damage);
            }
            Pool.Instance.ReturnToPool("Enemy",enemy);
            GridTester.Instance.SetGridType(enemy.transform.position, GridObject.GridType.FriendlyUnit, this);
        }

        private void FollowPath()
        {
            if (_path != null)
            {
                StartCoroutine(FollowPathCoroutine());
            }
        }

        private IEnumerator FollowPathCoroutine()
        {
            var endPosition = _path[_path.Count - 1];
            for (int i = 1; i < _path.Count; i++)
            {
                var startPosition = transform.position;
                endPosition = _path[i];
                var distance = Vector3.Distance(startPosition, endPosition);
                var time = distance / 5f;
                var elapsedTime = 0f;
                while (elapsedTime < time)
                {
                    transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / time));
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }

            _path.Clear(); // Clear the path list after the soldier has finished moving
            var gridObject = Grid<GridObject>.Instance.GetGridObject(endPosition);
            switch (gridObject.Type)
            {
                case GridObject.GridType.Empty:
                    GridTester.Instance.SetGridType(endPosition, GridObject.GridType.FriendlyUnit, this);
                    break;
                case GridObject.GridType.Enemy:
                    AttackEnemy(gridObject);
                    break;
                case GridObject.GridType.Building:
                
                    break;
            
            }
        }
    }
}
