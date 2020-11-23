using System;
using System.Collections.Generic;
using System.Text;

namespace Chickencoop.Tests.LobbyTests.TestCases
{
    public class LobbyRepositoryTestCase
    {
        public static readonly List<object[]> LobbyArgumentExceptionTestCase = new List<object[]>
        { //string Title, Games Enum
            new object[]{ "title", -2},
            new object[]{ "", 0},
            new object[]{ "    ", 0},
        };

        public static IEnumerable<object[]> LobbyArgumentExceptionIndex
        {
            get
            {
                List<object[]> tmp = new List<object[]>();
                for (int i = 0; i < LobbyArgumentExceptionTestCase.Count; i++)
                    tmp.Add(new object[] { i });
                return tmp;
            }
        }
    }
}
