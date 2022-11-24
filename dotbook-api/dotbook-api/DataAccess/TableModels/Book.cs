using dotbook_api.DataAccess.TableModels.Base;

namespace dotbook_api.DataAccess.TableModels
{
    public class Book : BaseNameEntity
    {
        public string Author { get; set; }

        public int PublishYear { get; set; }

        public int PublishId { get; set; }

        public Publish Publish { get; set; }

        public int ImageId { get; set; }

        public SavedFile Image { get; set; }

        public int PdfId { get; set; }

        public SavedFile Pdf { get; set; }

        public int UploadUserId { get; set; }

        public User UploadUser { get; set; }
    }
}
