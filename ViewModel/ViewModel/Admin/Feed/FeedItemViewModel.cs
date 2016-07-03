using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ViewModel.Admin.Feed
{
    public class FeedItemViewModel
    {
        public String Title { set; get; }
        public String AuthorName { set; get; }
        public String Content { set; get; }
        public String Url { set; get; }
        public DateTime LastUpdatedTime { set; get; }
        public DateTime PublishDate { set; get; }
    }
}
