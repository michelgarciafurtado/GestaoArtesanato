namespace LojaApp.Models.Utils;

public static class PersonalFormatter
{
    public static decimal DecimalFormatter(string number)
    {
        try
        {
            if (decimal.TryParse(number, System.Globalization.NumberStyles.Currency,
                                new System.Globalization.CultureInfo("pt-BR"),
                                out decimal numero)
                )
            {
                return Math.Round(numero, 2);
            }
        }
        catch (System.Exception )
        {
            throw;
        }
        return 0;
    }

}
