using NUnit.Framework;
using JsonSorter;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace UT_JsonSorter
{
    [TestFixture]
    public class JsonSorterTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WakeupTest()
        {
            Assert.IsTrue(true, message: "just to make sure everything is hooked up ok");
        }

        [Test]
        public void SortTest()
        {
            var expectedData = @"[{""name"":""Alice"",""timestamp"":""2021-07-03T12:45:00""},{""name"":""Bob"",""timestamp"":""2021-07-03T12:50:00""}]";
            var inputData = @"[{""name"": ""Bob"", ""timestamp"": ""2021-07-03T12:50:00"" }, {""name"": ""Alice"", ""timestamp"": ""2021-07-03T12:45:00"" }]";
            using var istream = new MemoryStream();
            var writer = new StreamWriter(istream);
            writer.Write(inputData);
            writer.Flush();
            istream.Position = 0;
            var inputStream = new StreamReader(istream);

            using var outputData = new MemoryStream();
            using var outputStream = new StreamWriter(outputData);
            var result = new Sorter().Sort(inputStream, outputStream, "timestamp");
            Assert.IsTrue(result);

            var output = System.Text.Encoding.UTF8.GetString(outputData.ToArray());
            Assert.IsTrue(output.SequenceEqual(expectedData));
        }
    }
}