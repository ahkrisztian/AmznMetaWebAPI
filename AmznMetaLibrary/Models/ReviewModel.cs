using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.Models
{
    public class ReviewModel : IReviewModel
    {
        public int modelId { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public string date { get; set; }

        public string stars { get; set; }
        public string linkToCostumer { get; set; }
    }
}
