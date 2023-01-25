using MyJourneyPlanner_OOkoye.DTOs;
using Microsoft.AspNetCore.Mvc;
using MyJourneyPlanner_OOkoye.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JourneyPlannerApp.DTOs;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using System.Collections;
using NuGet.Packaging;

namespace MyJourneyPlanner_OOkoye.Controllers
{
    public class PlanController : Controller
    {
        private JourneyplannerdbContext db = new JourneyplannerdbContext();
        public IActionResult Index()
        {
            return View();
        }

        [EnableCors("MyAllowSpecificOrigins")]
        [HttpGet]
        [Route("api/show")]
        public List<ResponseMessage> ShowPlan(string startstation, string destinationstation, string? viastation, string? excludingstation)
        {
            List<ResponseMessage> result = new List<ResponseMessage>();
            var Startstation = db.Stations.Where(s => s.StationName == startstation).FirstOrDefault();
            var Destinationstation = db.Stations.Where(s => s.StationName == destinationstation).FirstOrDefault();

            if (Startstation != null)
            {
                if (Destinationstation != null)
                {
                    var station = (from stations in db.Stations
                                   select new RouteDTO
                                   {

                                       StationId = stations.StationId,
                                       StationName = stations.StationName,
                                       LineId = db.LineStations.Where(p => p.StationId == stations.StationId).FirstOrDefault().LineId,
                                       LineName = db.Lines.Where(p => p.LineId == (db.LineStations.Where(p => p.StationId == stations.StationId).FirstOrDefault().LineId)).FirstOrDefault().LineName,
                                       LineColor = db.Lines.Where(p => p.LineId == (db.LineStations.Where(p => p.StationId == stations.StationId).FirstOrDefault().LineId)).FirstOrDefault().LineColor != null ? db.Lines.Where(p => p.LineId == (db.LineStations.Where(p => p.StationId == stations.StationId).FirstOrDefault().LineId)).FirstOrDefault().LineName : "",

                                   }
                       ).ToList();

                    Path p = new Path(db.Stations.Count() + 1);
                    var lStation = db.StationPairs.ToList();


                    for (int i = 0; i < lStation.Count(); i++)
                    {
                        p.addEdge(lStation[i].StartStationId, lStation[i].DestinatioinStationId);
                    }






                    int s = Startstation.StationId;


                    //  destination station
                    int d = Destinationstation.StationId;

                    List<RouteDTO> connectingroutes = new List<RouteDTO>();
                    if ( excludingstation == null)
                    {
                       if ( viastation == null )
                        {

                      
                        var FirstRoute = p.printAllPaths(s, d)[0];
                        for (int i=0; i < FirstRoute.Count; i++)
                        {
                            var item = new RouteDTO();
                            item.StationId = int.Parse(FirstRoute[i].ToString());
                            item.StationName = station.Where(item => item.StationId == int.Parse(FirstRoute[i].ToString())).FirstOrDefault().StationName;
                            item.LineId = station.Where(item => item.StationId == int.Parse(FirstRoute[i].ToString())).FirstOrDefault().LineId;
                            item.LineName = station.Where(item => item.StationId == int.Parse(FirstRoute[i].ToString())).FirstOrDefault().LineName;
                            item.LineColor = station.Where(item => item.StationId == int.Parse(FirstRoute[i].ToString())).FirstOrDefault().LineColor;

                            connectingroutes.Add(item);
                                if(i< FirstRoute.Count -1 && station.Where(item => item.StationId == int.Parse(FirstRoute[i].ToString())).FirstOrDefault().LineId != station.Where(item => item.StationId == int.Parse(FirstRoute[i +1].ToString())).FirstOrDefault().LineId)
                                {
                                    var item0 = new RouteDTO();
                                    var CurLine = station.Where(item => item.StationId == int.Parse(FirstRoute[i].ToString())).FirstOrDefault().LineName;
                                    var NewLine = station.Where(item => item.StationId == int.Parse(FirstRoute[i+ 1].ToString())).FirstOrDefault().LineName;
                                    var NewStation = station.Where(item => item.StationId == int.Parse(FirstRoute[i+1].ToString())).FirstOrDefault().StationName;
                                    var continuelineMsg = String.Format("change line from {0} to {1} and continue journey toward {2}",CurLine,NewLine,NewStation );
                                    item0.StationId = 0;
                                    item0.StationName = continuelineMsg;
                                    item0.LineId = 0;
                                    item0.LineName = "";
                                    connectingroutes.Add(item0);
                                }
                            }
                      

                            //   var connectingroutes = station.Where(a => FirstRoute.Any(b => a.StationId  == int.Parse(b.ToString())));

                            result.Add(new ResponseMessage() { Status = "OK", Message = "Retrieved Successfully", Data = JsonConvert.SerializeObject(connectingroutes) });
                        
                       }
                       else
                        {
                            var Viastation = db.Stations.Where(s => s.StationName == viastation).FirstOrDefault();
                            if (Viastation != null)
                            {
                                int v = Viastation.StationId;
                                
                                List<int> ViaRoute = new List<int>();
                                var AllRoute = p.printAllPaths(s, d);
                                 bool bfound = false;
                                for (int i = 0; i < AllRoute.Count ; i++)
                                {
                                    if (AllRoute[i].Contains(v))
                                        {
                                        ViaRoute = AllRoute[i];
                                        bfound = true;
                                    }
                                }
                               if( !bfound)
                                {
                                    result.Add(new ResponseMessage() { Status = "NotExists", Message = "Via Station does not exist in Route" });
                                    return result;
                                }

                                
                                for (int i = 0; i < ViaRoute.Count; i++)
                                {
                                    var item = new RouteDTO();
                                    item.StationId = int.Parse(ViaRoute[i].ToString());
                                    item.StationName = station.Where(item => item.StationId == int.Parse(ViaRoute[i].ToString())).FirstOrDefault().StationName;
                                    item.LineId = station.Where(item => item.StationId == int.Parse(ViaRoute[i].ToString())).FirstOrDefault().LineId;
                                    item.LineName = station.Where(item => item.StationId == int.Parse(ViaRoute[i].ToString())).FirstOrDefault().LineName;
                                    item.LineColor = station.Where(item => item.StationId == int.Parse(ViaRoute[i].ToString())).FirstOrDefault().LineColor;

                                    connectingroutes.Add(item);
                                    if (i < ViaRoute.Count - 1 && station.Where(item => item.StationId == int.Parse(ViaRoute[i].ToString())).FirstOrDefault().LineId != station.Where(item => item.StationId == int.Parse(ViaRoute[i + 1].ToString())).FirstOrDefault().LineId)
                                    {
                                        var item0 = new RouteDTO();
                                        var CurLine = station.Where(item => item.StationId == int.Parse(ViaRoute[i].ToString())).FirstOrDefault().LineName;
                                        var NewLine = station.Where(item => item.StationId == int.Parse(ViaRoute[i + 1].ToString())).FirstOrDefault().LineName;
                                        var NewStation = station.Where(item => item.StationId == int.Parse(ViaRoute[i + 1].ToString())).FirstOrDefault().StationName;
                                        var continuelineMsg = String.Format("change line from {0} to {1} and continue journey toward {2}", CurLine, NewLine, NewStation);
                                        item0.StationId = 0;
                                        item0.StationName = continuelineMsg;
                                        item0.LineId = 0;
                                        item0.LineName = "";
                                        connectingroutes.Add(item0);
                                    }
                                }

                               

                                result.Add(new ResponseMessage() { Status = "OK", Message = "Retrieved Successfully", Data = JsonConvert.SerializeObject(connectingroutes) });


                            }
                            else { result.Add(new ResponseMessage() { Status = "NotExists", Message = "Via Station does not exist" }); }
                        }

                        
                    }
                    else
                    {
                        var Excludingstation = db.Stations.Where(s => s.StationName == excludingstation).FirstOrDefault();
                        if (Excludingstation != null)
                        {
                            int e = Excludingstation.StationId;

                            List<int> ExclRoute = new List<int>();
                            var AllRoute0 = p.printAllPaths(s, d);
                            bool bfound0 = false;
                            for (int i = 0; i < AllRoute0.Count; i++)
                            {
                                if (AllRoute0[i].Contains(e))
                                {
                                    //AllRoute0.Remove(AllRoute0[i]);
                                    bfound0 = true;
                                }
                            }
                            //var itemNotExcl = AllRoute0.FirstOrDefault(r => int.Parse(r.ToString()) != e);
                            for (int i = 0; i < AllRoute0.Count; i++)
                            {
                                if (!AllRoute0[i].Contains(e))
                                {
                                    ExclRoute = AllRoute0[i];
                                    
                                }
                            }

                            if (!bfound0)
                            {
                                result.Add(new ResponseMessage() { Status = "NotExists", Message = "Excluding Station does not exist in Route" });
                                return result;
                            }
                            if (ExclRoute.Count == 0)
                            {
                                result.Add(new ResponseMessage() { Status = "NotExists", Message = "No Available Routes Outside the Excluding Station" });
                                return result;
                            }

                            for (int i = 0; i < ExclRoute.Count; i++)
                            {
                                var item = new RouteDTO();
                                item.StationId = int.Parse(ExclRoute[i].ToString());
                                item.StationName = station.Where(item => item.StationId == int.Parse(ExclRoute[i].ToString())).FirstOrDefault().StationName;
                                item.LineId = station.Where(item => item.StationId == int.Parse(ExclRoute[i].ToString())).FirstOrDefault().LineId;
                                item.LineName = station.Where(item => item.StationId == int.Parse(ExclRoute[i].ToString())).FirstOrDefault().LineName;
                                item.LineColor = station.Where(item => item.StationId == int.Parse(ExclRoute[i].ToString())).FirstOrDefault().LineColor;

                                connectingroutes.Add(item);
                                if (i < ExclRoute.Count - 1 && station.Where(item => item.StationId == int.Parse(ExclRoute[i].ToString())).FirstOrDefault().LineId != station.Where(item => item.StationId == int.Parse(ExclRoute[i + 1].ToString())).FirstOrDefault().LineId)
                                {
                                    var item0 = new RouteDTO();
                                    var CurLine = station.Where(item => item.StationId == int.Parse(ExclRoute[i].ToString())).FirstOrDefault().LineName;
                                    var NewLine = station.Where(item => item.StationId == int.Parse(ExclRoute[i + 1].ToString())).FirstOrDefault().LineName;
                                    var NewStation = station.Where(item => item.StationId == int.Parse(ExclRoute[i + 1].ToString())).FirstOrDefault().StationName;
                                    var continuelineMsg = String.Format("change line from {0} to {1} and continue journey toward {2}", CurLine, NewLine, NewStation);
                                    item0.StationId = 0;
                                    item0.StationName = continuelineMsg;
                                    item0.LineId = 0;
                                    item0.LineName = "";
                                    connectingroutes.Add(item0);
                                }
                            }
                            result.Add(new ResponseMessage() { Status = "OK", Message = "Retrieved Successfully", Data = JsonConvert.SerializeObject(connectingroutes) });


                        }
                        else { result.Add(new ResponseMessage() { Status = "NotExists", Message = "Excluding Station does not exist" }); }
                    }

                }
                else { result.Add(new ResponseMessage() { Status = "NotExists", Message = "Start Station does not exist" }); }
            }
            else { result.Add(new ResponseMessage() { Status = "NotExists", Message = "Destination Station does not exist" }); }

            //List<Train> trains = new List<Train>();
            //string source = "abc";
            //string destination = "xyz";


            //var results = route.Where(x => x.Routes.Any(y => y.StationId == source) && x.Routes.Any(y => y.StationId == destination))
            //    .Select(x => new {
            //        source = x.Routes.Where(y => y.StationId == source).FirstOrDefault(),
            //        destination = x.Routes.Where(y => y.StationId == destination).FirstOrDefault()
            //    })
            //    .Where(x => x.destination.StopNumber > x.source.StopNumber)
            //    .ToList();


            return result;
        }


// A directed graph using
// adjacency list representation
public class Path
    {
            //  List of stationIds in path
         List<List<int>> retList  = new List<List<int>>();
            
            // No. of vertices in path
            private int v;

        // adjacency list
        private List<int>[] adjList;

        // Constructor
        public Path(int vertices)
        {

            // initialise vertex count
            this.v = vertices;

            // initialise adjacency list
            initAdjList();
        }

        // utility method to initialise
        // adjacency list
        private void initAdjList()
        {
            adjList = new List<int>[v];

            for (int i = 0; i < v; i++)
            {
                adjList[i] = new List<int>();
            }
        }

        // add edge from u to v
        public void addEdge(int u, int v)
        {
            // Add v to u's list.
            adjList[u].Add(v);
        }

        // Prints all paths from
        // 's' to 'd'
        public List< List<int>> printAllPaths(int s, int d)
        {
            bool[] isVisited = new bool[v];
            List<int> pathList = new List<int>();

            // add source to path[]
            pathList.Add(s);

            // Call recursive utility
            printAllPathsUtil(s, d, isVisited, pathList);
                
                
                return retList;
        }

        // A recursive function to print
        // all paths from 'u' to 'd'.
        // isVisited[] keeps track of
        // vertices in current path.
        // localPathList<> stores actual
        // vertices in the current path
        private void printAllPathsUtil(int u, int d,
                                    bool[] isVisited,
                                    List<int> localPathList)
        {

            if (u.Equals(d))
            {
                    //Console.WriteLine(string.Join(" ", localPathList));
                    List<int> retList0 = new List<int>();
                    retList0.AddRange(localPathList);
                    retList.Add(retList0);
                    
                    return;
            }

            // Mark the current node
            isVisited[u] = true;

            // Recur for all the vertices
            // adjacent to current vertex
            foreach (int i in adjList[u])
            {
                if (!isVisited[i])
                {
                    // store current node
                    // in path[]
                    localPathList.Add(i);
                    printAllPathsUtil(i, d, isVisited,
                                    localPathList);

                    // remove current node
                    // in path[]
                    localPathList.Remove(i);
                }
            }

            // Mark the current node
            isVisited[u] = false;
        
        }

        //// Driver code
        //public static void Main(String[] args)
        //{
        //    // Create a sample graph
        //    Path g = new Path(4);
        //    g.addEdge(0, 1);
        //    g.addEdge(0, 2);
        //    g.addEdge(0, 3);
        //    g.addEdge(2, 0);
        //    g.addEdge(2, 1);
        //    g.addEdge(1, 3);

        //    // arbitrary source
        //    int s = 2;

        //    // arbitrary destination
        //    int d = 3;

        //    Console.WriteLine("Following are all different"
        //                    + " paths from " + s + " to " + d);
        //    g.printAllPaths(s, d);
        //}
    }

    // This code contributed by Rajput-Ji

    private object MessageFormatter(List<RouteDTO> route)
        {
            var obj = new object();
            return obj;
        }
    }
}



