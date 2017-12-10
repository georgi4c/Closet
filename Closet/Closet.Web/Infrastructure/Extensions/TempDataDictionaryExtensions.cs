using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Closet.Web.Infrastructure.Extesions
{
    public static class TempDataDictionaryExtensions
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[WebConstants.TempDataSuccessMessageKey] = message;
        }
    }
}
