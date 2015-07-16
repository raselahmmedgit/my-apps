using System.Text;
using SPEDU.Web.ViewModels;
using System.Web.Mvc;

namespace SPEDU.Web.Helpers
{
    public class VCardResultHelper : ActionResult
    {
        private VCardViewModel _card;

        protected VCardResultHelper() { }

        public VCardResultHelper(VCardViewModel card)
        {
            _card = card;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "text/vcard";
            //response.AddHeader("Content-Disposition", "attachment; fileName=" + _card.FirstName + " " + _card.LastName + ".vcf");
            response.AddHeader("Content-Disposition", "attachment; fileName=" + _card.FirstName + ".vcf");

            var cardString = _card.ToString();
            var inputEncoding = Encoding.Default;
            var outputEncoding = Encoding.GetEncoding("windows-1257");
            var cardBytes = inputEncoding.GetBytes(cardString);

            var outputBytes = Encoding.Convert(inputEncoding,
                                    outputEncoding, cardBytes);

            response.OutputStream.Write(outputBytes, 0, outputBytes.Length);
        }
    }
}