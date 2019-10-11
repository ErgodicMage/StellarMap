using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using StellarMap.Core.Types;

namespace CoreTests
{
    [TestClass]
    public class GroupedPropertiesTests    
    {
        [TestMethod]
        public void DefaultConstructorTest()
        {
            GroupedProperties groupProperties = new GroupedProperties();
            Assert.IsNotNull(groupProperties.AllProperties);
        }

        [TestMethod]
        public void ConstructorWithInitialGroupTest()
        {
            GroupedProperties groupProperties = new GroupedProperties("Default");
            Assert.IsNotNull(groupProperties.AllProperties);
            Assert.IsTrue(groupProperties.AllProperties.ContainsKey("Default"));
            Assert.IsNotNull(groupProperties.AllProperties["Default"]);
        }

        [TestMethod]
        public void GroupIndexerTest()
        {
            GroupedProperties groupProperties = new GroupedProperties("Default");
            IDictionary<string, string> properties = groupProperties["Default"];
            Assert.IsNotNull(properties);
            Assert.AreEqual(0, properties.Count);
        }

        [TestMethod]
        public void GroupIndexerThrowExeptionTest()
        {
            GroupedProperties groupProperties = new GroupedProperties("Default");
            Assert.ThrowsException<KeyNotFoundException>(() => groupProperties["NotDefault"]);
        }

        [TestMethod]
        public void PropertyIndexerTest()
        {
            GroupedProperties groupProperties = new GroupedProperties("Default");
            groupProperties["Default"].Add("Name", "ErgodicMage");

            string result = groupProperties["Default"]["Name"];
            Assert.AreEqual("ErgodicMage", result);
        }

        [TestMethod]
        public void PropertyIndexerThrowExceptionTest()
        {
            GroupedProperties groupProperties = new GroupedProperties("Default");
            groupProperties["Default"].Add("Name", "ErgodicMage");
            Assert.ThrowsException<KeyNotFoundException>(() => groupProperties["Default"]["NotName"]);
        }

        [TestMethod]
        public void AddGroupTest()
        {
            GroupedProperties groupProperties = new GroupedProperties();
            groupProperties.AddGroup("Default");
            Assert.IsNotNull(groupProperties.AllProperties);
            Assert.IsTrue(groupProperties.AllProperties.ContainsKey("Default"));
            Assert.IsNotNull(groupProperties.AllProperties["Default"]);
        }

        [TestMethod]
        public void AddGroupPropertiesTest()
        {
            var properties = new Dictionary<string, string>();
            properties.Add("Name", "ErgodicMage");
            properties.Add("Email", "ErgodicMage@gmail.com");

            GroupedProperties groupProperties = new GroupedProperties();
            groupProperties.AddGroup("Default", properties);

            Assert.IsNotNull(groupProperties.AllProperties);
            Assert.IsTrue(groupProperties.AllProperties.ContainsKey("Default"));
            Assert.IsNotNull(groupProperties.AllProperties["Default"]);
            Assert.AreEqual(2, groupProperties.AllProperties["Default"].Count);
            Assert.AreEqual("ErgodicMage", groupProperties["Default"]["Name"]);
            Assert.AreEqual("ErgodicMage@gmail.com", groupProperties["Default"]["Email"]);
        }

        [TestMethod]
        public void AddPropertyGroupTest()
        {
            GroupedProperties groupProperties = new GroupedProperties("Default");
            groupProperties.AddProperty("Default", "Name", "ErgodicMage");

            string result = groupProperties["Default"]["Name"];
            Assert.AreEqual("ErgodicMage", result);
        }

        [TestMethod]
        public void AddPropertyNoGroupTest()
        {
            GroupedProperties groupProperties = new GroupedProperties();
            groupProperties.AddProperty("Default", "Name", "ErgodicMage");

            Assert.IsTrue(groupProperties.AllProperties.ContainsKey("Default"));
            Assert.IsNotNull(groupProperties.AllProperties["Default"]);

            string result = groupProperties["Default"]["Name"];
            Assert.AreEqual("ErgodicMage", result);
        }

        [TestMethod]
        public void AddPropertiesNoGroupTest()
        {
            var properties = new Dictionary<string, string>();
            properties.Add("Name", "ErgodicMage");
            properties.Add("Email", "ErgodicMage@gmail.com");

            GroupedProperties groupProperties = new GroupedProperties();
            groupProperties.AddProperties("Default", properties);

            Assert.IsNotNull(groupProperties.AllProperties);
            Assert.IsTrue(groupProperties.AllProperties.ContainsKey("Default"));
            Assert.IsNotNull(groupProperties.AllProperties["Default"]);
            Assert.AreEqual(2, groupProperties.AllProperties["Default"].Count);
            Assert.AreEqual("ErgodicMage", groupProperties["Default"]["Name"]);
            Assert.AreEqual("ErgodicMage@gmail.com", groupProperties["Default"]["Email"]);
        }

                [TestMethod]
        public void AddPropertiesGroupTest()
        {
            GroupedProperties groupProperties = new GroupedProperties("Default");
            groupProperties.AddProperty("Default", "Test", "ThisTest");

            var properties = new Dictionary<string, string>();
            properties.Add("Name", "ErgodicMage");
            properties.Add("Email", "ErgodicMage@gmail.com");
            groupProperties.AddProperties("Default", properties);

            Assert.IsNotNull(groupProperties.AllProperties);
            Assert.IsTrue(groupProperties.AllProperties.ContainsKey("Default"));
            Assert.IsNotNull(groupProperties.AllProperties["Default"]);
            Assert.AreEqual(3, groupProperties.AllProperties["Default"].Count);
            Assert.AreEqual("ErgodicMage", groupProperties["Default"]["Name"]);
            Assert.AreEqual("ErgodicMage@gmail.com", groupProperties["Default"]["Email"]);
            Assert.AreEqual("ThisTest", groupProperties["Default"]["Test"]);
        }
    }
}
