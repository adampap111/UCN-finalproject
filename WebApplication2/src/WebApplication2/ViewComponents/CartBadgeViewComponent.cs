using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Heinbo.Services;
using Heinbo.Models;

namespace Heinbo.ViewComponents
{
    public class CartBadgeViewComponent : ViewComponent
    {
        private ICartService _cartService;
        private ISalesRepository _repository;

        public CartBadgeViewComponent(ICartService cartService, ISalesRepository repository)
        {
            _cartService = cartService;
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = await _repository.GetCurrentUser();
            var cartItemCount = _cartService.GetCartItems(currentUser.Id).Count;
            return View("/Views/Shared/CartBadge.cshtml", cartItemCount);
        }
    }
}
