namespace AmznMetaLibrary.Models
{
    public interface IReviewModel
    {
        string comment { get; set; }
        string date { get; set; }
        string linkToCostumer { get; set; }
        int modelId { get; set; }
        string stars { get; set; }
        string title { get; set; }
    }
}