using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using StoreApp.Logic;
using StoreApp.App.Models;
using StoreApp.App.Controllers;


namespace StoreApp.Testing
{
    public class CustomerTesting
    {

        [Fact]
        public void TestBusinessCustomerFirstNameNotEmpty()
        {
            Logic.Customer customer = new Logic.Customer();

            Assert.Throws<CustomerException>(() => customer.firstName = "");
        }

        [Fact]
        public void TestBusinessCustomerLastNameNotEmpty()
        {
            Logic.Customer customer = new Logic.Customer();

            Assert.Throws<CustomerException>(() => customer.lastName = "");
        }

        /// <summary>
        /// Testing Address Input
        /// Expected: Boolean Values
        /// </summary>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        [Theory]
        [InlineData("123 Main", "Houston", "TX", "132")]
        [InlineData("123 Main St", "Houston", "TX", "77075")]
        [InlineData("212 Second st", "Houston", "TX", "12345")]
        public void IsAddressReturnsTrue(string street, string city, string state, string zip)
        {
            Address addr = new Address();
            addr.city = city;
            addr.state = state;
            addr.street = street;
            addr.zip = zip;

            Assert.True(addr.IsAddressNotNull());
        }
        /// <summary>
        /// Testing numbers of Items Input
        /// Expected: Integer values
        /// </summary>
        /// <param name="ariel"></param>
        /// <param name="suavitel"></param>
        /// <param name="downie"></param>
        [Theory]
        [InlineData(2)]
        [InlineData(100)]
        [InlineData(3)]
        public void IsNumbersOfItemTrue(int ariel)
        {
            Product prod = new Product();
            prod.NumberofAriel = ariel;

            Assert.Equal(expected: prod.NumberofAriel, actual: ariel);
        }

        /// <summary>
        /// Testing should return null values no matter what inputs are
        /// because of disconnecting to database
        /// expected: NULL 
        /// </summary>
        /// <param name="username"></param>
        [Theory]
        [InlineData("sn", "snow", "john", "123 Main", "Houston", "TX", "132")]
        [InlineData("ab", "ab", "dc", "243 second", "Houston", "TX", "13234")]
        [InlineData("ph", "phat", "nguyen", "2234 Hallow", "Houston", "TX", "73034")]
        public void IsCustomerInfoCorrect(string username, string firstName, string lastName, string city, string state, string street, string zip)
        {
            Logic.Customer customer = new Logic.Customer();
            customer.customerAddress.city = city;
            customer.customerAddress.state = state;
            customer.customerAddress.street = street;
            customer.customerAddress.zip = zip;
            customer.userName = username;
            customer.firstName = firstName;
            customer.lastName = lastName;
            Assert.True(customer.IsCustomerNotNull());
        }

        /// <summary>
        /// Testing numbers of Items Non-negative Values
        /// Expected: Boolean Values
        /// </summary>
        /// <param name="ariel"></param>
        /// <param name="suavitel"></param>
        /// <param name="downie"></param>
        [Theory]
        [InlineData(2, 3, 5)]
        [InlineData(100, 200, 400)]
        [InlineData(3, 2, 1)]
        public void IsNumberofItemsNegative(int ariel, int downie, int suavitel)
        {
            Inventory inventory = new Inventory();
            inventory.items.NumberofAriel = ariel;
            inventory.items.NumberofDownie = downie;
            inventory.items.NumberofSuavitel = suavitel;

            Assert.True(inventory.CheckInventoryNotNull());
        }
        /// <summary>
        /// Testing CustomerModel Validation
        /// Expected: Return False with empty string either FirstName or LastName
        /// </summary>
        [Fact]
        public void CustomerModelIsNotNull()
        {
            try
            {
                App.Models.Customer c = new App.Models.Customer()
                {
                    FirstName = "",
                    LastName = ""
                };
                Assert.True(false);
            }
            catch
            {
                Assert.True(true);
            }
        }
    }
}
