using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace JsonSorter
{
    public class Sorter
    {
        public bool Sort(string inputFile, string outputFile, string sortField)
        {
            using var unsortedData = new StreamReader(inputFile);
            using var sortedData = new StreamWriter(outputFile);

            return Sort(unsortedData, sortedData, sortField);
        }

        /// <summary>
        /// Take a JSON array of objects and sort by the value of the sortField in each element.
        /// Objects without the sortField are discarded. The JSON document is assumed to be a simple
        /// array, so anything extra is discarded.
        /// </summary>
        /// <param name="unsortedData"></param>
        /// <param name="sortedData"></param>
        /// <param name="sortField"></param>
        /// <returns></returns>
        public bool Sort(StreamReader unsortedData, StreamWriter sortedData, string sortField)
        {
            var idata = unsortedData.ReadToEnd();
            using var doc = JsonDocument.Parse(idata);

            var ordered = doc.RootElement.EnumerateArray()
                                         .Where(o => o.TryGetProperty(sortField, out var val) && val.TryGetDateTime(out _))
                                         .OrderBy(o => o.GetProperty(sortField).GetDateTime())
                                         .ToList();
            var odata = JsonSerializer.Serialize(ordered);

            // this fails to do anything without a Flush()
            sortedData.Write(odata);
            sortedData.Flush();
            return true;
        }
    }
}
