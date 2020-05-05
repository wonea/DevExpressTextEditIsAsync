using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace ValidationIsAsyncExample
{
    public class TestValidationRule : ValidationRule
    {
        #region Constructors

        public TestValidationRule() { }

        #endregion

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
            => ValidationResult.ValidResult;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo,
            BindingExpressionBase owner)
        {
            var vm = (MainViewModel)((BindingExpression)owner).DataItem;
            var model = vm.Person;
            return Validate(value, model);
        }

        public ValidationResult Validate(object value, PersonModel model)
        {
            try
            {
                if (value == null)
                    return ValidationResult.ValidResult;

                var testName = GetBoundValue(value)?.ToString();

                if (string.IsNullOrEmpty(testName))
                    return ValidationResult.ValidResult;
                if (testName == "correct")
                {
                    model.Name = "That's right!";
                    return ValidationResult.ValidResult;
                }
                return new ValidationResult(false, $"{testName} is not accepted");
            }
            catch
            {
                return new ValidationResult(false, "An unexpected error occurred");
            }
        }

        public static object GetBoundValue(object value)
        {
            if (value is BindingExpression binding)
            {
                // ValidationStep was UpdatedValue or CommittedValue (Validate after setting)
                // Need to pull the value out of the BindingExpression.

                // Get the bound object and name of the property
                object dataItem = binding.DataItem;
                string propertyName = binding.ParentBinding.Path.Path;

                // Extract the value of the property - works for nested classes
                object propertyValue;
                var propertyNames = propertyName.Split('.');
                if (propertyNames.Length > 1)
                {
                    object parentValue = dataItem.GetType().GetProperty(propertyNames[0]).GetValue(dataItem, null);
                    propertyValue =
                        parentValue.GetType().GetProperty(propertyNames[1]).GetValue(parentValue, null);
                }
                else
                {
                    propertyValue = dataItem.GetType().GetProperty(propertyName).GetValue(dataItem, null);
                }

                // This is what we want.
                return propertyValue;
            }

            // ValidationStep was RawProposedValue or ConvertedProposedValue
            // The argument is already what we want!
            return value;
        }
    }
}
