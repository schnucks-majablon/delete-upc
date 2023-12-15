using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DeleteUPC.Helpers;
using DeleteUPC.Models;

namespace DeleteUPC.Controllers
{
    public class DeleteUPCController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Route("/DUPC/ValidateUPC")]
        [Route("DeleteUPC/DUPC/ValidateUPC")]
        // [AcceptVerbs("Get")]
        public string ValidateUPC(string UPC)
        {

            massWebClientResponse resp;
            DUPC p = new DUPC(new LoggerDatabaseProvider());
            resp = p.ValidateUPC(UPC);



            return JsonConvert.SerializeObject(resp);

        }

        [HttpGet]
        [Route("/DUPC/DisplayUPCinResults")]
        [Route("DeleteUPC/DUPC/DisplayUPCinResults")]
        // [AcceptVerbs("Get")]
        public string DisplayUPCinResults(string UPC)
        {

            massWebClientResponse resp;
            DUPC p = new DUPC(new LoggerDatabaseProvider());
            resp = p.DisplayUPCinResults(UPC);



            return JsonConvert.SerializeObject(resp);

        }


        [HttpGet]
        [Route("/DUPC/DisplayUPCIDinResults")]
        [Route("DeleteUPC/DUPC/DisplayUPCIDinResults")]
        // [AcceptVerbs("Get")]
        public string DisplayUPCIDinResults(string UPCID)
        {

            massWebClientResponse resp;
            DUPC p = new DUPC(new LoggerDatabaseProvider());
            resp = p.DisplayUPCIDinResults(UPCID);



            return JsonConvert.SerializeObject(resp);

        }


        [HttpGet]
        [Route("/DUPC/SearchUPC")]
        [Route("DeleteUPC/DUPC/SearchUPC")]
        // [AcceptVerbs("Get")]
        public string SearchUPC(string Search)
        {

            massWebClientResponse resp;
            DUPC p = new DUPC(new LoggerDatabaseProvider());
            resp = p.SearchUPC(Search);



            return JsonConvert.SerializeObject(resp);

        }


        [HttpGet]
        [Route("/DUPC/GetClusterNbr")]
        [Route("DeleteUPC/DUPC/GetClusterNbr")]
        // [AcceptVerbs("Get")]
        public string GetClusterNbr(string Cluster_Name)
        {

            massWebClientResponse resp;
            DUPC p = new DUPC(new LoggerDatabaseProvider());
            resp = p.GetClusterNbr(Cluster_Name);



            return JsonConvert.SerializeObject(resp);

        }

        [HttpGet]
        [Route("/DUPC/GetClusterUPCs")]
        [Route("DeleteUPC/DUPC/GetClusterUPCs")]
        // [AcceptVerbs("Get")]
        public string GetClusterUPCs(string CLUSTER_NBR)
        {

            massWebClientResponse resp;
            DUPC p = new DUPC(new LoggerDatabaseProvider());
            resp = p.GetClusterUPCs(CLUSTER_NBR);



            return JsonConvert.SerializeObject(resp);

        }

        [HttpGet]
        [Route("/DUPC/DisplayPendingRequests")]
        [Route("DeleteUPC/DUPC/DisplayPendingRequests")]
        // [AcceptVerbs("Get")]
        public string DisplayPendingRequests()
        {

            massWebClientResponse resp;
            DUPC p = new DUPC(new LoggerDatabaseProvider());
            resp = p.DisplayPendingRequests();



            return JsonConvert.SerializeObject(resp);

        }


    }
}