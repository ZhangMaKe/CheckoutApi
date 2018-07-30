using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using checkout.Models;
using checkout.Repositories;
using checkout.Requests;
using checkout.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;

namespace checkout.Controllers
{
    [Produces("application/json")]
    [Route("api/orders")]
    public class OrderController : Controller
    {
        #region Fields

        private readonly IRepository<Order> _orderRepository;

        #endregion

        #region Constructors
        public OrderController(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        #endregion

        #region Api Methods

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderRequest request)
        {
            var response = new CreateOrderResponse(null, "");
            if (request is null)
            {
                response.Message = "CreateOrder: request is null";
                return BadRequest(response);
            }

            var order = new Order();

            if (_orderRepository.Add(order))
            {
                response.Entity = order;
                response.Message = "OK";
                return Ok(response);
            }

            response.Message = "CreateOrder: order could not be created";
            return StatusCode((int)HttpStatusCode.InternalServerError, response);

        }

        [HttpPost("{orderId}/items")]
        public IActionResult AddItemToOrder(Guid orderId, [FromBody]Item item)
        {
            var response = new AddItemToOrderResponse(null, "");

            var order = _orderRepository.Get(orderId);
            if (order == null)
            {
                response.Message = "AddItemToOrder: order not found";
                return BadRequest(response);
            }

            order.Items.Add(item);

            if (_orderRepository.Update(order))
            {
                response.Entity = order;
                response.Message = "OK";
                return Ok(response);
            }

            response.Message = "AddItemToOrder: item could not be added to order";

            return StatusCode((int)HttpStatusCode.InternalServerError, response);

        }

        [HttpDelete("{orderId}/items/{itemId}")]
        public IActionResult RemoveItemFromOrder(Guid orderId, Guid itemId)
        {
            var response = new RemoveItemFromOrderResponse(null, "");
            if (orderId == Guid.Empty)
            {
                response.Message = "RemoveItemFromOrder: orderId is invalid";
                return BadRequest(response);
            }

            if (itemId == Guid.Empty)
            {
                response.Message = "RemoveItemFromOrder: itemId is invalid";
                return BadRequest(response);
            }

            var order = _orderRepository.Get(orderId);

            if (order == null)
            {
                response.Message = "RemoveItemFromOrder: order not found";
                return BadRequest(response);
            }

            var item = order.Items.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                response.Message = "RemoveItemFromOrder: item not found";
                return BadRequest(response);
            }

            order.Items.Remove(item);

            if (_orderRepository.Update(order))
            {
                response.Message = "OK";
                response.Entity = order;
                return Ok(response);
            }

            response.Message = "RemoveItemFromOrder: item could not be removed from order";

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

        [HttpPatch("{orderId}/items/{itemId}/quantity")]
        public IActionResult UpdateItemQuantity([FromBody]int quantity, Guid orderId, Guid itemId)
        {
            var response = new UpdateItemQuantityResponse(null, "");

            if (orderId == Guid.Empty)
            {
                response.Message = "UpdateItemQuantity: orderId is invalid";
                return BadRequest(response);
            }

            if (itemId == Guid.Empty)
            {
                response.Message = "UpdateItemQuantity: itemId is invalid";
                return BadRequest(response);
            }

            var order = _orderRepository.Get(orderId);

            if (order == null)
            {
                response.Message = "UpdateItemQuantity: order not found";
                return BadRequest(response);
            }

            var item = order.Items.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                response.Message = "UpdateItemQuantity: item not found";
                return BadRequest(response);
            }

            item.Quantity = quantity;

            if (_orderRepository.Update(order))
            {
                response.Message = "OK";
                response.Entity = order;
                return Ok(response);
            }

            response.Message = "UpdateItemQuantity: quantity could not be updated";

            return StatusCode((int)HttpStatusCode.InternalServerError, response);

        }

        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(Guid orderId)
        {
            var response = new DeleteOrderResponse("");

            if (orderId == Guid.Empty)
            {
                response.Message = "DeleteOrder: orderId is invalid";
                return BadRequest(response);
            }

            var order = _orderRepository.Get(orderId);

            if (order == null)
            {
                response.Message = "DeleteOrder: order not found";
                return BadRequest(response);
            }

            if (_orderRepository.Remove(order))
            {
                response.Message = "OK";
                return Ok(response);
            }

            response.Message = "DeleteOrder: order could not be deleted";

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

        #endregion

    }
}