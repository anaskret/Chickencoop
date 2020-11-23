using System;
using System.Collections.Generic;
using System.Text;

namespace Chickencoop.Tests.PersonalLeaderboardTests.TestCases
{
    public class PersonalLeadrboardRepositoryTestCase
    {
        public static readonly List<object[]> LeaderboardArgumentExceptionTestCase = new List<object[]>
        {
            new object[]{ new TimeSpan(0,5,0), DateTime.MinValue, 0, 0 },
            new object[]{ new TimeSpan(0,-1,0), DateTime.Today, 0, 0},
            new object[]{ new TimeSpan(0,1,0), DateTime.Today, 4, 0},
            new object[]{ new TimeSpan(0,1,0), DateTime.Today, 0, 3}
        };

        public static IEnumerable<object[]> LeaderboardArgumentExceptionIndex
        {
            get
            {
                List<object[]> tmp = new List<object[]>();
                for (int i = 0; i < LeaderboardArgumentExceptionTestCase.Count; i++)
                    tmp.Add(new object[] { i });
                return tmp;
            }
        }
    }
}
