using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetNetwork.WPF.ViewModels.Validation;

public class RecipientValidation
{
    public string ValidateRecipient(string? recipient, string? groupName)
    {
        if (!string.IsNullOrEmpty(recipient) && !string.IsNullOrEmpty(groupName)) return "Recipient can't be both a person and a group";
        if (string.IsNullOrEmpty(recipient) && string.IsNullOrEmpty(groupName)) return "Recipient can't be empty";
        return string.Empty;
    }
}
