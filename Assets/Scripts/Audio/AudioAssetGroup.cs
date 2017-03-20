using UnityEngine;
using System.Collections.Generic;

namespace Audio
{
	public class AudioAssetGroup : ScriptableObject
	{
		public IEnumerable<AudioAsset> AudioAssets { get { return audioAssets; } }

		[SerializeField] private AudioAsset[] audioAssets;
	}
}