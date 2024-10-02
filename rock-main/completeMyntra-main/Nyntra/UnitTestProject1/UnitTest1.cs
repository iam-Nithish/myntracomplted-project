using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nyntra.Models;
using System;
using System.Security.Cryptography.X509Certificates;
using Nyntra;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            {
                using (var context = new MyntraEntities())
                {
                    var admin = new User
                    {
                        Fullname = "b",
                        Email = "b@gmail.com",
                        Role = "User",
                        Password = "2",
                    };

                    // Act
                    context.Users.Add(admin);
                    context.SaveChanges();  // This commits the changes to the database

                    // Assert - Check if the data was inserted
                    var insertedAdmin = context.Users.SingleOrDefault(a => a.Fullname == "b");  // Query the database to find the admin by AdminId
                    Assert.IsNotNull(insertedAdmin);  // Check that the admin was successfully inserted
                    Assert.AreEqual("b", insertedAdmin.Fullname);  // Validate that the correct data was inserted
                    Assert.AreEqual("User", insertedAdmin.Role);  // Validate the phone number
                    Assert.AreEqual("2", insertedAdmin.Password);  // Validate the email
                }
            }
        }
    }
    
}
