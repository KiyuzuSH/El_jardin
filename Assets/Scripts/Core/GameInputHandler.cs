using UnityEngine;
using UnityEngine.SceneManagement;

namespace KiyuzuDev.ITGWDO.Core {
	/// <summary>
	/// A delegate layer for handling player inputs in the AVG view.
	/// </summary>
	/// <remarks>
	/// Author: Nianyi Wang
	/// Creation time: 05/31/2024 02:34
	/// </remarks>
	[RequireComponent(typeof(GameManager))]
	public class GameInputHandler : MonoBehaviour {
		#region Message handlers
		protected void OnReturnHome() {
			ReturnHome();
		}
		#endregion

		#region Interfaces
		/// <summary>
		/// End the current line and trigger the rest logic.
		/// </summary>
		public void ReturnHome()
		{
			Debug.Log("Return Home Page");
			if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
				LegacySceneLoader.Instance.LoadScene(0);
			else return;
			ScriptManager.Instance.SetLineById(1);
		}
		#endregion
	}
}
