using SdCommonService.Enums;
using SdCommonService.Helpers;
using SdCommonService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SdCommonService.Controllers
{
    public class SdCommonServiceController : ApiController
    {
        [HttpPost]
        public ResultMessage GetQueryData([FromBody]GetQueryDataRequest _request)
        {
            var queryHelper = new QueryHelper();
            var result = new ResultMessage();

            if (_request == null)
            {
                result.Code = StatusCodeEnum.RequestBodyNull.ToString();
                result.Message = "Request body alınamadı veri tiplerini kontrol ediniz";
                return result;
            }

            var findQuery = "select QueryString from SdCommonService where [Key] ='@param'".Replace("@param", _request.QueryCode);
            var QueryString = queryHelper.ExecuteQuery(findQuery).Select(t=>t.QueryString).FirstOrDefault();
            var filter = queryHelper.GetWhereClause(_request.Parameters);
            var orderBy = queryHelper.GetOrderByClause(_request.OrderBy);
            try
            {
                if (!string.IsNullOrEmpty(QueryString))
                {
                    QueryString = string.Concat(QueryString, filter, orderBy);
                    var data = queryHelper.ExecuteQuery(QueryString);
                    result.Code = data != null ? StatusCodeEnum.Success.ToString() : StatusCodeEnum.NoDataFound.ToString();
                    result.Data = data;
                    result.Message = data != null ? "İşlem Başarılı" : "Sorgu Sonucu Kayıt Bulunamadı";
                    result.QueryCode = _request.QueryCode;
                }
                else
                {
                    result.Data = null;
                    result.Message = $"Database'de {_request.QueryCode} Key'i ile kayıtlı query bulunamadı. Yapılan sorgu: {findQuery} ";
                    result.Code = StatusCodeEnum.QueryNotFound.ToString();
                    result.QueryCode = _request.QueryCode;
                }

            }
            catch (System.Exception ex)
            {
                result.Code = (StatusCodeEnum.Error).ToString();
                result.Data = null;
                result.Message = ex.Message;
                result.QueryCode = _request.QueryCode;
            }
           
            return result;
        }
    }
}
