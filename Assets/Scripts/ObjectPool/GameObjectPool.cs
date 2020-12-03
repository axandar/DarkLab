using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectPool {
	public class GameObjectPool<T> where T: MonoBehaviour {
		private List<T> _activeObjects;
		private List<T> _pooledObjects;

		private Action<T> _onPooled;
		private Action<T> _onReturned;
		private Action<T> _onInstantiated;
		
		private readonly int _persistentObjectsAmount;
		private GameObject _objectPrefab;
		
		public GameObjectPool(int persistentObjectsAmount, GameObject objectPrefab, Action<T> onInstantiated, Action<T> onPooled, Action<T> onReturned) {
			_persistentObjectsAmount = persistentObjectsAmount;
			_objectPrefab = objectPrefab;
			if (onInstantiated != null) {
				_onInstantiated = onInstantiated;
			}
			if (onPooled != null) {
				_onPooled = onPooled;
			}
			if (onReturned != null) {
				_onReturned = onReturned;
			}
			InitializeLists();
		}
		
		private void InitializeLists() {
			_activeObjects = new List<T>();
			_pooledObjects = new List<T>();
		}
		
		///<summary>Returns object from pooled objects or instantiates new if pool is empty</summary>
		public T GetObject() {
			var returnedObj = GetFromPooledOrInstantiate();
			
			returnedObj.gameObject.SetActive(true);
			_activeObjects.Add(returnedObj);
			_onReturned?.Invoke(returnedObj);

			return returnedObj;
		}

		private T GetFromPooledOrInstantiate() {
			T returnedObj;
			if (_pooledObjects.Count > 0) {
				returnedObj = _pooledObjects.Last();
				_pooledObjects.Remove(returnedObj);
			} else {
				returnedObj = GameObject.Instantiate(_objectPrefab, Vector3.zero, Quaternion.identity)
					.GetComponent<T>();
				_onInstantiated?.Invoke(returnedObj);
			}
			return returnedObj;
		}
		
		///<summary>Returns object to object pool or destroys gameobject if pool is at _persistentObjectsAmount</summary>
		public void ReturnObject(T obj) {
			_activeObjects.Remove(obj);
			if (_pooledObjects.Count >= _persistentObjectsAmount) {
				DestroyGameobject(obj);
			} else {
				_onPooled?.Invoke(obj);
				obj.gameObject.SetActive(false);
				_pooledObjects.Add(obj);
			}
		}	
		
		public void DestroyAllObjects() {
			DestroyObjectsInList(_activeObjects);
			DestroyObjectsInList(_pooledObjects);
		}

		private void DestroyObjectsInList(List<T> _objects) {
			foreach (var obj in _objects) {
				DestroyGameobject(obj);
			}
		}

		private void DestroyGameobject(T gameObjectToDestroy) {
			GameObject.Destroy(gameObjectToDestroy.gameObject);
		}
	}
}