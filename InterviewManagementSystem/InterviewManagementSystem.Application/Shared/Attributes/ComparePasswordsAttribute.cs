namespace InterviewManagementSystem.Application.Shared.Attributes;

internal sealed class ComparePasswordsAttribute(string comparisonProperty) : ValidationAttribute
{

    private readonly string _comparisonProperty = comparisonProperty;


    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {

        if (value is null)
            return new ValidationResult("Null value");


        var currentValue = value?.ToString();
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);


        if (property == null)
            return new ValidationResult($"Property '{_comparisonProperty}' not found.");


        var comparisonValue = property.GetValue(validationContext.ObjectInstance)?.ToString();


        if (!string.Equals(currentValue, comparisonValue))
            return new ValidationResult("The passwords do not match.");


        return ValidationResult.Success!;
    }
}
