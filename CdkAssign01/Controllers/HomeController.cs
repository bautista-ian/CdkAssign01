using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CdkAssign01.Models;
using CdkAssign01.Models.DTO;
using CdkAssign01.BAL;

namespace CdkAssign01.Controllers
{
    public class HomeController : Controller
    {
        ICustomersRepository _customersRepo;
        IOrdersRepository _ordersRepo;

        //Used for Dependency Injection
        public HomeController(ICustomersRepository customersRepo, IOrdersRepository ordersRepo)
        {
            if (customersRepo == null)
                throw new ArgumentNullException("customersRepo");

            if (ordersRepo == null)
                throw new ArgumentNullException("ordersRepo");

            _customersRepo = customersRepo;
            _ordersRepo = ordersRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lookup(string myDesiredCustomerId)
        {

            OrdersViewModel ordersListViewModel;

            int customerId = 0;
            bool success = int.TryParse(myDesiredCustomerId, out customerId);
            if (success)
            {
                CustomerDTO customerDTO = _customersRepo.GetCustomerById(customerId);
                OrdersDTO ordersDTO = _ordersRepo.GetOrdersForCustomer(customerId);
                ordersListViewModel = ConvertOrdersDTOToOrdersListViewModel(ordersDTO);
                AddCustomerDTOToOrdersListViewModel(ordersListViewModel, customerDTO);
                if (!ordersListViewModel.Customer.IsValidCustomer)
                {
                    ordersListViewModel.Message = "No customer with that ID";
                }
                else if (ordersListViewModel.Orders.Count() == 0)
                {
                    ordersListViewModel.Message = "No orders found";
                }
            }
            else
            {
                ordersListViewModel = new OrdersViewModel() { Message = "Not a valid customer ID" };
            }

            return View("Index", ordersListViewModel);
        }


        private OrdersViewModel ConvertOrdersDTOToOrdersListViewModel(OrdersDTO ordersDTO)
        {
            OrdersViewModel ordersListViewModel = new OrdersViewModel();

            ordersListViewModel.Orders = ordersDTO.Orders.Select(r => new OrderViewModel
            {
              OrderId = r.OrderId.ToString()
            , CustomerId = r.CustomerId.ToString()
            , Make = r.Make
            , Model = r.Model
            , Color = r.Color
            , Year = r.Year.ToString()
            , OwnershipType = r.OwnershipType
            }).ToList();
            
            return ordersListViewModel;
        }

        private void AddCustomerDTOToOrdersListViewModel(OrdersViewModel ordersListViewModel, CustomerDTO customerDTO)
        {
            ordersListViewModel.Customer.IsValidCustomer = (customerDTO.CustomerId > 0);
            ordersListViewModel.Customer.CustomerId = customerDTO.CustomerId;
            ordersListViewModel.Customer.FirstName = customerDTO.FirstName;
            ordersListViewModel.Customer.LastName = customerDTO.LastName;
            ordersListViewModel.Customer.Address = customerDTO.Address;
            ordersListViewModel.Customer.City = customerDTO.City;
            ordersListViewModel.Customer.StateCode = customerDTO.StateCode;
            ordersListViewModel.Customer.PostalCode = customerDTO.PostalCode;
        }

    }
}