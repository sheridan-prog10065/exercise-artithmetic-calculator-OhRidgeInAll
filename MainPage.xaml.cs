using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Maui.Graphics.Text;

namespace MathOperators;

public partial class MainPage : ContentPage
{
    /// <summary>
    /// Remembers the expressions calculated by user
    /// </summary>
    private ObservableCollection<string> _expList;
    public MainPage()
    {
        InitializeComponent();
        
        _expList = new ObservableCollection<string>();
        _lstExpHistory.ItemsSource = _expList;

    }

    //Take the values provided in the input fields and perform the selected operation on them
    private async void OnCalculateFields(object sender, EventArgs e)
    {
        try
        {
            //Get User Input for operation
            var firstOperand = ReadFirstOperand();
            double secondOperand = double.Parse(_txtRightOp.Text);
            char opEntry = ((string)_pckOperand.SelectedItem)[0]; //cast to string is possible because SelectedItem is a String

            //Perform the operation and obtain result --> Method
            double result = PerformArithmeticOperation(opEntry, firstOperand, secondOperand);
            //Output result to field. Show the work
            string expression = $"{firstOperand} {opEntry} {secondOperand} = {result}";
        
            //Remember the expression
            _expList.Add(expression);

            _txtMathExp.Text = null;
            _txtMathExp.Text = $"{firstOperand} {opEntry} {secondOperand} = {result}";
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
        
    }

    private double ReadFirstOperand()
    {
        double firstOperand = double.Parse(_txtLeftOp.Text);
        return firstOperand;
    }


    //Perform the Arithmetic Operation and obtain the result
    private double PerformArithmeticOperation(char opEntry, double firstOperator, double secondOperator)
    {
        switch (opEntry)
        {
            case '+':
                return firstOperator + secondOperator;
            
            case '-':
                return firstOperator - secondOperator;
            
            case '*':
                return firstOperator * secondOperator;
            
            case '/':
                return PerformDivision(firstOperator, secondOperator);
            
            case '%':
                return firstOperator % secondOperator;
            
            default:
                Debug.Assert(false ,"unknown arithmetic operation. Unable to perform arithmetic operation.");
                return 0;
        }
        
    }

    private double PerformDivision(double firstOperator, double secondOperator)
    {
        string divOp = _pckOperand.SelectedItem as string;
        if (divOp.Contains("Int", StringComparison.OrdinalIgnoreCase))
        {
            //Integer division
            int result = (int)firstOperator / (int)secondOperator;
            return result;
        }
        else
        {
            //Real division
            return firstOperator / secondOperator;
        }
    }
}