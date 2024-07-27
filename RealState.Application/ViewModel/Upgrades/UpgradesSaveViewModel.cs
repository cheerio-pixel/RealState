using System.ComponentModel.DataAnnotations;

namespace RealState.Application.ViewModel.Upgrades
{
    public class UpgradesSaveViewModel
    : BaseSaveViewModel
    {
        [Required(ErrorMessage = "The Name of this upgrade is necessary.")]
        [MaxLength(50, ErrorMessage = "The name cannot be longer than 50 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "The description of this upgrade is necessary.")]
        [MaxLength(100, ErrorMessage = "The description cannot be longer than 100 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}