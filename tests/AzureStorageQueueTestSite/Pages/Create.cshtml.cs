using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Messaging.Abstractions;
using System.Threading.Tasks;

namespace AzureStorageQueueTestSite.Pages
{
    public class CreateModel : PageModel
    {
        private MessageChannel<Message> _channel;

        public CreateModel(MessageChannel<Message> channel)
        {
            _channel = channel;
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _channel.Writer.WriteAsync(new Message() { Content = Message });
            return RedirectToPage("./Index");
        }
    }
}