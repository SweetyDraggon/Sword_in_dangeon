using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace MiniJSON
{
    /*
	public static class Json
	{
		private sealed class Parser : IDisposable
		{
			private enum TOKEN
			{
				NONE,
				CURLY_OPEN,
				CURLY_CLOSE,
				SQUARED_OPEN,
				SQUARED_CLOSE,
				COLON,
				COMMA,
				STRING,
				NUMBER,
				TRUE,
				FALSE,
				NULL
			}

			private const string WORD_BREAK = "{}[],:\"";

			private StringReader json;

			private static Dictionary<string, int> __f__switch_map0;

			private char PeekChar
			{
				get
				{
					return Convert.ToChar(this.json.Peek());
				}
			}

			private char NextChar
			{
				get
				{
					return Convert.ToChar(this.json.Read());
				}
			}

			private string NextWord
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (!Json.Parser.IsWordBreak(this.PeekChar))
					{
						stringBuilder.Append(this.NextChar);
						if (this.json.Peek() == -1)
						{
							break;
						}
					}
					return stringBuilder.ToString();
				}
			}

			private Json.Parser.TOKEN NextToken
			{
				get
				{
					this.EatWhitespace();
					if (this.json.Peek() == -1)
					{
						return Json.Parser.TOKEN.NONE;
					}
					char peekChar = this.PeekChar;
					switch (peekChar)
					{
					case '"':
						return Json.Parser.TOKEN.STRING;
					case '#':
					case '$':
					case '%':
					case '&':
					case '\'':
					case '(':
					case ')':
					case '*':
					case '+':
					case '.':
					case '/':
						IL_8D:
						switch (peekChar)
						{
						case '[':
							return Json.Parser.TOKEN.SQUARED_OPEN;
						case '\\':
						{
							IL_A2:
							switch (peekChar)
							{
							case '{':
								return Json.Parser.TOKEN.CURLY_OPEN;
							case '}':
								this.json.Read();
								return Json.Parser.TOKEN.CURLY_CLOSE;
							}
							string nextWord = this.NextWord;
							switch (nextWord)
							{
							case "false":
								return Json.Parser.TOKEN.FALSE;
							case "true":
								return Json.Parser.TOKEN.TRUE;
							case "null":
								return Json.Parser.TOKEN.NULL;
							}
							return Json.Parser.TOKEN.NONE;
						}
						case ']':
							this.json.Read();
							return Json.Parser.TOKEN.SQUARED_CLOSE;
						}
						goto IL_A2;
					case ',':
						this.json.Read();
						return Json.Parser.TOKEN.COMMA;
					case '-':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						return Json.Parser.TOKEN.NUMBER;
					case ':':
						return Json.Parser.TOKEN.COLON;
					}
					goto IL_8D;
				}
			}

			private Parser(string jsonString)
			{
				this.json = new StringReader(jsonString);
			}

			public static bool IsWordBreak(char c)
			{
				return char.IsWhiteSpace(c) || "{}[],:\"".IndexOf(c) != -1;
			}

			public static object Parse(string jsonString)
			{
				object result;
				using (Json.Parser parser = new Json.Parser(jsonString))
				{
					result = parser.ParseValue();
				}
				return result;
			}

			public void Dispose()
			{
				this.json.Dispose();
				this.json = null;
			}

			private Dictionary<string, object> ParseObject()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				this.json.Read();
				while (true)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					switch (nextToken)
					{
					case Json.Parser.TOKEN.NONE:
						goto IL_37;
					case Json.Parser.TOKEN.CURLY_OPEN:
					{
						IL_2B:
						if (nextToken == Json.Parser.TOKEN.COMMA)
						{
							continue;
						}
						string text = this.ParseString();
						if (text == null)
						{
							goto Block_2;
						}
						if (this.NextToken != Json.Parser.TOKEN.COLON)
						{
							goto Block_3;
						}
						this.json.Read();
						dictionary[text] = this.ParseValue();
						continue;
					}
					case Json.Parser.TOKEN.CURLY_CLOSE:
						return dictionary;
					}
					goto IL_2B;
				}
				IL_37:
				return null;
				Block_2:
				return null;
				Block_3:
				return null;
			}

			private List<object> ParseArray()
			{
				List<object> list = new List<object>();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					Json.Parser.TOKEN tOKEN = nextToken;
					switch (tOKEN)
					{
					case Json.Parser.TOKEN.SQUARED_CLOSE:
						flag = false;
						continue;
					case Json.Parser.TOKEN.COLON:
						IL_38:
						if (tOKEN != Json.Parser.TOKEN.NONE)
						{
							object item = this.ParseByToken(nextToken);
							list.Add(item);
							continue;
						}
						return null;
					case Json.Parser.TOKEN.COMMA:
						continue;
					}
					goto IL_38;
				}
				return list;
			}

			private object ParseValue()
			{
				Json.Parser.TOKEN nextToken = this.NextToken;
				return this.ParseByToken(nextToken);
			}

			private object ParseByToken(Json.Parser.TOKEN token)
			{
				switch (token)
				{
				case Json.Parser.TOKEN.CURLY_OPEN:
					return this.ParseObject();
				case Json.Parser.TOKEN.SQUARED_OPEN:
					return this.ParseArray();
				case Json.Parser.TOKEN.STRING:
					return this.ParseString();
				case Json.Parser.TOKEN.NUMBER:
					return this.ParseNumber();
				case Json.Parser.TOKEN.TRUE:
					return true;
				case Json.Parser.TOKEN.FALSE:
					return false;
				case Json.Parser.TOKEN.NULL:
					return null;
				}
				return null;
			}

			private string ParseString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					if (this.json.Peek() == -1)
					{
						break;
					}
					char nextChar = this.NextChar;
					char c = nextChar;
					if (c != '"')
					{
						if (c != '\\')
						{
							stringBuilder.Append(nextChar);
						}
						else
						{
							if (this.json.Peek() != -1)
							{
								nextChar = this.NextChar;
								char c2 = nextChar;
								switch (c2)
								{
								case 'n':
									stringBuilder.Append('\n');
									continue;
								case 'o':
								case 'p':
								case 'q':
								case 's':
									IL_A5:
									if (c2 == '"' || c2 == '/' || c2 == '\\')
									{
										stringBuilder.Append(nextChar);
										continue;
									}
									if (c2 == 'b')
									{
										stringBuilder.Append('\b');
										continue;
									}
									if (c2 != 'f')
									{
										continue;
									}
									stringBuilder.Append('\f');
									continue;
								case 'r':
									stringBuilder.Append('\r');
									continue;
								case 't':
									stringBuilder.Append('\t');
									continue;
								case 'u':
								{
									char[] array = new char[4];
									for (int i = 0; i < 4; i++)
									{
										array[i] = this.NextChar;
									}
									stringBuilder.Append((char)Convert.ToInt32(new string(array), 16));
									continue;
								}
								}
								goto IL_A5;
							}
							flag = false;
						}
					}
					else
					{
						flag = false;
					}
				}
				return stringBuilder.ToString();
			}

			private object ParseNumber()
			{
				string nextWord = this.NextWord;
				if (nextWord.IndexOf('.') == -1)
				{
					long num;
					long.TryParse(nextWord, out num);
					return num;
				}
				double num2;
				double.TryParse(nextWord, out num2);
				return num2;
			}

			private void EatWhitespace()
			{
				while (char.IsWhiteSpace(this.PeekChar))
				{
					this.json.Read();
					if (this.json.Peek() == -1)
					{
						break;
					}
				}
			}
		}

		private sealed class Serializer
		{
			private StringBuilder builder;

			private Serializer()
			{
				this.builder = new StringBuilder();
			}

			public static string Serialize(object obj)
			{
				Json.Serializer serializer = new Json.Serializer();
				serializer.SerializeValue(obj);
				return serializer.builder.ToString();
			}

			private void SerializeValue(object value)
			{
				string str;
				IList anArray;
				IDictionary obj;
				if (value == null)
				{
					this.builder.Append("null");
				}
				else if ((str = (value as string)) != null)
				{
					this.SerializeString(str);
				}
				else if (value is bool)
				{
					this.builder.Append((!(bool)value) ? "false" : "true");
				}
				else if ((anArray = (value as IList)) != null)
				{
					this.SerializeArray(anArray);
				}
				else if ((obj = (value as IDictionary)) != null)
				{
					this.SerializeObject(obj);
				}
				else if (value is char)
				{
					this.SerializeString(new string((char)value, 1));
				}
				else
				{
					this.SerializeOther(value);
				}
			}

			private void SerializeObject(IDictionary obj)
			{
				bool flag = true;
				this.builder.Append('{');
				foreach (object current in obj.Keys)
				{
					if (!flag)
					{
						this.builder.Append(',');
					}
					this.SerializeString(current.ToString());
					this.builder.Append(':');
					this.SerializeValue(obj[current]);
					flag = false;
				}
				this.builder.Append('}');
			}

			private void SerializeArray(IList anArray)
			{
				this.builder.Append('[');
				bool flag = true;
				foreach (object current in anArray)
				{
					if (!flag)
					{
						this.builder.Append(',');
					}
					this.SerializeValue(current);
					flag = false;
				}
				this.builder.Append(']');
			}

			private void SerializeString(string str)
			{
				this.builder.Append('"');
				char[] array = str.ToCharArray();
				char[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					char c = array2[i];
					char c2 = c;
					switch (c2)
					{
					case '\b':
						this.builder.Append("\\b");
						goto IL_151;
					case '\t':
						this.builder.Append("\\t");
						goto IL_151;
					case '\n':
						this.builder.Append("\\n");
						goto IL_151;
					case '\v':
						IL_46:
						if (c2 == '"')
						{
							this.builder.Append("\\\"");
							goto IL_151;
						}
						if (c2 != '\\')
						{
							int num = Convert.ToInt32(c);
							if (num >= 32 && num <= 126)
							{
								this.builder.Append(c);
							}
							else
							{
								this.builder.Append("\\u");
								this.builder.Append(num.ToString("x4"));
							}
							goto IL_151;
						}
						this.builder.Append("\\\\");
						goto IL_151;
					case '\f':
						this.builder.Append("\\f");
						goto IL_151;
					case '\r':
						this.builder.Append("\\r");
						goto IL_151;
					}
					goto IL_46;
					IL_151:;
				}
				this.builder.Append('"');
			}

			private void SerializeOther(object value)
			{
				if (value is float)
				{
					this.builder.Append(((float)value).ToString("R"));
				}
				else if (value is int || value is uint || value is long || value is sbyte || value is byte || value is short || value is ushort || value is ulong)
				{
					this.builder.Append(value);
				}
				else if (value is double || value is decimal)
				{
					this.builder.Append(Convert.ToDouble(value).ToString("R"));
				}
				else
				{
					this.SerializeString(value.ToString());
				}
			}
		}

		public static object Deserialize(string json)
		{
			if (json == null)
			{
				return null;
			}
			return Json.Parser.Parse(json);
		}

		public static string Serialize(object obj)
		{
			return Json.Serializer.Serialize(obj);
		}
	}
	*/
    /// <summary>
    /// This class encodes and decodes JSON strings.
    /// Spec. details, see http://www.json.org/
    /// 
    /// JSON uses Arrays and Objects. These correspond here to the datatypes IList and IDictionary.
    /// All numbers are parsed to doubles.
    /// </summary>
    public static class Json
    {
        enum TOKEN
        {
            NONE,
            CURLY_OPEN,
            CURLY_CLOSE,
            SQUARED_OPEN,
            SQUARED_CLOSE,
            COLON,
            COMMA,
            STRING,
            NUMBER,
            TRUE,
            FALSE,
            NULL
        };

        const int BUILDER_CAPACITY = 2000;

        /// <summary>
        /// On decoding, this value holds the position at which the parse failed (-1 = no error).
        /// </summary>
        static int lastErrorIndex = -1;
        static string lastDecode;

        /// <summary>
        /// Parses the string json into a value
        /// </summary>
        /// <param name="json">A JSON string.</param>
        /// <returns>An List&lt;object&gt;, a Dictionary&lt;string, object&gt;, a double, a string, null, true, or false</returns>
        public static object Deserialize(string json)
        {
            // save the string for debug information
            lastDecode = json;

            if (json != null)
            {
                char[] charArray = json.ToCharArray();
                int index = 0;
                bool success = true;
                object value = ParseValue(charArray, ref index, ref success);
                if (success)
                {
                    lastErrorIndex = -1;
                }
                else
                {
                    lastErrorIndex = index;
                }
                return value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a IDictionary / IList object or a simple type (string, int, etc.) into a JSON string
        /// </summary>
        /// <param name="json">A Dictionary&lt;string, object&gt; / List&lt;object&gt;</param>
        /// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
        public static string Serialize(object json)
        {
            StringBuilder builder = new StringBuilder(BUILDER_CAPACITY);
            bool success = SerializeValue(json, builder);
            return (success ? builder.ToString() : null);
        }

        /// <summary>
        /// On decoding, this function returns the position at which the parse failed (-1 = no error).
        /// </summary>
        /// <returns></returns>
        public static bool LastDecodeSuccessful()
        {
            return (lastErrorIndex == -1);
        }

        /// <summary>
        /// On decoding, this function returns the position at which the parse failed (-1 = no error).
        /// </summary>
        /// <returns></returns>
        public static int GetLastErrorIndex()
        {
            return lastErrorIndex;
        }

        /// <summary>
        /// If a decoding error occurred, this function returns a piece of the JSON string
        /// at which the error took place. To ease debugging.
        /// </summary>
        /// <returns></returns>
        public static string GetLastErrorSnippet()
        {
            if (lastErrorIndex == -1)
            {
                return "";
            }
            else
            {
                int startIndex = lastErrorIndex - 5;
                int endIndex = lastErrorIndex + 15;
                if (startIndex < 0)
                {
                    startIndex = 0;
                }
                if (endIndex >= lastDecode.Length)
                {
                    endIndex = lastDecode.Length - 1;
                }

                return lastDecode.Substring(startIndex, endIndex - startIndex + 1);
            }
        }

        static Dictionary<string, object> ParseObject(char[] json, ref int index)
        {
            Dictionary<string, object> table = new Dictionary<string, object>();
            TOKEN token;

            // {
            NextToken(json, ref index);

            while (true)
            {
                token = LookAhead(json, index);
                if (token == TOKEN.NONE)
                {
                    return null;
                }
                else if (token == TOKEN.COMMA)
                {
                    NextToken(json, ref index);
                }
                else if (token == TOKEN.CURLY_CLOSE)
                {
                    NextToken(json, ref index);
                    return table;
                }
                else
                {

                    // name
                    string name = ParseString(json, ref index);
                    if (name == null)
                    {
                        return null;
                    }

                    // :
                    token = NextToken(json, ref index);
                    if (token != TOKEN.COLON)
                    {
                        return null;
                    }

                    // value
                    bool success = true;
                    object value = ParseValue(json, ref index, ref success);
                    if (!success)
                    {
                        return null;
                    }

                    table[name] = value;
                }
            }
        }

        static List<object> ParseArray(char[] json, ref int index)
        {
            List<object> array = new List<object>();
            TOKEN token;

            // [
            NextToken(json, ref index);

            while (true)
            {
                token = LookAhead(json, index);
                if (token == TOKEN.NONE)
                {
                    return null;
                }
                else if (token == TOKEN.COMMA)
                {
                    NextToken(json, ref index);
                }
                else if (token == TOKEN.SQUARED_CLOSE)
                {
                    NextToken(json, ref index);
                    break;
                }
                else
                {
                    bool success = true;
                    object value = ParseValue(json, ref index, ref success);
                    if (!success)
                    {
                        return null;
                    }

                    array.Add(value);
                }
            }

            return array;
        }

        static object ParseValue(char[] json, ref int index, ref bool success)
        {
            switch (LookAhead(json, index))
            {
                case TOKEN.STRING:
                    return ParseString(json, ref index);
                case TOKEN.NUMBER:
                    return ParseNumber(json, ref index);
                case TOKEN.CURLY_OPEN:
                    return ParseObject(json, ref index);
                case TOKEN.SQUARED_OPEN:
                    return ParseArray(json, ref index);
                case TOKEN.TRUE:
                    NextToken(json, ref index);
                    return true;
                case TOKEN.FALSE:
                    NextToken(json, ref index);
                    return false;
                case TOKEN.NULL:
                    NextToken(json, ref index);
                    return null;
                case TOKEN.NONE:
                    break;
            }

            success = false;
            return null;
        }

        static string ParseString(char[] json, ref int index)
        {
            StringBuilder s = new StringBuilder();
            char c;

            EatWhitespace(json, ref index);

            c = json[index++];

            bool complete = false;
            while (!complete)
            {

                if (index == json.Length)
                {
                    break;
                }

                c = json[index++];
                if (c == '"')
                {
                    complete = true;
                    break;
                }
                else if (c == '\\')
                {

                    if (index == json.Length)
                    {
                        break;
                    }
                    c = json[index++];

                    if (c == '"')
                    {
                        s.Append('"');
                    }
                    else if (c == '\\')
                    {
                        s.Append('\\');
                    }
                    else if (c == '/')
                    {
                        s.Append('/');
                    }
                    else if (c == 'b')
                    {
                        s.Append('\b');
                    }
                    else if (c == 'f')
                    {
                        s.Append('\f');
                    }
                    else if (c == 'n')
                    {
                        s.Append('\n');
                    }
                    else if (c == 'r')
                    {
                        s.Append('\r');
                    }
                    else if (c == 't')
                    {
                        s.Append('\t');
                    }
                    else if (c == 'u')
                    {
                        int remainingLength = json.Length - index;
                        if (remainingLength >= 4)
                        {
                            char[] unicodeCharArray = new char[4];
                            Array.Copy(json, index, unicodeCharArray, 0, 4);

                            // Drop in the HTML markup for the unicode character
                            s.AppendFormat(string.Format("&#x{0};", unicodeCharArray));

                            // skip 4 chars
                            index += 4;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    s.Append(c);
                }

            }

            if (!complete)
            {
                return null;
            }

            return s.ToString();
        }

        static object ParseNumber(char[] json, ref int index)
        {
            EatWhitespace(json, ref index);

            int lastIndex = GetLastIndexOfNumber(json, index);
            int charLength = (lastIndex - index) + 1;
            char[] numberCharArray = new char[charLength];

            Array.Copy(json, index, numberCharArray, 0, charLength);
            index = lastIndex + 1;

            string numberStr = new string(numberCharArray);

            if (numberStr.IndexOf('.') == -1)
            {
                return Int64.Parse(numberStr);
            }

            return Double.Parse(numberStr);
        }

        static int GetLastIndexOfNumber(char[] json, int index)
        {
            int lastIndex;
            for (lastIndex = index; lastIndex < json.Length; lastIndex++)
            {
                if ("0123456789+-.eE".IndexOf(json[lastIndex]) == -1)
                {
                    break;
                }
            }
            return lastIndex - 1;
        }

        static void EatWhitespace(char[] json, ref int index)
        {
            for (; index < json.Length; index++)
            {
                if (" \t\n\r".IndexOf(json[index]) == -1)
                {
                    break;
                }
            }
        }

        static TOKEN LookAhead(char[] json, int index)
        {
            int saveIndex = index;
            return NextToken(json, ref saveIndex);
        }

        static TOKEN NextToken(char[] json, ref int index)
        {
            EatWhitespace(json, ref index);

            if (index == json.Length)
            {
                return TOKEN.NONE;
            }

            char c = json[index];
            index++;
            switch (c)
            {
                case '{':
                    return TOKEN.CURLY_OPEN;
                case '}':
                    return TOKEN.CURLY_CLOSE;
                case '[':
                    return TOKEN.SQUARED_OPEN;
                case ']':
                    return TOKEN.SQUARED_CLOSE;
                case ',':
                    return TOKEN.COMMA;
                case '"':
                    return TOKEN.STRING;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '-':
                    return TOKEN.NUMBER;
                case ':':
                    return TOKEN.COLON;
            }
            index--;

            int remainingLength = json.Length - index;

            if (remainingLength >= 5)
            {
                // false
                if (json[index] == 'f' &&
                json[index + 1] == 'a' &&
                json[index + 2] == 'l' &&
                json[index + 3] == 's' &&
                json[index + 4] == 'e')
                {
                    index += 5;
                    return TOKEN.FALSE;
                }
            }

            if (remainingLength >= 4)
            {
                // true
                if (json[index] == 't' &&
                json[index + 1] == 'r' &&
                json[index + 2] == 'u' &&
                json[index + 3] == 'e')
                {
                    index += 4;
                    return TOKEN.TRUE;
                }

                // null
                if (json[index] == 'n' &&
                json[index + 1] == 'u' &&
                json[index + 2] == 'l' &&
                json[index + 3] == 'l')
                {
                    index += 4;
                    return TOKEN.NULL;
                }
            }

            return TOKEN.NONE;
        }

        static bool SerializeObject(IDictionary anObject, StringBuilder builder)
        {
            bool first = true;

            builder.Append('{');

            foreach (object e in anObject.Keys)
            {
                if (!first)
                {
                    builder.Append(',');
                }

                SerializeString(e.ToString(), builder);
                builder.Append(':');

                if (!SerializeValue(anObject[e], builder))
                {
                    return false;
                }

                first = false;
            }

            builder.Append('}');
            return true;
        }

        static bool SerializeArray(IList anArray, StringBuilder builder)
        {
            builder.Append('[');

            bool first = true;

            foreach (object obj in anArray)
            {
                if (!first)
                {
                    builder.Append(',');
                }

                if (!SerializeValue(obj, builder))
                {
                    return false;
                }

                first = false;
            }

            builder.Append(']');
            return true;
        }

        static bool SerializeValue(object value, StringBuilder builder)
        {
            if (value == null)
            {
                builder.Append("null");
            }
            else if (value.GetType().IsArray)
            {
                SerializeArray((IList)value, builder);
            }
            else if (value is string)
            {
                SerializeString((string)value, builder);
            }
            else if (value is Char)
            {
                SerializeString(Convert.ToString((char)value), builder);
            }
            else if (value is IDictionary)
            {
                SerializeObject((IDictionary)value, builder);
            }
            else if (value is IList)
            {
                SerializeArray((IList)value, builder);
            }
            else if (value is bool)
            {
                builder.Append((bool)value ? "true" : "false");
            }
            else if (value.GetType().IsPrimitive)
            {
                SerializeNumber(Convert.ToDouble(value), builder);
            }
            else
            {
                return false;
            }
            return true;
        }

        static void SerializeString(string aString, StringBuilder builder)
        {
            builder.Append('\"');

            char[] charArray = aString.ToCharArray();
            foreach (var c in charArray)
            {
                if (c == '"')
                {
                    builder.Append("\\\"");
                }
                else if (c == '\\')
                {
                    builder.Append("\\\\");
                }
                else if (c == '\b')
                {
                    builder.Append("\\b");
                }
                else if (c == '\f')
                {
                    builder.Append("\\f");
                }
                else if (c == '\n')
                {
                    builder.Append("\\n");
                }
                else if (c == '\r')
                {
                    builder.Append("\\r");
                }
                else if (c == '\t')
                {
                    builder.Append("\\t");
                }
                else
                {
                    int codepoint = Convert.ToInt32(c);
                    if ((codepoint >= 32) && (codepoint <= 126))
                    {
                        builder.Append(c);
                    }
                    else
                    {
                        builder.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
                    }
                }
            }

            builder.Append('\"');
        }

        static void SerializeNumber(double number, StringBuilder builder)
        {
            builder.Append(number.ToString());
        }
    }
}
