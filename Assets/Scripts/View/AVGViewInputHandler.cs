using UnityEngine;

namespace KiyuzuDev.ITGWDO.View {
	/// <summary>
	/// A delegate layer for handling player inputs in the AVG view.
	/// </summary>
	/// <remarks>
	/// Author: Nianyi Wang
	/// Creation time: 05/31/2024 02:34
	/// </remarks>
	[RequireComponent(typeof(AVGView))]
	public class AVGViewInputHandler : MonoBehaviour {
		#region Message handlers
		protected void OnEndLine() {
			EndLine();
		}
		#endregion

		#region Interfaces
		/// <summary>
		/// End the current line and trigger the rest logic.
		/// </summary>
		/// <remarks>
		/// TODO: If the current line is rolling, immediately show all texts.
		/// </remarks>
		public void EndLine() {
			var dialogue = Core.DialogueManager.Instance;
			if(dialogue == null) {
				Debug.LogWarning("Warning: Cannot end line because no dialogue manager is present.");
				return;
			}
			if (AVGView.Instance.haveTextTypewriter)
			{
				AVGView.Instance.SkipTextTypewriter();
				return;
			}
			if (AVGView.Instance.haveMindTypewriter)
			{
				AVGView.Instance.SkipMindTypewriter();
				return;
			}

			dialogue.MoveNextLine();
			dialogue.ProcessLine();
		}
		#endregion
	}
}
