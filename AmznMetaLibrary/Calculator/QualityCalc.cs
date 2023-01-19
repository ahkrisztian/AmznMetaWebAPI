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
            List<ReviewModel> deutschreviewmodels = GermanReviews(_models);


            List<ReviewModel> badtreatments = BadTreatments(_bad, deutschreviewmodels);

            foreach (var badmodel in badtreatments)
            {
                deutschreviewmodels.Remove(badmodel);
            }

            List<ReviewModel> goodtreatments = GoodTreatments(_good, deutschreviewmodels);           

            return new TreatmentModel() { GoodTreatments = goodtreatments, BadTreatments = badtreatments};
        }


        private List<ReviewModel> GoodTreatments(string[] goodtreatments, List<ReviewModel> models)
        {
            //Dictionary<int, string> output = new Dictionary<int, string>();

            List<ReviewModel> goodtreatmentsmodels = new List<ReviewModel>();

            for (int i = 0; i < goodtreatments.Length; i++)
            {
                for (int j = 0; j < models.Count; j++)
                {

                    if (models[j].title != null && models[j].title.ToLower().Contains(goodtreatments[i].ToLower()))
                    {
                        //output.Add(i, goodtreatments[i]);
                        goodtreatmentsmodels.Add(models[j]);
                        models.Remove(models[j]);
                        break;
                    }

                    if (models[j].comment != null && models[j].comment.ToLower().Contains(goodtreatments[i].ToLower()))
                    {
                        //output.Add(i, goodtreatments[i]);
                        goodtreatmentsmodels.Add(models[j]);
                        models.Remove(models[j]);
                        break;
                    }
                }
            }

            return goodtreatmentsmodels;
        }

        private List<ReviewModel> BadTreatments(string[] badtreatments, List<ReviewModel> models)
        {
            //Dictionary<int, string> output = new Dictionary<int, string>();

            List<ReviewModel> badtreatmentsmodels = new List<ReviewModel>();

            for (int i = 0; i < badtreatments.Length; i++)
            {
                for (int j = 0; j < models.Count; j++)
                {

                    if (models[j].title != null && models[j].title.ToLower().Contains(badtreatments[i].ToLower()))
                    {
                        //output.Add(j, badgoodtreatments[i]);
                        badtreatmentsmodels.Add(models[j]);
                        break;
                    }

                    if (models[j].comment != null && models[j].comment.ToLower().Contains(badtreatments[i].ToLower()))
                    {
                        //output.Add(i, badgoodtreatments[i]);
                        badtreatmentsmodels.Add(models[j]);
                        break;
                    }
                }
            }

            return badtreatmentsmodels;
        }

        private List<ReviewModel> GermanReviews(List<ReviewModel> models)
        {
            List<ReviewModel> output = new List<ReviewModel>();

            foreach (var model in models)
            {
                if (model.date.ToLower().Contains("deutschland"))
                {

                    output.Add(model);
                }
            }

            return output;
        }

    }
}
