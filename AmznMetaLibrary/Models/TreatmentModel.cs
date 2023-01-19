using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.Models
{
    public class TreatmentModel
    {
        public List<ReviewModel> GoodTreatments { get; set; }

        public List<ReviewModel> BadTreatments { get; set; }
    }
}
