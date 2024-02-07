using System.Collections.Generic;
using System.Web.Mvc;

namespace HackathonTeamBuilder.Models
{
    public class Utilities
    {
        public static List<SelectListItem> GetRoleSelectList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "Front-end Developer", Text = "Front-end Developer" },
                new SelectListItem { Value = "Back-end Developer", Text = "Back-end Developer" },
                new SelectListItem { Value = "UI/UX Designer", Text = "UI/UX Designer" },
                new SelectListItem { Value = "Project Manager", Text = "Project Manager" }
            };
        }
    }
}