using CommanTickManager;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Games.ArchvizVR
{
	using UnityEngine;
	using System;
	using System.Collections;

	public class TVDisplayPopup : UISystem.Popup,ITick
	{

		#region PRIVATE_VARS
		[SerializeField] private VideoPlayer _videoPlayer;
		[SerializeField] private Sprite playIcon;
		[SerializeField] private Sprite pauseIcon;
		[SerializeField] private Image _videoButtonImage;
		#endregion

		#region PUBLIC_VARS

		#endregion

		#region UNITY_CALLBACKS

		private void OnEnable()
		{
			_videoPlayer.prepareCompleted += OnVideoPrepared;
			ProcessingUpdate.Instance.Add(this);
		}
		
		public void Tick()
		{
			if (_videoPlayer.isPlaying && _videoButtonImage.sprite != pauseIcon)
			{
				_videoButtonImage.sprite = pauseIcon;
			}
			else if (!_videoPlayer.isPlaying && _videoButtonImage.sprite != playIcon)
			{
				_videoButtonImage.sprite = playIcon;
			}
		}
		private void OnDisable()
		{
			_videoPlayer.prepareCompleted -= OnVideoPrepared;
			ProcessingUpdate.Instance.Remove(this);
		}

		#endregion

		#region PUBLIC_METHODS
		
		public override void Show()
		{
			_videoButtonImage.gameObject.SetActive(true);
			base.Show();
		}

		public override void Hide()
		{
			if (_videoPlayer.isPlaying)
			{
				_videoButtonImage.gameObject.SetActive(false);
				return;
			}
			_videoButtonImage.gameObject.SetActive(true);

			base.Hide();
		}

		public void OnVideoClick()
		{
			if(_videoPlayer.isPlaying)
			{
				_videoPlayer.Pause();
				return;
			}

			if (_videoPlayer.isPrepared)
			{
				_videoPlayer.Play();
			}
			_videoPlayer.Prepare();
		}
		#endregion

		#region PRIVATE_METHODS

		private void OnVideoPrepared(VideoPlayer vp)
		{
			vp.Play();
		}
		#endregion
		
	}
}