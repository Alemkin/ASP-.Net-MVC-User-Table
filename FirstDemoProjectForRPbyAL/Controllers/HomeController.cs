using System;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FirstDemoProjectForRPbyAL.Services.Logging;
using FirstDemoProjectForRPbyAL.Models;
using FirstDemoProjectForRPbyAL.ViewModels;
using PagedList;

namespace FirstDemoProjectForRPbyAL.Controllers
{
    /**
     * This is the Controller for the User Table and its associated views
     * 
     * <author> Alexander Lemkin </author>
     */
    public class HomeController : Controller
    {
        /* Variables */

        private static readonly LogFactory Logger = new LogFactory();
        private readonly DeadlinesEntities _db = new DeadlinesEntities();
        private Customer _projectCustomer;

        /* Controller Methods */

        //GET: Home/Index?{sortOrder}&{currentFilter}&{searchString}&{page}&{pageSizeSelection}
        public ActionResult Index(string sortOrder, string currentFilter, string userSearchString, int? page,
            int? pageSizeSelection)
        {
            /* ViewBag variables for use in Index page,
             * utilizing Actionlinks for sorting */

            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirmNameSortParm = String.IsNullOrEmpty(sortOrder) ? "firm_name_desc" : "";
            ViewBag.CustomerIDSortParm = sortOrder == "customer_id_asc" ? "customer_id_desc" : "customer_id_asc";
            ViewBag.CustomerCityParm = sortOrder == "customer_city_asc" ? "customer_city_desc" : "customer_city_asc";
            ViewBag.CustomerZipSortParm = sortOrder == "customer_zip_asc" ? "customer_zip_desc" : "customer_zip_asc";
            ViewBag.PartnerIDSortParm = sortOrder == "partner_id_asc" ? "partner_id_desc" : "partner_id_asc";
            ViewBag.PartnerNameSortParm = sortOrder == "partner_name_asc" ? "partner_name_desc" : "partner_name_asc";
            ViewBag.PartnerCreatedSortParm = sortOrder == "partner_created_asc" ? "partner_created_desc" : "partner_created_asc";
            ViewBag.FirstNameSortParm = sortOrder == "first_name_asc" ? "first_name_desc" : "first_name_asc";
            ViewBag.LastNameSortParm = sortOrder == "last_name_asc" ? "last_name_desc" : "last_name_asc";
            ViewBag.IDSortParm = sortOrder == "id_asc" ? "id_desc" : "id_asc";

            ViewBag.CurrentPage = page ?? 1;

            //ViewBag.CurrentFilter = searchString;

            ViewBag.CurrentUserNameFilter = userSearchString;

            IQueryable<User> users;
            try
            {
                users = from u in _db.Users
                        select u;
            }
            catch (Exception e)
            {
                ViewBag.Message = "Problem querying Users database";

                Logger.LogException(e, "HomeController/Index");

                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            /* If a search was initiated, filter the users list by the
             searchString that was passed in, based on User First and/or Last Name*/
            if (users != null)
            {
                try
                {
                    users = users.Where(u => u.IsAdministrator.Equals(true));

                    if (!String.IsNullOrEmpty(userSearchString))
                    {
                        string[] finalSearch = userSearchString.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                        /*Search by first and last name if more than one string entered */
                        if (finalSearch.Count() > 1)
                        {
                            string firstName = finalSearch.First();
                            string lastName = finalSearch.Last();

                            users = users.Where(u => u.LastName.Contains(lastName)
                                                     && u.FirstName.Contains(firstName));
                        }
                        else /* or else just search by first or by last name */
                        {
                            users = users.Where(u => u.LastName.Contains(userSearchString)
                                                     || u.FirstName.Contains(userSearchString));
                        }
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Problem Searching For Specific User";

                    Logger.LogException(e, "HomeController/Index");

                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                IQueryable<FullUserInfo> fullUserInfo;
                try
                {
                    /* Add Users, and associated Customers and Partners into IQueryable<FullUserInfo>*/
                    fullUserInfo =
                        from u in users
                        join c in _db.Customers on u.CustomerId equals c.CustomerId
                        join p in _db.Partners on c.PartnerID equals p.PartnerID into ps
                        from p in ps.DefaultIfEmpty()
                        select new FullUserInfo { User = u, Customer = c, Partner = p };
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Problem querying for Users, and assoc iated Customers and Partners";

                    Logger.LogException(e, "HomeController/Index");

                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                try
                {
                    /*Switching based on the passed in sorting order,
                     and sorting the queryable list of FullUserInfo accordingly*/
                    switch (sortOrder)
                    {
                        case "last_name_desc":
                            fullUserInfo = fullUserInfo.OrderByDescending(u => u.User.LastName);
                            break;
                        case "last_name_asc":
                            fullUserInfo = fullUserInfo.OrderBy(u => u.User.LastName);
                            break;
                        case "first_name_desc":
                            fullUserInfo = fullUserInfo.OrderByDescending(u => u.User.FirstName);
                            break;
                        case "first_name_asc":
                            fullUserInfo = fullUserInfo.OrderBy(u => u.User.FirstName);
                            break;
                        case "id_asc":
                            fullUserInfo = fullUserInfo.OrderBy(u => u.User.UserId);
                            break;
                        case "id_desc":
                            fullUserInfo = fullUserInfo.OrderByDescending(u => u.User.UserId);
                            break;
                        case "customer_id_asc":
                            fullUserInfo = fullUserInfo.OrderBy(u => u.Customer.CustomerId);
                            break;
                        case "customer_id_desc":
                            fullUserInfo = fullUserInfo.OrderByDescending(u => u.Customer.CustomerId);
                            break;
                        case "customer_city_asc":
                            fullUserInfo = fullUserInfo.OrderBy(u => u.Customer.City);
                            break;
                        case "customer_city_desc":
                            fullUserInfo = fullUserInfo.OrderByDescending(u => u.Customer.City);
                            break;
                        case "customer_zip_asc":
                            fullUserInfo = fullUserInfo.OrderBy(u => u.Customer.ZipCode);
                            break;
                        case "customer_zip_desc":
                            fullUserInfo = fullUserInfo.OrderByDescending(u => u.Customer.ZipCode);
                            break;
                        case "partner_id_asc":
                            fullUserInfo = fullUserInfo.OrderBy(u => u.Customer.PartnerID);
                            break;
                        case "partner_id_desc":
                            fullUserInfo = fullUserInfo.OrderByDescending(u => u.Customer.PartnerID);
                            break;
                        case "partner_name_asc":
                            fullUserInfo = fullUserInfo.OrderBy(u => u.Partner.Name);
                            break;
                        case "partner_name_desc":
                            fullUserInfo = fullUserInfo.OrderByDescending(u => u.Partner.Name);
                            break;
                        case "partner_created_asc":
                            fullUserInfo = fullUserInfo.OrderBy(u => u.Partner.CreatedOn);
                            break;
                        case "partner_created_desc":
                            fullUserInfo = fullUserInfo.OrderByDescending(u => u.Partner.CreatedOn);
                            break;
                        case "firm_name_desc":
                            fullUserInfo = fullUserInfo.OrderByDescending(u => u.Customer.FirmName);
                            break;
                        default: // firm_name_asc is default
                            fullUserInfo = fullUserInfo.OrderBy(u => u.Customer.FirmName);
                            break;
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Problem sorting objects in table";

                    Logger.LogException(e, "HomeController/Index");

                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                int pageSize = pageSizeSelection ?? 25;
                ViewBag.PageSizeSelection = pageSize;
                int pageNumber = (page ?? 1);
                try
                {
                    return View(fullUserInfo.ToPagedList(pageNumber, pageSize));
                }
                catch (Exception e)
                {
                    Logger.LogException(e, "HomeController/Index");
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }

        // GET: Home/Edit?{id}&{sortOrder}&{page}
        public ActionResult Edit(int? id, string sortOrder, int? page)
        {

            if (id == null)
            {
                ViewBag.Message = "No ID found for editing, please add the ID of the user you wish to edit.";

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user;
            try
            {
                user = _db.Users.Find(id);
            }
            catch (Exception e)
            {
                ViewBag.Message = "Problem finding the User";

                Logger.LogException(e, "HomeController/Edit");

                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (user == null)
            {
                ViewBag.Message = "No User was found for this ID.";

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userEdit = new UserEdit
            {
                User = user,
                PageNumber = page ?? 1,
                SortOrder = sortOrder
            };

            return View(userEdit);
        }

        // POST: Home/Edit/{UserEdit}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEdit userEdit)
        {
            if (userEdit.User == null)
            {
                ViewBag.Message = "No User found for editing, please add the ID of the user you wish to edit.";

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User userToUpdate;
            try
            {
                userToUpdate = _db.Users.Find(userEdit.User.UserId);
            }
            catch (Exception e)
            {
                ViewBag.Message = "Problem Finding the user to update";

                Logger.LogException(e, "HomeController/Edit(Post)");

                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            /* Replacing userToUpdate's fields with passed in UserEdit object*/
            userToUpdate.FirstName = userEdit.User.FirstName;
            userToUpdate.LastName = userEdit.User.LastName;
            userToUpdate.EmailAddress = userEdit.User.EmailAddress;
            userToUpdate.Password = userEdit.User.Password;
            userToUpdate.ChangedOn = DateTimeOffset.Now;

            try
            {
                _db.Users.AddOrUpdate(userToUpdate);

                _db.SaveChanges();

                return RedirectToAction("Index", "Home", new { sortOrder = userEdit.SortOrder });
            }
            catch (Exception e)
            {
                ViewBag.Error = "Problem, updating the user in the database.";

                Logger.LogException(e, "HomeController/Edit(Post)");

                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        //GET: Home/Create?{sortOrder}&{page}
        public ActionResult Create(string sortOrder, int? page)
        {
            var userEdit = new UserEdit
            {
                User = new User(),
                PageNumber = page ?? 1,
                SortOrder = sortOrder
            };

            return View(userEdit);
        }

        // POST: Home/Create/{UserEdit}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserEdit userEdit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    /* Creating a base Customer to wrap all new Users in on first load */
                    var checkCustomer = from c in _db.Customers
                                        where c.FirmName == "UserTableProject Incorporated"
                                        && c.City == "Orlando"
                                        && c.ZipCode == "32765"
                                        select c;
                    if (!checkCustomer.Any())
                    {
                        _projectCustomer = new Customer
                        {
                            FirmName = "UserTableProject Incorporated",
                            City = "Orlando",
                            ZipCode = "32765",
                            AddedOn = DateTimeOffset.Now,
                            ChangedOn = DateTimeOffset.Now
                        };
                        _db.Customers.Add(_projectCustomer);
                        _db.SaveChanges();
                        var returnedCustomer = from c in _db.Customers
                                               where c.FirmName == "UserTableProject Incorporated"
                                                     && c.City == "Orlando"
                                                     && c.ZipCode == "32765"
                                               select c;
                        _projectCustomer.CustomerId = returnedCustomer.Single().CustomerId;
                    }
                    else
                    {
                        _projectCustomer = checkCustomer.First();
                    }

                    userEdit.User.CustomerId = _projectCustomer.CustomerId;
                    userEdit.User.IsAdministrator = true;
                    userEdit.User.AddedOn = DateTimeOffset.Now;
                    userEdit.User.ChangedOn = DateTimeOffset.Now;

                    _db.Users.AddOrUpdate(userEdit.User);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "Home", new { sortOrder = userEdit.SortOrder });
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "Problem Adding the new User.";

                Logger.LogException(e, "HomeController/Create(Post)");

                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return View(userEdit);
        }

        // GET: Home/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                ViewBag.Message = "No ID found for deleting, please add the ID of the user you wish to delete.";

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user;
            try
            {
                user = _db.Users.Find(id);
            }
            catch (Exception e)
            {
                ViewBag.Message = "Problem finding the user in the database.";

                Logger.LogException(e, "HomeController/Delete");

                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }
            return View(user);
        }

        // POST: Home/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                //Only remove if there is more than 1 user left
                if ((from u in _db.Users
                     select u).Take(2).ToList().Count > 1)
                {
                    User user = _db.Users.Find(id);
                    _db.Users.Remove(user);
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = "Problem removing user from database.";

                Logger.LogException(e, "HomeController/Delete(Post)");

                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

        }

    }
}
