using System;
using System.Net;
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
            var order = new Order();

            //Act
            var response = _client.OrdersService.CreateOrder(order).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();
            var createdOrder = responseObject.Entity;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(typeof(Guid), createdOrder.Id.GetType());
            Assert.AreNotEqual(Guid.Empty, createdOrder.Id);
            Assert.AreEqual(order.Items.Count, createdOrder.Items.Count);
            Assert.AreEqual(order.Id, createdOrder.Id);
            
        }

        [Test]
        public void CreateOrderWithItem()
        {
            //Arrange
            var order = new Order();
            order.Items.Add(new Item("item 1", 2));

            //Act
            var response = _client.OrdersService.CreateOrder(order).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();
            var createdOrder = responseObject.Entity;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(order.Id, createdOrder.Id);
            Assert.AreEqual(order.Items.Count, createdOrder.Items.Count);
            Assert.AreEqual(order.Items[0].Id, createdOrder.Items[0].Id);
        }


        #endregion

        #region Add Item to Order Tests

        [Test]
        public void AddItemToOrder()
        {
            //Arrange
            var order = _client.OrdersService.CreateOrder(new Order()).Result.ToEntity<EntityApiResponse<Order>, Order>();
            var item = new Item("Item1", 1);

            //Act
            var response = _client.OrdersService.AddItemToOrder(order.Id, item).Result;
            var orderWithAddedItem = response.ToEntity<EntityApiResponse<Order>, Order>();

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
            var order = _client.OrdersService.CreateOrder(new Order()).Result.ToEntity<EntityApiResponse<Order>, Order>();
            var item = new Item("Item1", 1);
            _client.OrdersService.AddItemToOrder(order.Id, item).Result
                .ToEntity<EntityApiResponse<Order>, Order>();

            //Act
            var response = _client.OrdersService.RemoveItemFromOrder(order.Id, item.Id).Result;
            var orderWithItemRemoved = response.ToEntity<EntityApiResponse<Order>, Order>();

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(order.Id, orderWithItemRemoved.Id);
            Assert.AreEqual(0, orderWithItemRemoved.Items.Count);
        }

        [Test]
        public void RemoveInvalidItemFromOrder()
        {
            //Arrange
            var order = _client.OrdersService.CreateOrder(new Order()).Result.ToEntity<EntityApiResponse<Order>, Order>();
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
            var order = _client.OrdersService.CreateOrder(new Order()).Result.ToEntity<EntityApiResponse<Order>, Order>();

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
            var order = _client.OrdersService.CreateOrder(new Order()).Result.ToEntity<EntityApiResponse<Order>, Order>();
            var item = new Item("item 1", 1);
            var orderWithAddedItem = _client.OrdersService.AddItemToOrder(order.Id, item).Result
                .ToEntity<EntityApiResponse<Order>, Order>();

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
            var order = _client.OrdersService.CreateOrder(new Order()).Result.ToEntity<EntityApiResponse<Order>, Order>();
            var item = new Item("item 1", 1);

            //Act
            var response = _client.OrdersService.UpdateItemQuantity(2, order.Id, item.Id).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("UpdateItemQuantity: item not found", responseObject.Message);
        }

        #endregion

        #region Clear Items Tests

        [Test]
        public void ClearItems()
        {
            //Arrange
            var order = _client.OrdersService.CreateOrder(new Order()).Result.ToEntity<EntityApiResponse<Order>, Order>();
            var orderWithItem = _client.OrdersService.AddItemToOrder(order.Id, new Item("item1", 2)).Result
                .ToEntity<EntityApiResponse<Order>, Order>();

            //Act
            var response = _client.OrdersService.ClearItems(orderWithItem.Id).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("OK", responseObject.Message);
            Assert.AreEqual(0, responseObject.Entity.Items.Count);

        }

        [Test]
        public void ClearItemsInvalidOrder()
        {
            //Arrange
            var order = new Order();

            //Act
            var response = _client.OrdersService.ClearItems(order.Id).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("ClearItems: order not found", responseObject.Message);
        }

        [Test]
        public void ClearItemsInvalidOrderId()
        {
            //Arrange

            //Act
            var response = _client.OrdersService.ClearItems(new Guid()).Result;
            var responseObject = response.ToResponseObject<EntityApiResponse<Order>>();

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("ClearItems: orderId is invalid", responseObject.Message);

        }

        #endregion
    }
}
