using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace DeleteUPC.Helpers
{

    public struct ClusterNode
    {
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Text { get; set; }
        public bool Children { get; set; } // if node has sub-nodes set true or not set false
    }


    public class massWebClientResponse
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        public string PrimaryKeyValue { get; set; }         // returns inserted/generated key value to calling function
        public string AppData { get; set; }
        // Need AppData2 for GetChildren, which returns the child records and also a date, which is not part of the child records
        public DataSet AppDataSet { get; set; }

        public string oktaRole { get; set; }

    }
}
