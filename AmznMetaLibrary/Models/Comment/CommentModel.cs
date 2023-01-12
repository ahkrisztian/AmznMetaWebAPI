using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.Models.Comment
{
    public class CommentModel : IReviewModel
    {
        public string comment { get; set; }
        public string date { get; set; }
        public string linkToCostumer { get; set; }
        public int modelId { get; set; }
        public string stars { get; set; }
        public string title { get; set; }

        public string Formed;


        public CommentModel(string _title, string _comment, string formed)
        {
            title = _title;
            comment = _comment;

        }
    }

}
