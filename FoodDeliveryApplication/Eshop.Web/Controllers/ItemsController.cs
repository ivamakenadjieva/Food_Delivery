using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using EShop.Service.Interface;
using EShop.Domain.Domain;
using EShop.Repository.Interface;

namespace Eshop.Web.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IRestaurantService _restaurantService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IRepository<ItemInRestaurant> _repository;
       


        public ItemsController(IItemService itemService, IShoppingCartService shoppingCartService, IRestaurantService restaurantService, IRepository<ItemInRestaurant> repository)
        {
            _itemService = itemService;
            _shoppingCartService = shoppingCartService;
            _restaurantService = restaurantService;
            _repository = repository;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(_itemService.GetAllItems());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _itemService.GetDetailsForItems(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.RestaurantId = new SelectList(_restaurantService.GetAllRestaurants(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ItemName,ItemDescription,Price,Rating,RestaurantId")] ItemDto product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();

                var newItem = new Item
                {
                    Id = product.Id,
                    ItemName = product.ItemName,
                    ItemDescription = product.ItemDescription,
                    Price = product.Price,
                    Rating = product.Rating,
                    Restaurant = product.Restaurant,
                    RestaurantId=product.RestaurantId
                };
                _itemService.CreateNewItem(newItem);
                var restaurant = _restaurantService.GetDetailsForRestaurant(product.RestaurantId);
                var itemInRestaurant = new ItemInRestaurant
                {
                    ItemId = newItem.Id,
                    Item = newItem,
                    Restaurant = restaurant,
                    RestaurantId = (Guid)product.RestaurantId,
                };
                restaurant.Items.Add(itemInRestaurant);
                _repository.Insert(itemInRestaurant);

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


            public IActionResult AddToCart(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _itemService.GetDetailsForItems(id);

            ItemInShoppingCart ps = new ItemInShoppingCart();

            if (product != null)
            {
                ps.ItemId = product.Id;
            }

            return View(ps);
        }

        [HttpPost]
        public IActionResult AddToCartConfirmed(ItemInShoppingCart model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.AddToShoppingConfirmed(model, userId);

            

            return View("Index", _itemService.GetAllItems());
        }


        // GET: Products/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _itemService.GetDetailsForItems(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,ItemName,ItemDescription,Price,Rating")] Item product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _itemService.UpdateExistingItem(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _itemService.GetDetailsForItems(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _itemService.DeleteItem(id);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
