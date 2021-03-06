using System.Text;
using BeatThat.Pools;
using UnityEngine;

namespace BeatThat.TransformPathExt
{
    /// <summary>
    /// ext methods for retrieving a Transform's path, which is often useful for debugging, e.g. myComp.Path() // returns '/root/mycompparent/mycomp'
    /// </summary>
    public static class Ext
	{
		/// <summary>
		/// Gets the path for a Transform from the root, e.g. '/root/parent/mytransform'
		/// </summary>
		static public string Path(this Component c)
		{
            if(c == null) {
                return null;
            }

			return GetPath((c is Transform)? c as Transform: c.transform);
		}

		/// <summary>
		/// Gets the path for a Transform from the root, e.g. '/root/parent/mytransform'
		/// </summary>
		static public string Path(this GameObject go)
		{
            if(go == null) {
                return null;
            }

			return GetPath(go.transform);
		}

		/// <summary>
		/// Gets the path for a Transform from the root, e.g. '/root/parent/mytransform'
		/// </summary>
		static public string Path(this Transform t)
		{
            if (t == null)
            {
                return null;
            }

			return GetPath(t);
		}

		/// <summary>
		/// Gets the path for a Transform from an ancestor Transform, e.g. '/ancestor/parent/mytransform'
		/// </summary>
		static public string PathFrom(this Transform t, Transform root)
		{
            if(t == null) {
                return null;
            }

			return GetPathFrom(t, root);
		}

		/// <summary>
		/// Gets the path for a Transform from the root, e.g. '/root/parent/mytransform'
		/// </summary>
		public static string GetPath(Transform t)
		{
            if(t == null) {
                return null;
            }

			string path = null;
			StringBuilder b = null;
			try {
				b = StringBuilderPool.Get();

				b = GetPathFrom(t, null, b);

				if(b[0] != '/') {
					b.Insert(0, "/");
				}

				path = b.ToString();
			}
			finally {
				if(b != null) {
					StringBuilderPool.Return(b);
				}
			}

			return path;
		}

		public static string GetPathFrom(Transform t, Transform root)
		{
            if(t == null) {
                return null;
            }

			if(t == root) {
				return "";
			}


			string path = null;
			StringBuilder b = null;
			try {
				b = GetPathFrom(t, root, b);
				path = b.ToString();
			}
			finally {
				if(b != null) {
					StringBuilderPool.Return(b);
				}
			}

			return path;
		}

		public static StringBuilder GetPathFrom(Transform t, Transform root, StringBuilder b)
		{
			if(b == null) {
				b = new StringBuilder();
			}

			if(t == root) {
				return b;
			}

			b.Append(t.name);
			while((t = t.parent) != null && t != root) {
				b.Insert(0, "/").Insert(0, t.name);
			}

			return b;
		}

	}
}


