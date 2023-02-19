using System;
using System.Globalization;
using UnityEngine;

namespace CoreGame
{

    public static class Utils
    {
        public static bool IsInternetAvailable()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }

        public static float GetScaledTime(int FPS = 30)
        {
            return Time.deltaTime * (float)FPS;
        }

        public static double GetTimeSinceEpoch()
        {
            return Math.Round((DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
        }

        public static void DrawBox(float x, float y, float width, float height, Color c)
        {
            Vector2 v = new Vector2(x, y);
            Vector2 v2 = new Vector2(x + width, y - height);
            Vector2 vector = new Vector2(width, 0f);
            Vector2 vector2 = new Vector2(0f, height);
            UnityEngine.Debug.DrawRay(v, vector, c);
            UnityEngine.Debug.DrawRay(v2, -vector, c);
            UnityEngine.Debug.DrawRay(v, -vector2, c);
            UnityEngine.Debug.DrawRay(v2, vector2, c);
        }

        public static int InvertCoordinate(int y)
        {
            return Game.Instance.map.invertCoordinate(y);
        }

        public static void DestroyImmediateChildren(GameObject go)
        {
            foreach (Transform transform in go.transform)
            {
                UnityEngine.Object.DestroyImmediate(transform.gameObject);
            }
        }

        public static string convertIntArray(int[] arrayToWrite, string separater = ",")
        {
            string text = string.Empty;
            for (int i = 0; i < arrayToWrite.Length; i++)
            {
                if (i != 0)
                {
                    text += separater;
                }
                text += arrayToWrite[i].ToString();
            }
            return text;
        }

        public static int[] convertStringToIntArray(string strIntComma)
        {
            string[] array = strIntComma.Split(new char[]
            {
            ','
            });
            int[] array2 = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = Convert.ToInt32(array[i]);
            }
            return array2;
        }

        public static string ColorToHex(Color32 color)
        {
            return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        }

        public static Color HexToColor(string hex)
        {
            byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }
    }
}