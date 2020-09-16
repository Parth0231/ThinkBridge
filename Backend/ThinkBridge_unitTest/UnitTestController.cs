using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ThinkBridge_Api.Models;
using ThinkBridge_Api.Controllers;
using ThinkBridge_Api.Repository;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace ThinkBridge_unitTest
{
    public class UnitTestController
    {
        private InventoryRepository repository;
        public static DbContextOptions<ThinkBridgeDBContext> dbContextOptions { get; }
        public static string connectionString = "Server=DESKTOP-5RFESPC\\SDCSERVER;Initial Catalog=ThinkBridgeDB;MultipleActiveResultSets=true;Integrated Security=true";
        static UnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ThinkBridgeDBContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        public UnitTestController()
        {
            var context = new ThinkBridgeDBContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
          db.Seed(context);

            repository = new InventoryRepository(context);

        }

       #region Get By Id  

        [Fact]
        public async void Task_GetinventById_Return_OkResult()
        {
            //Arrange  
            var controller = new InventoriesController(repository);
            var inventid = 2;

            //Act  
            var data = await controller.GetInventory(inventid);

            //Assert  
            Assert.IsType<ActionResult<Inventory>>(data);
        }




        [Fact]
        public async void Task_GetinventById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new InventoriesController(repository);
            var inventid = 5;

            //Act  
            var data = await controller.GetInventory(inventid);

            //Assert  
            Assert.IsType<NotFoundResult>(data.Result);
        }

        [Fact]
        public async void Task_GetinventById_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new InventoriesController(repository);
            long? inventid = null;

            //Act  
            var data = await controller.GetInventory(inventid);

            //Assert  
            Assert.IsType<BadRequestResult>(data.Result);
        }

        [Fact]
        public async void Task_GetinventById_MatchResult()
        {
            //Arrange  
            var controller = new InventoriesController(repository);
            long inventid = 1;

            //Act  
            var data = await controller.GetInventory(inventid);

            //Assert  
            Assert.IsType<OkObjectResult>(data.Result);

            var okResult = data.Result.Should().BeOfType<OkObjectResult>().Subject;

            Assert.IsType<OkObjectResult>(okResult);
   
        }
        #endregion



        #region Get All  
        [Fact]
        public async void Task_Getinvents_Return_OkResult()
        {
            //Arrange  
            var controller = new InventoriesController(repository);

            //Act  
            var data = await controller.GetInventory();

            //Assert  
            Assert.IsType<OkObjectResult>(data.Result);
        }

        [Fact]
        public async void Task_Getinvents_MatchResult()
        {
            //Arrange  
            var controller = new InventoriesController(repository);

            //Act  
            var data = await controller.GetInventory();

            //Assert  
            Assert.IsType<OkObjectResult>(data.Result);          
        }


        #endregion




        #region Add New invent  
        [Fact]
        public async void Task_Add_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new InventoriesController(repository);
            var inventory = new Inventory() { Name = "Test Title 3", Description = "Test Description 3", Price = 22, Image = null };

            //Act  
            var data = await controller.PostInventory(inventory);

            //Assert  
            Assert.IsType<OkObjectResult>(data.Result);
        }

       
        #endregion





        #region Update Existing Blog  

        [Fact]
        public async void Task_Update_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new InventoriesController(repository);
            var inventId = 2;

            //Act  
            var existingPost = await controller.GetInventory(inventId);
            var okResult = existingPost.Result.Should().BeOfType<OkObjectResult>().Subject;          

            var post = new Inventory();
            post.Name = "Test name 2 Updated";
            post.Description = "description updated";
            post.Price =33;
            post.Image = null;

            var updatedData = await controller.PutInventory(post);

            //Assert  
            Assert.IsType<OkResult>(updatedData);
        }



        #endregion


        #region Delete Post  

        [Fact]
        public async void Task_Delete_Post_Return_OkResult()
        {
            //Arrange  
            var controller = new InventoriesController(repository);
            var inventid = 2;

            //Act  
            var data = await controller.DeleteInventory(inventid);

            //Assert  
            Assert.IsType<OkResult>(data.Result);
        }

       
        [Fact]
        public async void Task_Delete_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new InventoriesController(repository);
            long inventid = 0;

            //Act  
            var data = await controller.DeleteInventory(inventid);

            //Assert  
            Assert.IsType<BadRequestResult>(data.Result);
        }

        #endregion
    }
}
