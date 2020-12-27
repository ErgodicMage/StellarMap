using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using StellarMap.Core.Types;

namespace CoreTests
{
    [TestClass]
    public class GroupedPropertiesTests
    {
        private const string Default = "Default";
        private const string NotDefault = "NotDefault";
        private const string Name = "Name";
        private const string NotName = "NotName";
        private const string NameValue = "Ergodic Mage";
        private const string NotNameValue = "Not Ergodic Mage";
        private const string Email = "Email";
        private const string NotEmail = "NotEmail";
        private const string EmailValue = "ErgodicMage@gmail.com";
        private const string NotEmailValue = "NotErgodicMage@gmail.com";

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void DefaultConstructorTest()
        {
            GroupedProperties groupProperties = new GroupedProperties();
            Assert.IsNotNull(groupProperties.PropertyGroups);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void InitialGroupConstructorTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);
            Assert.IsNotNull(groupProperties.PropertyGroups);
            Assert.AreEqual(1, groupProperties.PropertyGroups.Count);
            Assert.IsTrue(groupProperties.PropertyGroups.ContainsKey(Default));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void GroupIndexerTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            Assert.IsNotNull(groupProperties[Default]);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void GroupIndexerExceptionTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            Assert.ThrowsException<KeyNotFoundException>(() => groupProperties[NotDefault]);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void PropertyIndexerTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);
            groupProperties[Default].Add(Name, NameValue);

            Assert.AreEqual(NameValue, groupProperties[Default, Name]);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void PropertyIndexerExceptionTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            Assert.ThrowsException<KeyNotFoundException>(() => groupProperties[Default, NotName]);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void AddGroupTest()
        {
            GroupedProperties groupProperties = new GroupedProperties();
            groupProperties.Add(Default);

            Assert.AreEqual(1, groupProperties.PropertyGroups.Count);
            Assert.IsTrue(groupProperties.PropertyGroups.ContainsKey(Default));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void AddExistingGroupTest()
        {
            GroupedProperties groupProperties = new GroupedProperties();
            groupProperties.Add(Default);
            groupProperties.Add(Default);

            // no effect on PropertyGroups
            Assert.AreEqual(1, groupProperties.PropertyGroups.Count);
            Assert.IsTrue(groupProperties.PropertyGroups.ContainsKey(Default));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void AddGroupandPropertiesTest()
        {
            var properties = new Dictionary<string, string>();
            properties.Add(Name, NameValue);
            properties.Add(Email, EmailValue);

            GroupedProperties groupProperties = new GroupedProperties();
            groupProperties.AddGroup(Default, properties);

            Assert.AreEqual(1, groupProperties.PropertyGroups.Count);
            Assert.IsTrue(groupProperties.PropertyGroups.ContainsKey(Default));
            Assert.AreEqual(2, groupProperties.PropertyGroups[Default].Count);
            Assert.AreEqual(NameValue, groupProperties[Default, Name]);
            Assert.AreEqual(EmailValue, groupProperties[Default, Email]);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void AddPropertyTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);
            groupProperties.Add(Default, Name, NameValue);

            Assert.AreEqual(1, groupProperties[Default].Count);
            Assert.IsTrue(groupProperties[Default].ContainsKey(Name));
            Assert.AreEqual(NameValue, groupProperties[Default, Name]);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void AddPropertyAnotherGroupTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);
            groupProperties.Add(NotDefault, Name, NameValue);

            Assert.AreEqual(0, groupProperties[Default].Count);
            Assert.IsFalse(groupProperties.PropertyGroups.ContainsKey(NotDefault));
            Assert.IsFalse(groupProperties[Default].ContainsKey(Name));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void AddPropertyDuplicateTest()
        {
            GroupedProperties groupProperties = new GroupedProperties();
            groupProperties.Add(Default);
            groupProperties.Add(Default, Name, NameValue);
            groupProperties.Add(Default, Email, EmailValue);
            groupProperties.Add(Default, Name, NameValue);

            Assert.AreEqual(1, groupProperties.PropertyGroups.Count);
            Assert.IsTrue(groupProperties.PropertyGroups.ContainsKey(Default));
            Assert.AreEqual(2, groupProperties.PropertyGroups[Default].Count);
            Assert.AreEqual(NameValue, groupProperties[Default, Name]);
            Assert.AreEqual(EmailValue, groupProperties[Default, Email]);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void AddPropertiesTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            var properties = new Dictionary<string, string>();
            properties.Add(Name, NameValue);
            properties.Add(Email, EmailValue);

            groupProperties.AddProperties(Default, properties);

            Assert.AreEqual(1, groupProperties.PropertyGroups.Count);
            Assert.IsTrue(groupProperties.PropertyGroups.ContainsKey(Default));
            Assert.AreEqual(2, groupProperties.PropertyGroups[Default].Count);
            Assert.AreEqual(NameValue, groupProperties[Default, Name]);
            Assert.AreEqual(EmailValue, groupProperties[Default, Email]);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void AddPropertiesDuplicateTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            var properties = new Dictionary<string, string>();
            properties.Add(Name, NameValue);
            properties.Add(Email, EmailValue);

            groupProperties.AddProperties(Default, properties);
            groupProperties.AddProperties(Default, properties);

            Assert.AreEqual(1, groupProperties.PropertyGroups.Count);
            Assert.IsTrue(groupProperties.PropertyGroups.ContainsKey(Default));
            Assert.AreEqual(2, groupProperties.PropertyGroups[Default].Count);
            Assert.AreEqual(NameValue, groupProperties[Default, Name]);
            Assert.AreEqual(EmailValue, groupProperties[Default, Email]);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void AddPropertiesAnotherGroupTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            var properties = new Dictionary<string, string>();
            properties.Add(Name, NameValue);
            properties.Add(Email, EmailValue);

            groupProperties.AddProperties(NotDefault, properties);

            Assert.AreEqual(0, groupProperties[Default].Count);
            Assert.IsFalse(groupProperties.PropertyGroups.ContainsKey(NotDefault));
            Assert.IsFalse(groupProperties[Default].ContainsKey(Name));
            Assert.IsFalse(groupProperties[Default].ContainsKey(Email));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void RemoveGroupTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);
            groupProperties.Add(NotDefault);

            Assert.AreEqual(2, groupProperties.PropertyGroups.Count);
            Assert.IsTrue(groupProperties.PropertyGroups.ContainsKey(Default));
            Assert.IsTrue(groupProperties.PropertyGroups.ContainsKey(NotDefault));

            groupProperties.Remove(NotDefault);

            Assert.AreEqual(1, groupProperties.PropertyGroups.Count);
            Assert.IsTrue(groupProperties.PropertyGroups.ContainsKey(Default));
            Assert.IsFalse(groupProperties.PropertyGroups.ContainsKey(NotDefault));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void RemoveGroupNotExistTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            groupProperties.Remove(NotDefault);

            Assert.AreEqual(1, groupProperties.PropertyGroups.Count);
            Assert.IsTrue(groupProperties.PropertyGroups.ContainsKey(Default));
            Assert.IsFalse(groupProperties.PropertyGroups.ContainsKey(NotDefault));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void RemovePropertyTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            var properties = new Dictionary<string, string>();
            properties.Add(Name, NameValue);
            properties.Add(Email, EmailValue);

            groupProperties.AddProperties(Default, properties);

            groupProperties.Remove(Default, Email);

            Assert.AreEqual(1, groupProperties.PropertyGroups.Count);
            Assert.IsTrue(groupProperties[Default].ContainsKey(Name));
            Assert.AreEqual(NameValue, groupProperties[Default, Name]);
            Assert.IsFalse(groupProperties[Default].ContainsKey(Email));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void RemovePropertyNotExistTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            var properties = new Dictionary<string, string>();
            properties.Add(Name, NameValue);
            properties.Add(Email, EmailValue);

            groupProperties.AddProperties(Default, properties);

            groupProperties.Remove(Default, NotName);

            Assert.AreEqual(1, groupProperties.PropertyGroups.Count);
            Assert.IsTrue(groupProperties[Default].ContainsKey(Name));
            Assert.IsTrue(groupProperties[Default].ContainsKey(Email));
            Assert.AreEqual(NameValue, groupProperties[Default, Name]);
            Assert.AreEqual(EmailValue, groupProperties[Default, Email]);
            Assert.IsFalse(groupProperties[Default].ContainsKey(NotName));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void RemovePropertyGroupNotExistTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            var properties = new Dictionary<string, string>();
            properties.Add(Name, NameValue);
            properties.Add(Email, EmailValue);

            groupProperties.AddProperties(Default, properties);

            groupProperties.Remove(NotDefault, NotName);

            Assert.AreEqual(1, groupProperties.PropertyGroups.Count);
            Assert.IsTrue(groupProperties[Default].ContainsKey(Name));
            Assert.IsTrue(groupProperties[Default].ContainsKey(Email));
            Assert.AreEqual(NameValue, groupProperties[Default, Name]);
            Assert.AreEqual(EmailValue, groupProperties[Default, Email]);
            Assert.IsFalse(groupProperties[Default].ContainsKey(NotName));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void GetTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            var properties = new Dictionary<string, string>();
            properties.Add(Name, NameValue);
            properties.Add(Email, EmailValue);

            groupProperties.AddProperties(Default, properties);

            string retName = groupProperties.Get(Default, Name);
            string retEmail = groupProperties.Get(Default, Email);

            Assert.AreEqual(NameValue, retName);
            Assert.AreEqual(EmailValue, retEmail);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void GetGroupTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            var properties = new Dictionary<string, string>();
            properties.Add(Name, NameValue);
            properties.Add(Email, EmailValue);

            groupProperties.AddProperties(Default, properties);

            var returnproperties = groupProperties.Get(Default);

            Assert.AreEqual(2, returnproperties.Count);
            Assert.IsTrue(returnproperties.ContainsKey(Name));
            Assert.AreEqual(NameValue, returnproperties[Name]);
            Assert.IsTrue(returnproperties.ContainsKey(Email));
            Assert.AreEqual(EmailValue, returnproperties[Email]);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void GetPropertyNotExistTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            var properties = new Dictionary<string, string>();
            properties.Add(Name, NameValue);
            properties.Add(Email, EmailValue);

            groupProperties.AddProperties(Default, properties);

            string retName = groupProperties.Get(Default, NotName);

            Assert.IsTrue(string.IsNullOrEmpty(retName));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void GetGroupNotExistTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);

            var properties = new Dictionary<string, string>();
            properties.Add(Name, NameValue);
            properties.Add(Email, EmailValue);

            groupProperties.AddProperties(Default, properties);

            var returnproperties = groupProperties.Get(NotDefault);

            Assert.IsNull(returnproperties);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void SetTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);
            groupProperties.Add(Default, Name, NameValue);

            groupProperties.Set(Default, Name, NotNameValue);

            Assert.AreEqual(NotNameValue, groupProperties.Get(Default, Name));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void SetPropertyNotExistTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);
            groupProperties.Add(Default, Name, NameValue);

            groupProperties.Set(Default, NotName, NotNameValue);

            Assert.AreEqual(NameValue, groupProperties.Get(Default, Name));
            Assert.IsTrue(string.IsNullOrEmpty(groupProperties.Get(Default, NotName)));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void SetGroupNotExistTest()
        {
            GroupedProperties groupProperties = new GroupedProperties(Default);
            groupProperties.Add(Default, Name, NameValue);

            groupProperties.Set(NotDefault, Name, NameValue);

            Assert.AreEqual(NameValue, groupProperties.Get(Default, Name));
            Assert.IsTrue(string.IsNullOrEmpty(groupProperties.Get(NotDefault, Name)));
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void BasicToStringTest()
        {
            //GroupedProperties groupProperties = new GroupedProperties(Default);

            //var properties = new Dictionary<string, string>();
            //properties.Add(Name, NameValue);
            //properties.Add(Email, EmailValue);
            //groupProperties.AddProperties(Default, properties);

            //groupProperties.AddGroup("Another Group");
            //groupProperties.AddProperty("Another Group", "AnotherName", "Another Harry Miller");
            //groupProperties.AddProperty("Another Group", "AnotherEmail", "Another.Harry.Miller@rrd.com");

            //string retValue = groupProperties.ToString();

            //string expected = TestingUtilities.ReadResource("GroupedPropertiesToStringValue.txt");

            //Assert.AreEqual(expected, retValue);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void EqualsTest()
        {
            GroupedProperties groupProperties = new GroupedProperties();
            groupProperties.Add(Default);
            groupProperties.Add(Default, Name, NameValue);
            groupProperties.Add(Default, Email, EmailValue);

            GroupedProperties othergroupProperties = new GroupedProperties();
            othergroupProperties.Add(Default);
            othergroupProperties.Add(Default, Name, NameValue);
            othergroupProperties.Add(Default, Email, EmailValue);

            bool bRet = groupProperties.Equals(othergroupProperties);

            Assert.IsTrue(bRet);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void EqualsReferenceTest()
        {
            GroupedProperties groupProperties = new GroupedProperties();
            groupProperties.Add(Default);
            groupProperties.Add(Default, Name, NameValue);
            groupProperties.Add(Default, Email, EmailValue);

            bool bRet = groupProperties.Equals(groupProperties);

            Assert.IsTrue(bRet);

        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void EqualsObjectTest()
        {
            GroupedProperties groupProperties = new GroupedProperties();
            groupProperties.Add(Default);
            groupProperties.Add(Default, Name, NameValue);
            groupProperties.Add(Default, Email, EmailValue);

            bool bRet = groupProperties.Equals((object)groupProperties);

            Assert.IsTrue(bRet);

        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void EqualsFalse1Test()
        {
            GroupedProperties groupProperties = new GroupedProperties();
            groupProperties.Add(Default);
            groupProperties.Add(Default, Name, NameValue);
            groupProperties.Add(Default, Email, EmailValue);

            GroupedProperties othergroupProperties = new GroupedProperties();
            othergroupProperties.Add(NotDefault);
            othergroupProperties.Add(NotDefault, NotName, NotNameValue);
            othergroupProperties.Add(NotDefault, NotEmail, NotEmailValue);

            bool bRet = groupProperties.Equals(othergroupProperties);

            Assert.IsFalse(bRet);
        }

        [TestMethod]
        [TestCategory(TestCategories.UnitTest)]
        public void EqualsFalse2Test()
        {
            GroupedProperties groupProperties = new GroupedProperties();
            groupProperties.Add(Default);
            groupProperties.Add(Default, Name, NameValue);
            groupProperties.Add(Default, Email, EmailValue);

            GroupedProperties othergroupProperties = new GroupedProperties();
            othergroupProperties.Add(Default);
            othergroupProperties.Add(Default, Name, NameValue);
            othergroupProperties.Add(Default, Email, NotEmailValue);

            bool bRet = groupProperties.Equals(othergroupProperties);

            Assert.IsFalse(bRet);
        }
    }
}
