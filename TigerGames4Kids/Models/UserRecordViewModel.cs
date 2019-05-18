using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TigerGames4Kids.Models;

namespace TigerGames4Kids.Controllers
{
    public class UserRecordViewModel : Controller
    {
        public UserType User { get; set; }
        public List<RecordType> Records { get; set; }
    }
}
