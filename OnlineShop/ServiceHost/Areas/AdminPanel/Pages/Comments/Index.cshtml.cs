using System.Collections.Generic;
using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.AdminPanel.Pages.Comments
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<CommentViewModel> Comments;

        private readonly ICommentApplication _application;

        public IndexModel(ICommentApplication application)
        {
            _application = application;
        }

        public void OnGet(CommentSearchModel searchModel)
        {
            Comments = _application.Search(searchModel);
        }

        public IActionResult OnGetCancel(long id)
        {
            var result = _application.Cancel(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetConfirm(long id)
        {
            var result = _application.Confirm(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
