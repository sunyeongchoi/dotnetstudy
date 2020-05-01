using SportsStore.Domain.Entities;
using System.Web.Mvc;

namespace SportsStore.WebUI.Infrastructure.Binder
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, 
            ModelBindingContext bindingContext)
        {
            //세션에서 Cart개체 가져오기
            Cart cart = null;
            if(controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            }

            //세션 데이터에 Cart개체가 없다면 새로 생성한다.
            if(cart == null){
                cart = new Cart();
                if(controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }

            //Cart개체를 반환한다.
            return cart;
        }
    }
}