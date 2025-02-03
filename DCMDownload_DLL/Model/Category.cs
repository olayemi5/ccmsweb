using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCMDownload_DLL.Model
{
        public class Category
        {
            public Sheet1[] Sheet1 { get; set; }
        }

        public class Sheet1
        {
            public string Category { get; set; }
            public string Item { get; set; }
            public string CCMSCODE { get; set; }
        }

    
}
