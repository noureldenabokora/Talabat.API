using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specification.Order_spec;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IpaymentService _paymentService;

        /*  private readonly IGenricRepository<DeliveryMethod> _delvieryMethodRepo;
private readonly IGenricRepository<Order> _orderRepo;
private readonly IGenricRepository<Product> _productsRepo;
*/
        public OrderService(
            IBasketRepository basketRepository,
                     IUnitOfWork unitOfWork,
            IpaymentService paymentService

            /*   IGenricRepository<Product> productsRepo,
               IGenricRepository<DeliveryMethod> delvieryMethodRepo,
               IGenricRepository<Order> orderRepo
               */

            )
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            /*   _productsRepo = productsRepo;
_delvieryMethodRepo = delvieryMethodRepo;
_orderRepo = orderRepo;*/
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethod, Address shippingAddress)
        {
            //1 Get Basket From Baskets Repo
            var basket = await _basketRepository.GetBasketAsync(basketId);

            // 2 Get Selected Items at basket from prducts Repo
            var orderItems = new List<OrderItem>();

            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                    var productItemOrder = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);

                    var orderItem = new OrderItem(productItemOrder, product.Price, item.Quantity);

                    orderItems.Add(orderItem);

                }
            }

            // 3 Calculate Subtootal
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 4 get Deleviry method 

            var delvieryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethod);

            // 5 create Order

            var spec = new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntentId);

            var existingOrder = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

            if (existingOrder is not null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);

                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
            }
            var order = new Order(buyerEmail, shippingAddress, delvieryMethod, orderItems, subtotal, basket.PaymentIntentId);

            await _unitOfWork.Repository<Order>().Add(order);

            // 6 save to Database
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;

            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            return orders;
        }

        public async Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderSpecification(orderId, buyerEmail);

            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);

            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            var deliveryMethod = _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethod;
        }
    }
}
