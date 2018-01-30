using System.Text;
using UnityEngine;

public class D
{

	/// <summary>
	/// Author: WeslomPo
	/// InspiredBy: Ant.Karlov
	/// </summary>

	public static void Log(params object[] aArgs) {
#if DEBUG || UNITY_EDITOR
		Pretty(aArgs).Log();
#endif
	}

	public static void Warning(params object[] aArgs) {
#if DEBUG || UNITY_EDITOR
		Pretty(aArgs).Warning();
#endif
	}

	public static void Error(params object[] aArgs) {
#if DEBUG || UNITY_EDITOR
		Pretty(aArgs).Error();
#endif
	}

	public static LogPrettifier Pretty(params object[] arguments) {
		return new LogPrettifier(arguments);
	}

	public class LogPrettifier
	{
		private readonly StringBuilder _builder = new StringBuilder();

		public LogPrettifier(params object[] arguments) {
#if DEBUG || UNITY_EDITOR
			ToText(arguments);
#endif
		}

		public void Log() {
#if DEBUG || UNITY_EDITOR
			Debug.logger.Log(LogType.Log, _builder.ToString());
#endif
		}

		public void Warning() {
#if DEBUG || UNITY_EDITOR
			Debug.logger.Log(LogType.Warning, _builder.ToString());
#endif
		}

		public void Error() {
#if DEBUG || UNITY_EDITOR
			Debug.logger.Log(LogType.Error, _builder.ToString());
#endif
		}

		public LogPrettifier Bold {
			get { return Brackets("<b>", "</b>"); }
		}

		public LogPrettifier Italic {
			get { return Brackets("<i>", "</i>"); }
		}

		public LogPrettifier Red {
			get { return Color("red"); }
		}

		public LogPrettifier Yellow {
			get { return Color("yellow"); }
		}

		public LogPrettifier Blue {
			get { return Color("blue"); }
		}

		public LogPrettifier Green {
			get { return Color("green"); }
		}

		public LogPrettifier Size(string size) {
			_builder.Insert(0, ">").Insert(0, size).Insert(0, "<size=").Append("</size>");
			return this;
		}

		public LogPrettifier Color(string color) {
			_builder.Insert(0, ">").Insert(0, color).Insert(0, "<color=").Append("</color>");
			return this;
		}

		public LogPrettifier Brackets(string before, string after) {
			_builder.Insert(0, before).Append(after);
			return this;
		}

		public override string ToString() {
			return _builder.ToString();
		}

		private void ToText(params object[] arguments) {
			for (int index = 0, length = arguments.Length; index < length; index++)
				if (arguments[index] == null)
					_builder.Append("Null ");
				else
					_builder.Append(arguments[index]).Append(" ");
		}

	}

}
