
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstDemoProjectForRPbyAL.Tests.Controllers
{
    /**
     * Test class for the Home Controller.
     * Methods are not implemented as I need to create in memory hosting of the
     * SQL server to host the HTTP calls and handling, as well as the
     * DB queries, with an example database to control the models for testing
     * 
     * <author> Alexander Lemkin </author>
     */
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            
            //Create instance of Home Controller
            //Send parameters to Index method
            //assert the result is not null

            //(test)Pull the model passed back to the view and assert it has elelments in it

            //(test)iterate through sortOrders and assert the models passed back are sorted accordingly

            //(test)Send in a test searchString and assert the model only contains expected results

        }

        [TestMethod]
        public void Edit()
        {
            //Create instance of Home Controller
            //Send parameters to Edit (GET) method
            //assert the result is not null

            //assert the user returned was the expected

        }

        [TestMethod]
        public void EditPost()
        {
            //Create instance of Home Controller
            //Send example UserEdit object into Edit (POST) overload, with actual user in it
            //query the database and assert the user that been updated with the expected info

        }

        [TestMethod]
        public void Create()
        {
            //Create instance of HomeController
            //Send parameters to Create (GET) method
            //assert the ActionResult is not null and contains the expected empty UserEdit object

        }

        [TestMethod]
        public void CreatePost()
        {
            //Create instance of Home Controller
            //Send example UserEdit object with a fille dout User object in to to Create (POST) overload
            //Assert the passed in user exists in the database after the call

        }

        [TestMethod]
        public void Delete()
        {
            //Create instance of Home Controller
            //Create a new User first, checking to see if this User exists already first
            //Send parameters to Delete (GET) method with created/retrieved User
            //Asset the ActionResult is not null and contains the expected User

        }

        [TestMethod]
        public void DeletePost()
        {
            //Create instance of Home Controller
            //Send in id of previously created delete User into Delete (POST) overload
            //Query the database and ensure the User with this id has been deleted

            //Do it again with example database with only 1 user in it, and assert the Delete fails

        }

    }
}
