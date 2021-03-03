using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TestingAssignment.Controllers;
using TestingAssignment.Models;

namespace TestingAssignment.Test
{
    public class PassengerControllerTest
    {

        private readonly PassengersController _passengerController;

        public PassengerControllerTest()
        {
            _passengerController = new PassengersController();
        }

        [Fact]
        public void Test_GetPassengers()
        {
            var response = _passengerController.GetPassengers();
            Assert.Equal(6, response.Content.Count);
        }

        [Fact]
        public void Test_GetPassenger()
        {
            var response = _passengerController.GetPassenger(3);
            Assert.Equal(new Passenger().GetType(), response.Content.GetType());
            Assert.Equal(3, response.Content.Id);
        }

        [Fact]
        public void Test_PostPassenger()
        {
            var newPassenger = new Passenger(7, "Smith", "Waugh", "6758475968");
            var response = _passengerController.PostPassenger(newPassenger);
            Assert.Equal(new Passenger().GetType(), response.Content.GetType());
            Assert.Equal(7, response.Content.Id);
        }

        [Fact]
        public void Test_PutPassenger()
        {
            var newPassenger = new Passenger(3, "Kirk", "Steeple", "6758475968");
            var id = 3;
            var response = _passengerController.PutPassenger(id, newPassenger);
            Assert.Equal(newPassenger.GetType(), response.Content.GetType());
            Assert.Equal(3, response.Content.Id);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        public void Test_DeletePassenger(int id)
        {
            var response = _passengerController.DeletePassenger(id);
            var content = response.Content;
            var dummyPassenger = new Passenger();
            Assert.Equal(dummyPassenger.GetType(), content.GetType());
            Assert.Equal(id, response.Content.Id);
        }
    }
}
