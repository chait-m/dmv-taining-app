using System.ComponentModel.DataAnnotations;
using DmvTrainingApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Text.Json;

namespace DmvTrainingApp.Pages;

public partial class NewVehicleTitle : ComponentBase
{
    private int step = 1;
    private VehicleTitleModel vehicleTitleModel = new();
    private EditContext _editContext;
    [Inject] private IJSRuntime JS { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;
    private int lastApplicationNumber = 0;

    protected override void OnInitialized()
    {
        _editContext = new EditContext(vehicleTitleModel);
    }

    private string GetInputClass(string fieldName)
    {
        var fieldIdentifier = new FieldIdentifier(vehicleTitleModel, fieldName);
        if (_editContext.GetValidationMessages(fieldIdentifier).Any())
            return "is-invalid";
        if (_editContext.IsModified(fieldIdentifier))
            return "is-valid";
        return string.Empty;
    }

    private bool ValidateStep1()
    {
        _editContext.MarkAsUnmodified();
        var context = new ValidationContext(vehicleTitleModel);
        var results = new List<ValidationResult>();
        bool isValid = true;
        isValid &= Validator.TryValidateProperty(vehicleTitleModel.VIN, new ValidationContext(vehicleTitleModel) { MemberName = nameof(vehicleTitleModel.VIN) }, results);
        isValid &= Validator.TryValidateProperty(vehicleTitleModel.Make, new ValidationContext(vehicleTitleModel) { MemberName = nameof(vehicleTitleModel.Make) }, results);
        isValid &= Validator.TryValidateProperty(vehicleTitleModel.Model, new ValidationContext(vehicleTitleModel) { MemberName = nameof(vehicleTitleModel.Model) }, results);
        isValid &= Validator.TryValidateProperty(vehicleTitleModel.Year, new ValidationContext(vehicleTitleModel) { MemberName = nameof(vehicleTitleModel.Year) }, results);
        isValid &= Validator.TryValidateProperty(vehicleTitleModel.Color, new ValidationContext(vehicleTitleModel) { MemberName = nameof(vehicleTitleModel.Color) }, results);
        isValid &= Validator.TryValidateProperty(vehicleTitleModel.BodyType, new ValidationContext(vehicleTitleModel) { MemberName = nameof(vehicleTitleModel.BodyType) }, results);
        isValid &= Validator.TryValidateProperty(vehicleTitleModel.OdometerReading, new ValidationContext(vehicleTitleModel) { MemberName = nameof(vehicleTitleModel.OdometerReading) }, results);
        foreach (var result in results)
        {
            foreach (var memberName in result.MemberNames)
            {
                var field = new FieldIdentifier(vehicleTitleModel, memberName);
                _editContext.NotifyFieldChanged(field);
            }
        }
        _editContext.NotifyValidationStateChanged();
        return isValid;
    }

    private void NextStep()
    {
        bool isValid = step switch
        {
            1 => ValidateStep1(),
            _ => _editContext.Validate()
        };
        if (isValid)
        {
            step++;
            StateHasChanged();
        }
    }
    private void PrevStep()
    {
        if (step > 1) step--;
    }
    private async Task UploadFile(InputFileChangeEventArgs e, string propertyName)
    {
        var file = e.File;
        using var stream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(stream);
        var bytes = stream.ToArray();
        switch (propertyName)
        {
            case nameof(vehicleTitleModel.BillOfSale):
                vehicleTitleModel.BillOfSale = bytes;
                break;
            case nameof(vehicleTitleModel.ProofOfInsurance):
                vehicleTitleModel.ProofOfInsurance = bytes;
                break;
            case nameof(vehicleTitleModel.ProofOfIdentity):
                vehicleTitleModel.ProofOfIdentity = bytes;
                break;
        }
    }

    private async void HandleValidSubmit()
    {
        lastApplicationNumber = await SaveApplicationToLocalDb();
        // Navigate to confirmation page with application number
        Navigation.NavigateTo($"/application-confirmation/{lastApplicationNumber}");
        // Reset state for next use
        step = 1;
        vehicleTitleModel = new();
        _editContext = new EditContext(vehicleTitleModel);
    }

    private async Task<int> SaveApplicationToLocalDb()
    {
        // Get the last application number from local storage
        int lastAppNumber = await JS.InvokeAsync<int>("dmvAppDb.getLastAppNumber");
        int newAppNumber = lastAppNumber > 0 ? lastAppNumber + 1 : 100001;
        vehicleTitleModel.ApplicationNumber = newAppNumber;
        await JS.InvokeVoidAsync("dmvAppDb.saveApplication", JsonSerializer.Serialize(vehicleTitleModel), newAppNumber);
        return newAppNumber;
    }
}
