using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendUnitTest
{
    [TestClass]
    public class UnitTest1 : TestBase
    {
        [TestInitialize]
        public void Init()
        {
            ResetCategoryInUseField();
            ResetContactsInUseField();
            ResetCustomersInUseField();
        }
        /// <summary>
        /// Test marking several records as not in use
        /// </summary>
        [TestMethod]
        public void CategoryInUse()
        {
            var tableName = "Categories";
            var primaryKey = "CategoryID";

            var identifierList = new List<int>() {2,6,7};

            SetInUse(tableName, primaryKey, identifierList,false);  

            Assert.IsTrue(GetInUse(tableName, primaryKey, identifierList, false), 
                $"Expected {identifierList.Count} records to be marked as not in use");
        }
        [TestMethod]
        public void ContactsInUse()
        {
            var tableName = "Contact";
            var primaryKey = "ContactIdentifier";

            var identifierList = new List<int>() { 27, 44, 60, 99 };

            SetInUse(tableName, primaryKey, identifierList, false);

            Assert.IsTrue(GetInUse(tableName, primaryKey, identifierList, false),
                $"Expected {identifierList.Count} records to be marked as not in use");

        }
        [TestMethod]
        public void CustomersInUse() 
        {

            var tableName = "Customers";
            var primaryKey = "CustomerIdentifier";

            var identifierList = new List<int>() { 2, 5, 11, 34, 42, 90, 87};

            SetInUse(tableName, primaryKey, identifierList, false);

            Assert.IsTrue(GetInUse(tableName, primaryKey, identifierList, false),
                $"Expected {identifierList.Count} records to be marked as not in use");

        }
        [TestMethod]
        public void ProductsInUse() 
        {

            var tableName = "Products";
            var primaryKey = "ProductID";

            var identifierList = new List<int>() { 10, 13, 34, 43};

            SetInUse(tableName, primaryKey, identifierList, false);

            Assert.IsTrue(GetInUse(tableName, primaryKey, identifierList, false),
                $"Expected {identifierList.Count} records to be marked as not in use");

        }
        /// <summary>
        /// This test has a primary key that does not exists
        /// which validates the prior test are valid.
        /// </summary>
        [TestMethod]
        public void ProductsInUseExpectFailure()
        {

            var tableName = "Products";
            var primaryKey = "ProductID";

            var identifierList = new List<int>() { 10, 13, 34, 43, 177 };

            SetInUse(tableName, primaryKey, identifierList, false);

            Assert.IsFalse(GetInUse(tableName, primaryKey, identifierList, false),
                $"Expected {identifierList.Count} records to be marked as not in use");

        }
    }
}
