using Microsoft.AspNetCore.Mvc.Rendering;
using Projects.Account;
using System.Collections.Generic;
using System.Linq;

namespace Projects.WebNext.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel() { }

        public LoginViewModel(IEnumerable<User> users)
        {
            Users = users.Select(u => new SelectListItem { Value = u.UserName, Text = u.UserName }).ToList();
        }

        public string UserName { get; set; }

        public List<SelectListItem> Users { get; }
    }
}
