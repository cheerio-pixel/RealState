using System.ComponentModel.DataAnnotations;

namespace RealState.Application.ViewModel.PropertyType
{
    public class PropertyTypeSaveViewModel
    : BaseSaveViewModel
    {
        [Required(ErrorMessage = "The Name of this property type is necessary.")]
        [MaxLength(50, ErrorMessage = "The name cannot be longer than 50 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "The description of this property type is necessary.")]
        [MaxLength(100, ErrorMessage = "The description cannot be longer than 100 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}