using UnityEngine;

namespace KiyuzuDev.ITGWDO.View {
	/// <summary>
	/// A delegate layer for handling player inputs in the Collection view.
	/// </summary>
	/// <remarks>
	/// Author: Zhe Wang
	/// Creation time: 06/01/2024 04:41
	/// </remarks>
	[RequireComponent(typeof(CollectionView))]
	public class CollectionViewInputHandler : MonoBehaviour {
		#region Message handlers
		protected void OnSwitchType() {
			SwitchType();
		}
		#endregion

		private Canvas thisCanvas;

		private void Start()
		{
			thisCanvas = GetComponent<Canvas>();
		}

		#region Interfaces
		/// <summary>
		/// Switch the Visuable State of this Canvas Component
		/// </summary>
		private void SwitchType()
		{
			thisCanvas.enabled = !thisCanvas.enabled;
		}
		#endregion
	}
}
