using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class SentenceReader
    {
        private StreamReader _streamReader;

        public SentenceReader(String filePath)
        {
            try
            {
                _streamReader = new StreamReader(filePath, System.Text.Encoding.Default);

            }
            catch (FileNotFoundException e)
            {
                throw;
            }
            catch (IOException e)
            {
                throw;
            }
        }

        public IEnumerable<String> ReadAll()
        {
            string sentence;
            var sentences = new List<string>();
            while ((sentence = this.ReadSentence()) != null)
            {
                sentences.Add(sentence);
            }
            return sentences.AsEnumerable();
        }

        public string ReadSentence()
        {
            char currentChar;
            StringBuilder sb = new StringBuilder();

            while (true)
            {
                if (_streamReader.EndOfStream)
                {
                    if (sb.Length == 0) { return null; }
                    else
                    {
                        return sb.ToString();
                    }
                }
                currentChar = (char)_streamReader.Read(); 
                if (Char.IsWhiteSpace(currentChar))
                {
                    sb.Append(' ');
                    skipToNextLetter();
                }
                else if (currentChar == '—' && sb.Length == 0)
                {
                    //do nothing
                }
                else if (currentChar != '\n' && currentChar != '”' && currentChar != '\r'
                        && currentChar != '“' && currentChar != '\"' && currentChar != '\t')
                {
                    sb.Append(currentChar);
                }
                else if (currentChar == '\n' && sb.Length > 0)
                {
                    sb.Append(' ');
                }

                if (currentChar == '?' || currentChar == '!' || currentChar == '.')
                {
                    skipToNextLetter();
                    char nextLetter = (char)_streamReader.Peek();
                    if (Char.IsUpper(nextLetter))
                    {
                        return sb.ToString();
                    }
                    else
                    {
                        sb.Append(' ');
                    }
                }
            }
        }

        private void skipToNextLetter()
        {
            var nextLetter = (char)_streamReader.Peek();
            while (! Char.IsLetterOrDigit(nextLetter))
            {
                if (_streamReader.EndOfStream)
                {
                    return;
                }
                _streamReader.Read();
                nextLetter = (char)_streamReader.Peek();
            }
        }

        //private void skipToNextNonSpace()
        //{
        //    var nextLetter = (char)_streamReader.Peek();
        //    while (nextLetter == ' ' || nextLetter == '\t')
        //    {
        //        _streamReader.Read();
        //        nextLetter = (char)_streamReader.Peek();
        //    }
        //}

        //private bool isLowerCaseLetter(char ch)
        //{
        //    return Char.IsLetter(ch) && Char.IsLower(ch);
        //}

        public void Close()
        {
            _streamReader.Close();
            _streamReader.Dispose();
        }
    }
}
