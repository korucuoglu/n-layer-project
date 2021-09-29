using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UdemyNLayerProject.API.Controllers;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;
using Xunit;

namespace UdemyNLayerProject.Test
{
    public class PersonsApiControllerTest
    {
        private readonly Mock<IService<Person>> _mockRepo;
        private readonly PersonsController _controller;
        private List<Person> persons;

        public PersonsApiControllerTest()
        {
            _mockRepo = new Mock<IService<Person>>();
            _controller = new PersonsController(_mockRepo.Object);

            persons = new List<Person>()
            {
                new Person{Id=1, Name="Halit", SurName="Korucuoğlu"},
                new Person{Id=2, Name="Betül", SurName="Korucuoğlu"},

            };
        }

        [Fact]
        public async void Index_ActionExecutes_ReturnPersonList()
        {
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(persons);
            var result = await _controller.GetAll();
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var personList = Assert.IsAssignableFrom<IEnumerable<Person>>(viewResult.Value);

            Assert.Equal(2, personList.Count());
        }

        [Fact]
        public async void Create_ActionExecutes_ReturnCreated()
        {
            Person newPerson = null;

            _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Person>())).Callback<Person>(x => newPerson = x);
            var result = await _controller.Add(persons.First());
            _mockRepo.Verify(repo => repo.AddAsync(It.IsAny<Person>()), Times.Once);
            Assert.Equal(persons.First().Id, newPerson.Id);

        }




    }
}
