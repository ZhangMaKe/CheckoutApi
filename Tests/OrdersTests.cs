using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using checkoutClient;
using checkoutClient.Models;
using checkoutClient.Responses;
using checkoutClient.Services;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class OrdersTests
    {
        private ICheckoutClient _client;

        [SetUp]
        public void Setup()
        {
            var httpRequestService = new HttpRequestService();
            var ordersService = new OrdersService(httpRequestService);
            _client = new CheckoutClient(ordersService);
        }

        #region Create Order Tests

        [Test]
        public void CreateOrder()
        {
            //Arrange

            //Act
            var response = _client.OrdersService.CreateOrder().Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();
            var order = responseObject.Entity;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(typeof(Guid), order.Id.GetType());
            Assert.AreNotEqual(Guid.Empty, order.Id);
            Assert.AreEqual(0, order.Items.Count);
        }

        #endregion

        #region Add Item to Order Tests

        [Test]
        public void AddItemToOrderAsync()
        {
            //Arrange
            var order = _client.OrdersService.CreateOrder().Result.ToOrder<CreateOrderResponse>();
            var item = new Item("Item1", 1);

            //Act
            var response = _client.OrdersService.AddItemToOrder(order.Id, item).Result;
            var orderWithAddedItem = response.ToOrder<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(order.Id, orderWithAddedItem.Id);
            Assert.AreEqual(item.Id, orderWithAddedItem.Items[0].Id);
            Assert.AreEqual(item.Name, orderWithAddedItem.Items[0].Name);
            Assert.AreEqual(item.Quantity, orderWithAddedItem.Items[0].Quantity);
        }

        [Test]
        public void AddItemToInvalidOrder()
        {
            //Arrange
            var item = new Item("Item1", 1);

            //Act
            var response = _client.OrdersService.AddItemToOrder(new Guid(), item).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }

        #endregion

        #region Remove Item from Order Tests

        [Test]
        public void RemoveItemFromOrder()
        {
            //Arrange
            var order = _client.OrdersService.CreateOrder().Result.ToOrder<CreateOrderResponse>();
            var item = new Item("Item1", 1);
            _client.OrdersService.AddItemToOrder(order.Id, item).Result
                .ToOrder<EntityApiResponse<Order>>();

            //Act
            var response = _client.OrdersService.RemoveItemFromOrder(order.Id, item.Id).Result;
            var orderWithItemRemoved = response.ToOrder<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(order.Id, orderWithItemRemoved.Id);
            Assert.AreEqual(0, orderWithItemRemoved.Items.Count);
        }

        [Test]
        public void RemoveInvalidItemFromOrder()
        {
            //Arrange
            var order = _client.OrdersService.CreateOrder().Result.ToOrder<CreateOrderResponse>();
            var item = new Item("Item1", 1);

            //Act
            var response = _client.OrdersService.RemoveItemFromOrder(order.Id, item.Id).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(responseObject.Message, "RemoveItemFromOrder: item not found");
        }

        [Test]
        public void RemoveItemFromInvalidOrder()
        {
            //Arrange
            var order = new Order();
            var item = new Item("Item1", 2);

            //Act
            var response = _client.OrdersService.RemoveItemFromOrder(order.Id, item.Id).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(responseObject.Message, "RemoveItemFromOrder: order not found");

        }

        [Test]
        public void RemoveItemFromOrderInvalidOrderId()
        {
            //Arrange

            //Act
            var response = _client.OrdersService.RemoveItemFromOrder(new Guid(), new Guid()).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("RemoveItemFromOrder: orderId is invalid", responseObject.Message);
        }

        [Test]
        public void RemoveItemFromOrderInvalidItemId()
        {
            //Arrange
            var order = _client.OrdersService.CreateOrder().Result.ToOrder<EntityApiResponse<Order>>();

            //Act
            var response = _client.OrdersService.RemoveItemFromOrder(order.Id, new Guid()).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("RemoveItemFromOrder: itemId is invalid", responseObject.Message);
        }

        #endregion

        #region Update Item Quantity Tests

        [Test]
        public void UpdateItemQuantity()
        {
            //Arrange
            var order = _client.OrdersService.CreateOrder().Result.ToOrder<CreateOrderResponse>();
            var item = new Item("item 1", 1);
            var orderWithAddedItem = _client.OrdersService.AddItemToOrder(order.Id, item).Result
                .ToOrder<AddItemToOrderResponse>();

            //Act
            var newQuantity = 2;
            var response = _client.OrdersService
                .UpdateItemQuantity(newQuantity, orderWithAddedItem.Id, orderWithAddedItem.Items[0].Id).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();
            var orderWithUpdatedQuantity = responseObject.Entity;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("OK", responseObject.Message);
            Assert.AreEqual(item.Id, orderWithUpdatedQuantity.Items[0].Id);
            Assert.AreEqual(2, orderWithUpdatedQuantity.Items[0].Quantity);

        }

        [Test]
        public void UpdateItemQuantityInvalidOrderId()
        {
            //Arrange

            //Act
            var response = _client.OrdersService.UpdateItemQuantity(2, new Guid(), new Guid()).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("UpdateItemQuantity: orderId is invalid", responseObject.Message);

        }

        [Test]
        public void UpdateItemQuantityInvalidItemId()
        {
            //Arrange
            var order = new Order();

            //Act
            var response = _client.OrdersService.UpdateItemQuantity(2, order.Id, new Guid()).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("UpdateItemQuantity: itemId is invalid", responseObject.Message);
        }

        [Test]
        public void UpdateItemQuantityInvalidOrder()
        {
            //Arrange
            var order = new Order();
            var item = new Item("item1", 1);

            //Act
            var response = _client.OrdersService.UpdateItemQuantity(2, order.Id, item.Id).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("UpdateItemQuantity: order not found", responseObject.Message);
        }

        [Test]
        public void UpdateItemQuantityInvalidItem()
        {
            //Arrange
            var order = _client.OrdersService.CreateOrder().Result.ToOrder<EntityApiResponse<Order>>();
            var item = new Item("item 1", 1);

            //Act
            var response = _client.OrdersService.UpdateItemQuantity(2, order.Id, item.Id).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("UpdateItemQuantity: item not found", responseObject.Message);
        }

        #endregion

        #region Delete Order Tests

        [Test]
        public void DeleteOrder()
        {
            //Arrange
            var order = _client.OrdersService.CreateOrder().Result.ToOrder<CreateOrderResponse>();

            //Act
            var response = _client.OrdersService.DeleteOrder(order.Id).Result;
            var responseObject = response.ToResponseObject<DeleteOrderResponse>();

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("OK", responseObject.Message);

        }

        [Test]
        public void DeleteInvalidOrder()
        {
            //Arrange
            var order = new Order();

            //Act
            var response = _client.OrdersService.DeleteOrder(order.Id).Result;
            var responseObject = response.ToResponseObject<DeleteOrderResponse>();

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("DeleteOrder: order not found", responseObject.Message);
        }

        [Test]
        public void DeleteInvalidOrderId()
        {
            //Arrange

            //Act
            var response = _client.OrdersService.DeleteOrder(new Guid()).Result;
            var responseObject = response.ToResponseObject<DeleteOrderResponse>();

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("DeleteOrder: orderId is invalid", responseObject.Message);

        }

        #endregion
    }
}
