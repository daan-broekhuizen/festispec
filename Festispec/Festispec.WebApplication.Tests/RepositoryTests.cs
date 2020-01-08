using System;
using Festispec.WebApplication.Models.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Festispec.WebApplication.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void UserRepository_GetMyAssignments_Succeed()
        {
            //a
            var repo = new UserRepository();

            //a
            var assignments = repo.GetMyAssignments(1);

            //a
            Assert.AreEqual(1, assignments.Count);
        }
    }
}
