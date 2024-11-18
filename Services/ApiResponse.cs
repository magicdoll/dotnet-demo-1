using System.Dynamic;

namespace DapperWebApiExample.Services
{
    public class ApiResponse<T>
    {
        public T Result { get; set; }
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        public ApiResponse(T result, int statusCode = 200, string status = "OK", string message = "Success")
        {
            if (result is IEnumerable<IDictionary<string, object>> resultList)
            {
                Result = (T)(object)resultList.Select(row => row.ToDictionary(kvp => kvp.Key.ToLower(), kvp => kvp.Value)).ToList();
            }
            else if (result is IDictionary<string, object> resultDict)
            {
                Result = (T)(object)resultDict.ToDictionary(kvp => kvp.Key.ToLower(), kvp => kvp.Value);
            }
            else if (result is IEnumerable<dynamic> dynamicList)
            {
                var lowercaseList = dynamicList.Select(item =>
                {
                    var expando = new ExpandoObject() as IDictionary<string, object>;
                    foreach (var kvp in (IDictionary<string, object>)item)
                    {
                        expando[kvp.Key.ToLower()] = kvp.Value;
                    }
                    return expando;
                }).ToList();
                Result = (T)(object)lowercaseList;
            }
            else
            {
                Result = result;
            }

            StatusCode = statusCode;
            Status = status;
            Message = message;
        }
    }
}
