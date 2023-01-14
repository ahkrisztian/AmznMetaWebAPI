using AmznMetaLibrary.Models;
using AmznMetaLibrary.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.Calculator
{
    public abstract class CalculatorBaseClass
    {
        protected readonly List<ReviewModel> _models;
        protected readonly string[] _good;
        protected readonly string[] _bad;

        public CalculatorBaseClass(List<ReviewModel> models, string[] good, string[] bad)
        {
            _models = models;
            _good = good;
            _bad = bad;
        }
        public abstract TreatmentModel Judgement();
    }
}
