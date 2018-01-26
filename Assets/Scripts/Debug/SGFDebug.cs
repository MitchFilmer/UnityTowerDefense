using UnityEngine;

using System.Diagnostics;
using System.IO;

namespace SGF
{
	public static class Debug
	{        
		public enum Channel
		{
			System = 0,
			Data = 1,
			Audio = 2,
			Art = 3,
            VR = 4,
            AI = 5,
            Gameplay = 6,
		};

		private static string[] _textColor = {
			"white",
			"cyan",
			"green",
			"magenta",
            "lime",
            "yellow",
            "aqua",
			};
        
        private const string _debugFlag = "SGF_CONDITIONAL_LOGGING";

        [Conditional(_debugFlag)]
		public static void Log(string msg, Channel ch = Channel.System)
		{
			UnityEngine.Debug.Log(string.Format("<color={0}>{1}</color>", _textColor[(int)ch], msg));
		}

        [Conditional(_debugFlag)]
        public static void LogToFile(string msg, string filename, string extension = "txt")
		{
			string root = Application.persistentDataPath;
			string path = root + string.Format("/{0}.{1}", filename, extension);

			File.WriteAllText(path, msg);
		}
	}
}