using AmznMetaLibrary.Models;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace AmznMetaLibrary.Calculator
{
    public class QualityCalc : CalculatorBaseClass
    {
        public QualityCalc(List<ReviewModel> models, string[] good, string[] bad) : base(models, good, bad) { }
        public override TreatmentModel Judgement()
        {
            int goodtreatments = GoodTreatments(_good, _models);

            int badtreatments = BadTreatments(_bad, _models);

            return new TreatmentModel() { GoodTreatments = goodtreatments, BadTreatments = badtreatments };
        }


        private int GoodTreatments(string[] goodtreatments, List<ReviewModel> models)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();


            for (int i = 0; i < goodtreatments.Length; i++)
            {
                for (int j = 0; j < models.Count; j++)
                {

                    if (models[j].title != null && models[j].title.ToLower().Contains(goodtreatments[i].ToLower()))
                    {
                        output.Add(i, goodtreatments[i]);
                        break;
                    }

                    if (models[j].comment != null && models[j].comment.ToLower().Contains(goodtreatments[i].ToLower()))
                    {
                        output.Add(i, goodtreatments[i]);
                        break;
                    }
                }
            }

            return output.Count;
        }

        private int BadTreatments(string[] badgoodtreatments, List<ReviewModel> models)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();


            for (int i = 0; i < badgoodtreatments.Length; i++)
            {
                for (int j = 0; j < models.Count; j++)
                {

                    if (models[j].title != null && models[j].title.ToLower().Contains(badgoodtreatments[i].ToLower()))
                    {
                        output.Add(j, badgoodtreatments[i]);
                        break;
                    }

                    if (models[j].comment != null && models[j].comment.ToLower().Contains(badgoodtreatments[i].ToLower()))
                    {
                        output.Add(i, badgoodtreatments[i]);
                        break;
                    }
                }
            }

            return output.Count;
        }

    }
}
