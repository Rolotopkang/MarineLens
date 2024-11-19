using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Redesire
{
    /// <summary>
    /// Alsu using those in DemiLib
    /// http://demigiant.github.io/apis/demilib/html/namespace_d_g_1_1_de_extensions.html
    /// </summary>
    public static class TT
    {
        //------------------------------------------------------------------------------------------------
        // Finite Numbers       

        public enum FinNum
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Three = 3
        }

        public static FinNum RandomPosFinNum()
        {
            return (FinNum)Random.Range(1, 4);
        }

        public static FinNum RandomFinNum()
        {
            return (FinNum)Random.Range(0, 4);
        }

        //------------------------------------------------------------------------------------------------
        // Randomness

        public static T OneRndExcluding<T>(this IList<T> list, T excluded)
        {
            List<T> listToClone = list.Where(item => !item.Equals(excluded)).ToList();
            return listToClone[Random.Range(0, listToClone.Count)];
        }

        public static T OneRnd<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static int PosOrNegRND()
        {
            return (Random.Range(0f, 1f) > .5 ? 1 : -1);
        }

        //N.B. Changes the list passed.
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }



        //------------------------------------------------------------------------------------------------
        // Extensions

        public static bool Between(this int num, int lower, int upper, bool inclusive = true)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }

        public static bool IsEven(this int x)
        {
            return x % 2 == 0;
        }

        public static bool Ex(this string s)
        {
            return s != null && s.Trim().Length > 0;
        }


        //------------------------------------------------------------------------------------------------
        // Shortcuts

        public static bool Ex(params string[] args)
        {
            bool ex = true;
            foreach (string s in args)
            {
                ex = ex && (s != null && s.Trim().Length > 0);
                if (!ex)
                    break;
            }
            return ex;
        }

        public static bool Ex<T>(IEnumerable<T> o)
        {
            bool result = false;
            if (o != null)
            {
                result = o.Any();
            }
            return result;
        }

        public static IEnumerable<T> EnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }


        //------------------------------------------------------------------------------------------------
        //Colors

        public static string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }

        public static Color HexToColor(string hex)
        {
            byte r = Byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = Byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = Byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }


        //http://gamedev.stackexchange.com/questions/38536/given-a-rgb-color-x-how-to-find-the-most-contrasting-color-y
        public static Color ContrastColor(Color c)
        {
            Color ret = Color.white;

            float Y = 0.2126f * c.r + 0.7152f * c.g + 0.0722f * c.b;

            //float S = (Mathf.Max(c.r, c.g, c.b) - Mathf.Min(c.r, c.g, c.b)) / Mathf.Max(c.r, c.g, c.b);

            if (Y > .5f)
                ret = Color.black;
            return ret;
        }

        public static Color BrightenColor(Color c)
        {
            HSBColor hsbColor = new HSBColor(c);
            if (hsbColor.s < .5f)
            {
                hsbColor.s = .8f;
            }
            return hsbColor.ToColor();
        }


        //------------------------------------------------------------------------------------------------
        // Others

        public static Transform Search(this Transform target, string name)
        {
            if (target.name == name)
                return target;

            for (int i = 0; i < target.childCount; ++i)
            {
                var result = Search(target.GetChild(i), name);

                if (result != null)
                    return result;
            }

            return null;
        }

        public static string GetIP()
        {
            string ip = "Unknown IP";
/*#if UNITY_EDITOR
            ip = Network.player.ipAddress;
#endif
#if UNITY_IOS
                ip= Network.player.ipAddress;
#endif
#if UNITY_ANDROID
        ip= Network.player.ipAddress;
#endif
#if UNITY_STANDALONE
            ip = Network.player.ipAddress;
#endif*/
            return ip;
        }

        public static GameObject FindGameObjectChildWithTag(GameObject parent, string tag)
        {
            Transform[] ch = parent.GetComponentsInChildren<Transform>();
            foreach (Transform c in ch)
            {
                if (c.tag == tag)
                    return c.gameObject;
            }
            return null;
        }

        public static Rect ScreenRect
        {
            get { return new Rect(-Screen.width / 2f, -Screen.height / 2f, Screen.width, Screen.height); }
        }



        //CSV reader from https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/

        private static string SPLIT_RE = @";(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        //private static string SPLIT_RE = @";";
        private static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
        private static char[] TRIM_CHARS = { '\"' };

        public static Dictionary<string, string> ReadCSV(string file)
        {
            var list = new Dictionary<string, string>();

            TextAsset data = Resources.Load(file) as TextAsset;

            var lines = Regex.Split(data.text, LINE_SPLIT_RE);

            if (lines.Length <= 1) return list;

            //var header = Regex.Split(lines[0], SPLIT_RE);
            for (var i = 1; i < lines.Length; i++)
            {
                string[] values = Regex.Split(lines[i], SPLIT_RE);
                if (values.Length == 0 || values[0] == "") continue;

                string one = values[0].TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "").ToUpper();
                string two = values[1].TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                list[one] = two;
            }
            return list;
        }

        public static void SetSize(this RectTransform trans, Vector2 newSize)
        {
            Vector2 oldSize = trans.rect.size;
            Vector2 deltaSize = newSize - oldSize;
            trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
            trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
        }
        public static void SetWidth(this RectTransform trans, float newSize)
        {
            SetSize(trans, new Vector2(newSize, trans.rect.size.y));
        }
        public static void SetHeight(this RectTransform trans, float newSize)
        {
            SetSize(trans, new Vector2(trans.rect.size.x, newSize));
        }


    }
}
