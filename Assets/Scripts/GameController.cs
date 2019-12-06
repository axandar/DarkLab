using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
	[SerializeField] private UnityEvent GameEndEvent;

	public void GameEnded() {
		GameEndEvent.Invoke();
	}
}
