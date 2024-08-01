using FluentAssertions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMRG.Reader.Headers;
using XMRG.Reader.Readers.HeaderReaders;

using Xunit;

namespace XMRG.Reader.Tests;
public class GivenASecondHeader {

    public class OfTheMostModernBuild {

        public class WithTheHPOperatingSystem {

            [Fact]
            public void WhenTheFirstFieldIsParsed() {

                var bytes = new byte[] { 72, 80, 109, 121, 85, 115, 101, 114, 73, 100 };

                var (result, rest) = new UserDataReader().Parse(0, bytes).Value;

                var expected = new UserData(OperatingSystemType.HP, "myUserId");

                result
                    .Should()
                    .Be(expected);
            }
        }

        public class WithTheLXOperatingSystem {

            [Fact]
            public void WhenTheFirstFieldIsParsed() {

                var bytes = new byte[] { 76, 88, 109, 121, 85, 115, 101, 114, 73, 100 };

                var (result, rest) = new UserDataReader().Parse(0, bytes).Value;

                var expected = new UserData(OperatingSystemType.LX, "myUserId");

                result
                    .Should()
                    .Be(expected);
            }
        }
    }

    public class OfTheOlderBuild {

        [Fact]
        public void WhenTheFirstFieldIsParsed() {

            var bytes = new byte[] { 109, 121, 85, 115, 101, 114, 73, 100, 48, 48 };

            var (result, rest) = new UserDataReader().Parse(0, bytes).Value;

            var expected = new UserData(OperatingSystemType.Unknown, "myUserId00");

            result
                .Should()
                .Be(expected);
        }
    }

    public class OfAnyBuild {

        [Fact]
        public void WhenTheSecondFieldIsParsed() {

            // TODO - why is it a char[20] instead of a char[19]? Where is the extra?
            var bytes = new byte[20] { 50, 48, 50, 50, 45, 48, 49, 45, 48, 51, 32, 48, 56, 58, 48, 49, 58, 50, 50, 32 };

            var (result, rest) = new DateTimeReader().Parse(0, bytes).Value;

            var expected = DateTime.Parse("2022-01-03 08:01:22");

            result
                .Should()
                .Be(expected);
        }
    }
}
