using RBDProject.Controllers;
using RBDProject.Models;
using System.Threading.Tasks;


namespace RBDProject.Components.Pages
{
    partial class Group
    {
        List<RbdGrupo> grupos { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            grupos = new List<RbdGrupo>();

            RBDGrupoesControllers A = new RBDGrupoesControllers();

            grupos = await A.GetAll();

            StateHasChanged();
        }
    }
}
