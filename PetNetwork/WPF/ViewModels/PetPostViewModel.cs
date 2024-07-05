using PetNetwork.Domain.Models;

namespace PetNetwork.WPF.ViewModels;
public class PetPostViewModel : PostViewModel
{
    private Pet _pet;

    public Pet Pet
    {
        get => _pet;
        set
        {
            if (_pet == value) return;
            _pet = value;
            OnPropertyChanged();
        }
    }


    public PetPost ToPetPost() => new(Id, Title, Description, Author, ImageUrl, VideoUrl, LikeCount, Status,
        CreatedAt, Pet);
}

