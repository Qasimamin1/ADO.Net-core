using CRUDwithADONet.DAL;
using CRUDwithADONet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CRUDwithADONet.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Employee_DAL _dal;

        // Create a constructor 
        public EmployeeController(Employee_DAL dal)
        {
            _dal = dal;
        }

        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                employees = _dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return View(employees);
        }
        // insert action method 
        [HttpGet]

        public IActionResult Create() { 
        

                 return View(); 
        }

        [HttpPost]
            
        //it will receive employee model  
        public IActionResult Create(Employee model)
        {
            //call our insert  method 

            //check model state valid or not 

            try
            {
                if (ModelState.IsValid)
                {

                    TempData["errorMessage"] = "Model data is invalid";

                }
                bool result = _dal.Insert(model);

                if (!result)
                {

                    TempData["errorMessage"] = "Unable to save the data";
                    return View();

                }
                TempData["successMessage"] = "Employee details saved";


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]

        public IActionResult Edit(int id) {

            //call edit store procedure method 
            try
            {
                Employee employee = _dal.GetById(id);

                //employee data is available or not 
                if (employee.Id == 0)
                {


                    TempData["ErrorMessage"] = $"Employee details not found with Id :{id}";
                    return RedirectToAction("Index");

                }
                return View(employee);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]

        public IActionResult Edit(Employee Model)
        {

            //call edit store procedure method 
            try
            {
                if (!ModelState.IsValid)
                {

                    TempData["errorMessage"] = "Model data is invalid";
                    return View();

                }  
                bool result = _dal.Update(Model);
               

                    if (!result)
                    {

                        TempData["errorMessage"] = "Unable to update  the data";
                        return View();

                    }
                    TempData["successMessage"] = "Employee details updated";
                    return RedirectToAction("Index");


                
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }



        [HttpGet]

        public IActionResult Delete(int id)
        {

            //call edit store procedure method 
            try
            {
                Employee employee = _dal.GetById(id);

                //employee data is available or not 
                if (employee.Id == 0)
                {


                    TempData["ErrorMessage"] = $"Employee details not found with Id :{id}";
                    return RedirectToAction("Index");

                }
                return View(employee);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost , ActionName("Delete")]

        public IActionResult DeleteConfrimed(Employee model)
        {

            //call edit store procedure method 
            try
            {
                
                bool result = _dal.Delete(model.Id);


                if (!result)
                {

                    TempData["errorMessage"] = "Unable to delete  the data";
                    return View();

                }
                TempData["successMessage"] = "Employee deleted updated";
                return RedirectToAction("Index");



            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

    }
}
