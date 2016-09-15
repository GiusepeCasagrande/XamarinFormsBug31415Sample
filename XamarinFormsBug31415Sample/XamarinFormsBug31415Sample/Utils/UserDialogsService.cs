using System;
using Acr.UserDialogs;

namespace XamarinFormsBug31415Sample.Utils
{
    public class UserDialogsService : IUserDialogsService
    {
        public void ShowError(string message = "")
        {
            UserDialogs.Instance.ShowError(message);
        }

        public void ShowSucces(string message = "")
        {
            UserDialogs.Instance.ShowSuccess(message);
        }

        public void ShowLoading(string title)
        {
            title = !string.IsNullOrEmpty(title) ? title : "Carregando...";
            UserDialogs.Instance.ShowLoading(title, MaskType.Gradient);
        }

        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}
