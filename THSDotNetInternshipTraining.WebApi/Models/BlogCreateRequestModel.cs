namespace THSDotNetInternshipTraining.WebApi.Models
{
    public class BlogCreateRequestModel
    {
        public string BlogTitle { get; set; } = null!;

        public string BlogAuthor { get; set; } = null!;

        public string BlogContent { get; set; } = null!;
    }

    public class BlogCreateResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
    }

    public class BlogUpdateRequestModel
    {
        public string BlogTitle { get; set; } = null!;

        public string BlogAuthor { get; set; } = null!;

        public string BlogContent { get; set; } = null!;
    }

    public class BlogUpdateResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public BlogModel Data { get; set; } = null!;
    }

    public class BlogModel
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public string BlogAuthor { get; set; } = null!;

        public string BlogContent { get; set; } = null!;
    }

    public class BlogPatchRequestModel
    {
        public string? BlogTitle { get; set; } = null!;

        public string? BlogAuthor { get; set; } = null!;

        public string? BlogContent { get; set; } = null!;
    }
}