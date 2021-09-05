using _01_OnlineShopQuery.Contracts.Product;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductQuery _query;
        private readonly ICommentApplication _commentApplication;
        public ProductQueryModel Product { get; set; }

        public ProductModel(IProductQuery query,ICommentApplication commentApplication)
        {
            _query = query;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Product = _query.GetDetails(id);
        }

        public IActionResult OnPost(CreateComment command,string productSlug)
        {
            command.Type = CommentType.Product;

            _commentApplication.Create(command);

            return new RedirectToPageResult("/Product", new {Id = productSlug});
        }
    }
}
