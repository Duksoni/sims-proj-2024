using Microsoft.Win32;
using PetNetwork.Application.Utility;
using PetNetwork.Application.Utility.Constants;
using System.IO;

namespace PetNetwork.WPF.ViewModels;

public class PaymentImportViewModel : AllPaymentsViewModel
{

    private string _filePath = string.Empty;
    public string FilePath
    {
        get => _filePath;
        set
        {
            if (!File.Exists(value))
            {
                Error = "File doesn't exist.";
                _filePath = string.Empty;
                return;
            }
            _filePath = value;
            OnPropertyChanged();
        }
    }

    private string _error = string.Empty;
    public string Error
    {
        get => _error;
        set
        {
            _error = value;
            OnPropertyChanged();
        }
    }

    private bool _importsSaved;
    public bool ImportsSaved
    {
        get => _importsSaved;
        private set
        {
            _importsSaved = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ChooseFileCommand => new(_ => ChooseFile());

    public RelayCommand TryImportingCommand => new(_ => TryImporting(), _ => FilePath != string.Empty && File.Exists(FilePath));

    public RelayCommand SaveImportsCommand => new(_ => SaveImports(), _ => Payments.Count > 0);


    public PaymentImportViewModel(): base(false) { }

    private void ChooseFile()
    {
        var dialog = new OpenFileDialog
        {
            DefaultExt = ".json",
            Filter = "Text documents (.json)|*.json"
        };

        var result = dialog.ShowDialog();

        if (result != true) return;
        FilePath = dialog.FileName;
        Error = string.Empty;
    }

    private void TryImporting()
    {
        Error = string.Empty;

        if (!IsValidSchema()) return;

        var paymentDtos = new PaymentExtractor(FilePath).TryExtracting(out var error);
        if (paymentDtos.Count == 0)
        {
            Error = error;
            return;
        }

        Payments.Clear();
        foreach (var paymentDto in paymentDtos)
            Payments.Add(new PaymentViewModel(paymentDto));
    }

    private void SaveImports()
    {
        foreach (var importedPayment in Payments)
            PaymentService.Add(importedPayment.ToPayment());
        ImportsSaved = true;
    }

    private bool IsValidSchema()
    {
        if (JsonSchemaValidator.IsValidDocument(GlobalConstants.BankStatementSchemaFilePath, FilePath,
                out var validationErrors)) return true;
        Error = validationErrors;
        return false;
    }
}